using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

//classe qui hérite des featuees réseau et on y set le temps de netsend au serveur)
[NetworkSettings(channel = 0, sendInterval = 0.033f)]
public class Player_SyncTransform : NetworkBehaviour {

	public struct t_playerTransform
	{
		public Vector3 playerPosition;
		public Quaternion playerRotation;
		public Quaternion camRotation;
	}

	//valeurs sync, elle sont hooké, à chauqe modif, la fonction de hook est appelé
	[SyncVar (hook = "syncPlayerTransformValues")] private t_playerTransform syncPlayerTransform;

	//référence du transform du player et de sa camera interne
	[SerializeField] Transform playerTransform;
	[SerializeField] Transform camTransform;

	//le client envois les nouvelles positions qu'en cas de changement majeur, on va donc lerper les derneires pos et rotations des clients pour le srendre fluide
	private float lerpRate;
	private float normalLerpRate = 15.0f;
	private float fasterLerpRate = 60.0f;

	//sauvegarde des dernires pos et rotation, si on est asse loin d'eux, on va pouvoir retransmettre au server
	private t_playerTransform lastPlayerTransform;

	//distance min pour envoyer les nouvelles pos et rot au serveur
	private float minPositionDiffToSend = 0.5f;
	private float minRotationDiffToSend = 5f;

	//reference du networkManager client et des valeurs pour afficher le ping
	private NetworkClient nClient;
	private int latency;
	private Text latencyText;

	//liste de l'historique de la position, rotation du joueur et rotation de la caméra, si ca lag on effectuera les actions dans l'ordre mais plsu rapidement pour donner l'impression que le jeu lag pas
	private List<t_playerTransform> syncPlayerTransformList = new List<t_playerTransform>();

	//si on est en retard on utilise donc l'historique
	[SerializeField] private bool useHistoricalLerping = false;

	//valeur min si on est enfin proche de la premier position de l'historique et qu'on peut supprimer le premier maillon afin de passer au suivant
	private float isCloseEnoughPosition = 0.2f;
	private float isCloseEnoughRotation = 2f;

	//longueur des listes mini pour passer accelerer la vitesse afin de rattrapper le retard
	private int minLengthListForHighSpeed = 5;

	void Start(){
		nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
		latencyText = GameObject.Find ("Latency Text").GetComponent<Text> ();
		lerpRate = normalLerpRate;
	}

	//update est call à chaque frame CPU du jeu, donc variable en fonction du taux d'utilisation du CPU
	void Update(){
		lerpTransform ();
		showLatency ();
	}

	//fixedUpdate est call à chaque valeur constante de temps, donc invariablement à interval réguliere
	void FixedUpdate(){
		TransmitTransform ();
	}

	//fonction qui gere quel lerping on utlise, celui qui fait pour quand ca lag ou le normal
	// et ca concerne uniquement l'affichage des autres clients
	void lerpTransform(){
		if (!isLocalPlayer) {
			if (useHistoricalLerping){
				historicalLerping();
			}
			else{
				ordinaryLerping();
			}
		}
	}

	//command est une command que le client peut appeller sur le serveur (la fonction n'existe que sur le serveur donc)
	[Command]
	void CmdProvideTransformToServer(t_playerTransform newPlayerTransform){
		syncPlayerTransform = newPlayerTransform;
	}

	//client n'existe que coté client, la fonction isLocalPlayer ne sera true que si le code en question est bien le player du client, sinon, tout les clients executeraient le code des autres
	//cette fonction envoie les nouvelles infos pos et rot au serveur que si il y a un changement majeur afin de ne pas spam le serveur.
	[Client]
	void TransmitTransform(){
		if (isLocalPlayer && (Vector3.Distance(playerTransform.position, lastPlayerTransform.playerPosition) > minPositionDiffToSend ||
		                      Quaternion.Angle(playerTransform.rotation, lastPlayerTransform.playerRotation) > minRotationDiffToSend ||
		                      Quaternion.Angle(camTransform.rotation, lastPlayerTransform.camRotation) > minRotationDiffToSend)) {
			lastPlayerTransform.playerPosition = playerTransform.position;
			lastPlayerTransform.playerRotation = playerTransform.rotation;
			lastPlayerTransform.camRotation = camTransform.rotation;
			CmdProvideTransformToServer(lastPlayerTransform);
		}
	}

	//fonction de hook que le client recoi, il met à jour la nouvelle position/rotation et l'ajoute à l'historique
	[Client]
	void syncPlayerTransformValues(t_playerTransform latestPlayerTransform){
		syncPlayerTransform = latestPlayerTransform;
		syncPlayerTransformList.Add (syncPlayerTransform);
	}

	//fonction qui chope le network client et qui affiche le ping
	void showLatency(){
		if (isLocalPlayer) {
			latency = nClient.GetRTT();
			latencyText.text = latency.ToString();
		}
	}

	//fonction appellé quand ca lag pas, elle fait un lerping réaliste des différentes valeurs
	void ordinaryLerping(){
		playerTransform.position = Vector3.Lerp(playerTransform.position, syncPlayerTransform.playerPosition, Time.deltaTime * lerpRate);
		playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerTransform.playerRotation, Time.deltaTime * lerpRate);
		camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncPlayerTransform.camRotation, Time.deltaTime * lerpRate);
	}

	//focntion appelé quand ca lag, elle va lerp le client à chaque étape de déplacement/rotation dans leur ordre chronologique afin que le joueur ne se rend pas compte que ca lag
	//(mais il y a un temps de retard entre ce que le client voit et la réalité, c'est du voyage dans le temps ouhouhouhyouhouyoohuyu), si l'une des listes devient trop longue, on accelere la vitesse
	void historicalLerping(){
		if (syncPlayerTransformList.Count > 0) {
			playerTransform.position = Vector3.Lerp(playerTransform.position, syncPlayerTransformList[0].playerPosition, Time.deltaTime * lerpRate);
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, syncPlayerTransformList[0].playerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncPlayerTransformList[0].camRotation, Time.deltaTime * lerpRate);
			if (Vector3.Distance(playerTransform.position, syncPlayerTransformList[0].playerPosition) < isCloseEnoughPosition &&
			    Quaternion.Angle(playerTransform.rotation, syncPlayerTransformList[0].playerRotation) < isCloseEnoughRotation &&
			    Quaternion.Angle(camTransform.rotation, syncPlayerTransformList[0].camRotation) < isCloseEnoughRotation)
				syncPlayerTransformList.RemoveAt(0);
		}
		if (syncPlayerTransformList.Count > minLengthListForHighSpeed)
			lerpRate = fasterLerpRate;
		else
			lerpRate = normalLerpRate;
	}
}
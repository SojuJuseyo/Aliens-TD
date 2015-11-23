using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_ID : NetworkBehaviour {

	[SyncVar] public string playerUniqueIdentity;

	public NetworkInstanceId PlayerNetID;
	public Player_Board.e_player playerTeam;

	private string playerPrefixName = "PlayerNetID : ";
	private Transform myTransform;
	private Text PlayerTeamText;

	public override void OnStartLocalPlayer(){
		GetNetIdentity ();
		setIdentity ();
		GameObject.Find ("GameInterfaces").GetComponent<InGameInterface> ().InitInterfaces (playerTeam, this.gameObject);
	}

	void Awake(){
		PlayerTeamText = GameObject.Find ("PlayerTeam Text").GetComponent<Text> ();
		myTransform = transform;
	}

	void Update(){
		if (myTransform.name == "" || myTransform.name == "Player(Clone)") {
			setIdentity();
		}
	}

	[Client]
	void GetNetIdentity(){
		PlayerNetID = GetComponent<NetworkIdentity>().netId;
		CmdTellServerMyIdentity (MakeUniqueIdentity());
	}
	
	void setIdentity(){
		if (!isLocalPlayer) {
			myTransform.name = playerUniqueIdentity;
		}
		else{
			myTransform.name = MakeUniqueIdentity();
		}
		GameObject[] Players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject Player in Players){
			string otherName = Player.GetComponent<Player_ID>().playerUniqueIdentity;
			if (otherName != playerUniqueIdentity){
				if (otherName != ""){
					playerTeam = Player_Board.e_player.PLAYER2;
					PlayerTeamText.text = "Player One";
				}
				else{
					playerTeam = Player_Board.e_player.PLAYER1;
					PlayerTeamText.text = "Player Two";

				}
			}
		}
		if (isLocalPlayer)
			GetComponent<Player_Board>().CreateBoards(this);
	}

	string MakeUniqueIdentity(){
		string uniqueName = playerPrefixName + PlayerNetID.ToString ();
		return uniqueName;
	}

	[Command]
	void CmdTellServerMyIdentity(string name){
		playerUniqueIdentity = name;
	}
}

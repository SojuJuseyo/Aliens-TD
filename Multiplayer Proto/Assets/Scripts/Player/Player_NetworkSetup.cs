using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_NetworkSetup : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	[SerializeField] AudioListener audioListener;

	public bool isReady = false;
	public bool areYouStillHere = false;


	void Update(){
		SayAlive ();
	}

	[Client]
	void SayAlive(){
		if (isLocalPlayer)
			CmdAlive();
	}

	[Command]
	void CmdAlive(){
		RpcSayAlive ();
	}

	[ClientRpc]
	void RpcSayAlive(){
		areYouStillHere = true;
	}

	[Client]
	public void setReady(bool value){
		if (isLocalPlayer)
			CmdSetReady(value);
	}

	[Command]
	void CmdSetReady(bool value){
		RpcSetReady (value);
	}

	[ClientRpc]
	void RpcSetReady(bool value){
		isReady = value;
	}

	public void SetEnabledPlayer(bool value){
		if (isLocalPlayer) {
			GameObject.Find("Scene Camera").GetComponent<Camera>().enabled = !value;
			GetComponent<Player_Controls>().enabled = value;
			FPSCharacterCam.enabled = value;
			audioListener.enabled = value;
		}
	}
}
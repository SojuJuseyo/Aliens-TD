using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	[SerializeField] AudioListener audioListener;

	void Start () {
		if (isLocalPlayer) {
			GameObject.Find("Scene Camera").SetActive(false);
			GetComponent<PlayerControls>().enabled = true;
			FPSCharacterCam.enabled = true;
			audioListener.enabled = true;
		}
	}
}
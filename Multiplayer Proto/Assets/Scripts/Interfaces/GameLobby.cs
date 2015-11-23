using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameLobby : NetworkBehaviour {

	public enum e_gamestate {NONE, WAITING_CONNECTION, WAITING_READY, GAME_STARTED};

	public Canvas CanvasHUD;
	public Text textStatus;
	public Text textButtonReady;
	public GameObject buttonReady;
	public e_gamestate gameState = e_gamestate.NONE;

	private InGameInterface menu;
	private TimerAndIncome timers;

	void Start(){
		menu = GetComponent<InGameInterface> ();
		timers = GetComponent<TimerAndIncome> ();
		menu.SetEnableAllCanvas (false);
		SetGameStatus (e_gamestate.WAITING_CONNECTION);
	}

	void Update(){
		UpdateStatus ();
		CheckIfAllPlayersReady ();
	}

	public void OnClickReady(){
		buttonReady.GetComponent<Image>().color = Color.red;
		textButtonReady.text = "Accepted";
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.GetComponent<Player_NetworkSetup>().setReady(true);
		}
	}
	
	private void UpdateStatus(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length > 1 && gameState == e_gamestate.WAITING_CONNECTION && gameState != e_gamestate.WAITING_READY) {
			SetGameStatus (e_gamestate.WAITING_READY);
		}
		if (players.Length <= 1 && gameState != e_gamestate.WAITING_CONNECTION){
			SetGameStatus (e_gamestate.WAITING_CONNECTION);
		}
	}

	private void SetGameStatus(e_gamestate mode){
		if (mode != gameState) {
			switch (mode) {
			case e_gamestate.WAITING_CONNECTION:
				if (gameState == e_gamestate.GAME_STARTED)
					Application.LoadLevel ("Menu");
				textStatus.text = "Waiting your opponent to connect";
				CanvasHUD.enabled = false;
				SetEnablePlayers (false);
				buttonReady.SetActive(false);
				gameState = mode;
				break;
			case e_gamestate.WAITING_READY:
				textStatus.text = "Your opponent is connected, accept the match when you're ready";
				buttonReady.GetComponent<Image>().color = Color.white;
				textButtonReady.text = "Accept";
				CanvasHUD.enabled = false;
				SetEnablePlayers (false);
				buttonReady.SetActive(true);
				gameState = mode;
				break;
			case e_gamestate.GAME_STARTED:
				textStatus.text = "";
				CanvasHUD.enabled = true;
				SetEnablePlayers (true);
				buttonReady.SetActive(false);
				gameState = mode;
				timers.StartCounters();
				break;
			}
		}
	}

	private void SetEnablePlayers(bool value){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players){
			player.GetComponent<Player_NetworkSetup>().SetEnabledPlayer(value);
		}
	}

	private void CheckIfAllPlayersReady(){
		if (gameState == e_gamestate.WAITING_READY) {
			bool isAllPlayersReady = true;
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<Player_NetworkSetup> ().isReady == false)
					isAllPlayersReady = false;
			}
			if (isAllPlayersReady)
				SetGameStatus (e_gamestate.GAME_STARTED);
		}
	}
}

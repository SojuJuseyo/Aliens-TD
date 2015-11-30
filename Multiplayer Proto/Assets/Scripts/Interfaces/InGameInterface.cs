using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameInterface : MonoBehaviour {

	public enum e_InterfaceMode {NONE, IN_GAME, PUT_TOWER, EDIT_TOWER};
	public Canvas CanvasHUD;
	public Canvas CanvasPutTower;
	public Canvas CanvasEditTower;
	public Player_Board.t_infoSlot LastFocusedSlot;
	public Player_Board.e_player playerTeam;

	private e_InterfaceMode currentMode = e_InterfaceMode.NONE;
	private GameObject PlayerObject;

	void Update(){
		CheckShortcuts ();
	}

	public void OnClickSell(){
		PlayerObject.GetComponent<Player_Board> ().SellTower (50, LastFocusedSlot);
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void OnCLickPutTower(int tower){
		Player_Board.e_tower newTower = (Player_Board.e_tower)tower;
		PlayerObject.GetComponent<Player_Board> ().WannaPutTower (newTower, LastFocusedSlot);
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void OnClickEditTowerLevelUp(){
		PlayerObject.GetComponent<Player_Board> ().WannaEditLevelUpTower(LastFocusedSlot);
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void OnClickEditTowerColor(int color){
		Player_Board.e_color newColor = (Player_Board.e_color)color;
		PlayerObject.GetComponent<Player_Board> ().WannaEditColorTower(newColor, LastFocusedSlot);
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void OnCLickCancel(){
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void InitInterfaces(Player_Board.e_player player, GameObject thePlayerObject){
		playerTeam = player;
		PlayerObject = thePlayerObject;
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public GameObject getMyPlayerObject(){
		return PlayerObject;
	}

	public void SetEnableAllCanvas(bool value){
		CanvasHUD.enabled = value;
		CanvasPutTower.enabled = value;
		CanvasEditTower.enabled = value;
	}

	public void changeMenuMode(e_InterfaceMode mode){
		if (currentMode != mode && GetComponent<GameLobby>().gameState == GameLobby.e_gamestate.GAME_STARTED){
			currentMode = mode;
			CanvasHUD.enabled = true;
			switch (mode){
				case e_InterfaceMode.IN_GAME :
					CanvasPutTower.enabled = false;
					CanvasEditTower.enabled = false;
					enableAllSlot(true);
					break;
				case e_InterfaceMode.PUT_TOWER :
					CanvasPutTower.enabled = true;
					CanvasEditTower.enabled = false;
					enableAllSlot(false);
					break;
				case e_InterfaceMode.EDIT_TOWER :
					CanvasPutTower.enabled = false;
					CanvasEditTower.enabled = true;
					enableAllSlot(false);
					break;
			}
		}
	}

	private void enableAllSlot(bool value){
		GameObject [] slots = GameObject.FindGameObjectsWithTag ("Slot");
		foreach (GameObject theSlot in slots) {
			FocusingSlot script = theSlot.GetComponent<FocusingSlot>();
			if (script.getInfos().player == playerTeam){
				script.enableSlot(value);
				if (value)
					script.resetRender();
			}
		}
	}

	private void CheckShortcuts(){
		if (currentMode == e_InterfaceMode.PUT_TOWER)
			ShortcutPutTower ();
		else if (currentMode == e_InterfaceMode.EDIT_TOWER)
			ShortcutEditTower ();
	}

	private void ShortcutEditTower(){
		Debug.Log ("shortcut edit");
		if (Input.GetButtonDown ("key1")) {
			OnClickEditTowerLevelUp();
		}
		if (Input.GetButtonDown ("key2")) {
			OnClickEditTowerColor(1);
		}
		if (Input.GetButtonDown ("key3")) {
			OnClickEditTowerColor(2);
		}
		if (Input.GetButtonDown ("key4")) {
			OnClickEditTowerColor(3);
		}
		if (Input.GetButtonDown ("key5")) {
			OnClickSell();
		}
		if (Input.GetButtonDown ("Cancel")) {
			OnCLickCancel();
		}
	}

	private void ShortcutPutTower(){
		Debug.Log ("shortcut put");
		if (Input.GetButtonDown ("key1")) {
			OnCLickPutTower(1);
		}
		if (Input.GetButtonDown ("key2")) {
			OnCLickPutTower(2);
		}
		if (Input.GetButtonDown ("key3")) {
			OnCLickPutTower(3);
		}
		if (Input.GetButtonDown ("key4")) {
			OnCLickPutTower(4);
		}
		if (Input.GetButtonDown ("key5")) {
			OnCLickPutTower(5);
		}
		if (Input.GetButtonDown ("key6")) {
			OnCLickPutTower(6);
		}
		if (Input.GetButtonDown ("Cancel")) {
			OnCLickCancel();
		}
	}
}

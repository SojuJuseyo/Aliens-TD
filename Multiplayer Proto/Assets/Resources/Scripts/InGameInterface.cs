using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameInterface : MonoBehaviour {

	public enum e_InterfaceMode {NONE, IN_GAME, PUT_TOWER, EDIT_TOWER};
	public Canvas CanvasHUD;
	public Canvas CanvasPutTower;
	public Canvas CanvasEditTower;
	public Player_Board.t_infoSlot LastFocusedSlot;

	private e_InterfaceMode currentMode = e_InterfaceMode.NONE;
	private Player_Board.e_player playerTeam;
	private GameObject PlayerObject;

	public void OnCLickPutTower(int tower){
		Player_Board.e_tower NewTower = (Player_Board.e_tower)tower;
		PlayerObject.GetComponent<Player_Board>().WannaPutTower(NewTower, LastFocusedSlot);
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void OnClickEditTowerLevelUp(int level){

	}

	public void OnClickEditTowerColor(int color){

	}

	public void OnCLickCancel(){
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void InitInterfaces(Player_Board.e_player player, GameObject thePlayerObject){
		playerTeam = player;
		PlayerObject = thePlayerObject;
		changeMenuMode (e_InterfaceMode.IN_GAME);
	}

	public void changeMenuMode(e_InterfaceMode mode){
		if (currentMode != mode){
			CanvasHUD.enabled = true;
			setButtons (CanvasHUD, true);
			switch (mode){
				case e_InterfaceMode.IN_GAME :
					CanvasPutTower.enabled = false;
					setButtons (CanvasPutTower, false);
					CanvasEditTower.enabled = false;
					setButtons (CanvasPutTower, false);
					enableAllSlot(true);
					break;
				case e_InterfaceMode.PUT_TOWER :
					CanvasPutTower.enabled = true;
					setButtons (CanvasPutTower, true);
					CanvasEditTower.enabled = false;
					setButtons (CanvasPutTower, false);
					enableAllSlot(false);
					break;
				case e_InterfaceMode.EDIT_TOWER :
					CanvasPutTower.enabled = false;
					setButtons (CanvasPutTower, false);
					CanvasEditTower.enabled = true;
					setButtons (CanvasPutTower, true);
					enableAllSlot(false);
					break;
			}
		}
	}

	private void setButtons(Canvas theCanvas, bool value){
		/*
		if (theCanvas) {
			theCanvas.GetComponentInChildren<Button> ().enabled = false;
			theCanvas.GetComponentInChildren<Button> ().enabled = value;
		}
		*/
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
}

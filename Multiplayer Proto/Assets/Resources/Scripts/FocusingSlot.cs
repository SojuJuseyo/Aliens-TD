using UnityEngine;
using System.Collections;

public class FocusingSlot : MonoBehaviour {

	//variables éditable en IDE
	public Material focusMaterial;
	public Material selectionnedMaterial;

	//environnement
	private Player_Board.t_infoSlot infosSlot;
	private bool isEnabled = true;

	public void resetRender(){
		GetComponent<MeshRenderer> ().enabled = false;
	}

	public void enableSlot(bool value){
		isEnabled = value;
	}

	public Player_Board.t_infoSlot getInfos(){
		return infosSlot;
	}

	public void setInfos(Player_Board.t_infoSlot newInfos){
		infosSlot = newInfos;
	}

	public int getID(){
		return (infosSlot.id);
	}
	
	void OnMouseOver(){
		if (isEnabled) {
			GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	void OnMouseExit(){
		if (isEnabled) {
			GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	void OnMouseDown(){
		if (isEnabled) {
			if (infosSlot.tower == Player_Board.e_tower.NONE){
				InGameInterface Menu = GameObject.Find("GameInterfaces").GetComponent<InGameInterface>();
				Menu.LastFocusedSlot = infosSlot;
				Menu.changeMenuMode(InGameInterface.e_InterfaceMode.PUT_TOWER);
			}
			else{
				GameObject.Find("GameInterfaces").GetComponent<InGameInterface>().changeMenuMode(InGameInterface.e_InterfaceMode.EDIT_TOWER);
			}
		}
	}	
}

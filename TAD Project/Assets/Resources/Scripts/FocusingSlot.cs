using UnityEngine;
using System.Collections;

public class FocusingSlot : MonoBehaviour {

	private BoardManager Board;
	private BoardManager.e_player player;
	private int slotID;

	public void setID(int id, BoardManager theBoard, BoardManager.e_player thePlayer){
		slotID = id;
		Board = theBoard;
		player = thePlayer;
	}

	public int getID(){
		return (slotID);
	}

	void OnMouseOver(){
		if (!Board.InterfaceInGame.isSlotsLocked)
		   GetComponent<MeshRenderer> ().enabled = true;
	}

	void OnMouseExit(){
		if (!Board.InterfaceInGame.isSlotsLocked)
		  GetComponent<MeshRenderer> ().enabled = false;
	}

	void OnMouseDown(){
		if (!Board.InterfaceInGame.isSlotsLocked){
		    BoardManager.infoSlot slot = Board.getSlotOnBoardID (slotID, player);
			if (slot.tower == BoardManager.e_tower.NONE)
		        Board.wannaPutTower(slot);
			else
				Board.wannaEditTower(slot);
		}
	}
}

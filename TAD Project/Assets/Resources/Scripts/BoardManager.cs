using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	//configuration des boards :
	public int LEN_BOARD_X = 60;
	public int LEN_BOARD_Y = 30;
	public int POS_BOARD1_X = 0;
	public int POS_BOARD1_Y = 0;
	public int POS_BOARD2_X = 0;
	public int POS_BOARD2_Y = 40;

	//structure contenue sur chaque case de la board qui contient toutes les infos d'une case :
	public struct infoSlot{
		public int			id;
		public float 		x;
		public float		z;
		public e_tower 		tower;
		public int 			level;
		public e_color 		color;
		public e_player		player;
		public GameObject	refSlotObject;
		public GameObject	refTower;
	}

	//infos :
	public enum e_tower {NONE, STANDARD, GATLING, AA, CAC, SNIPER, MORTAR, DETECTOR};
	public enum e_color {NONE, RED, GREEN, BLUE};
	public enum e_player {PLAYER1, PLAYER2};
	public List<infoSlot> slotsListPlayer1 = new List<infoSlot>();
	public List<infoSlot> slotsListPlayer2 = new List<infoSlot>();

	//environnement:
	public InterfaceInGameScript InterfaceInGame;

	//instanciement des bailles
	public void Awake(){
		createSlotsOnBoard (LEN_BOARD_X, LEN_BOARD_Y, POS_BOARD1_X, POS_BOARD1_Y, e_player.PLAYER1);
		createSlotsOnBoard (LEN_BOARD_X, LEN_BOARD_Y, POS_BOARD2_X, POS_BOARD2_Y, e_player.PLAYER2);
	}

	//init des bailles
	public void Start(){
		InterfaceInGame = this.GetComponent<InterfaceInGameScript> ();
	}

	//cree une board :
	public void createSlotsOnBoard(int dim1, int dim2, int coord1, int coord2, e_player player){
		Vector3 tmpPos;
		int idCount = 0;
		tmpPos.y = 0;
		dim1 += 2;
		dim2 += 2;
		for (int i = 0; i < dim1; i++){
			for (int o = 0; o < dim2; o++) {
				GameObject tmpSlot;
				bool isBorder;
				if (i == 0 || o == 0 || i == dim1 - 1 || o == dim2 - 1){
					tmpSlot = Instantiate (Resources.Load ("Prefabs/Border", typeof(GameObject))) as GameObject;
					isBorder = true;
				}
				else{
					tmpSlot = Instantiate (Resources.Load ("Prefabs/Slot", typeof(GameObject))) as GameObject;
					isBorder = false;
				}
				infoSlot tmpInfoSlot = new infoSlot();
				if (!isBorder){
					tmpInfoSlot.id = idCount++;
					tmpInfoSlot.tower = e_tower.NONE;
					tmpInfoSlot.level = 0;
					tmpInfoSlot.color = e_color.NONE;
				}
				if (player == e_player.PLAYER1){
					tmpPos.x = i + POS_BOARD1_X;
					tmpPos.z = o + POS_BOARD1_Y;
					tmpSlot.transform.position = tmpPos;
					tmpInfoSlot.x = tmpPos.x;
					tmpInfoSlot.z = tmpPos.z;
					tmpInfoSlot.player = e_player.PLAYER1;
					tmpInfoSlot.refSlotObject = tmpSlot;
					if (!isBorder)
						slotsListPlayer1.Add(tmpInfoSlot);
				}
				else {
					tmpPos.x = i + POS_BOARD2_X;
					tmpPos.z = o + POS_BOARD2_Y;
					tmpSlot.transform.position = tmpPos;
					tmpInfoSlot.x = tmpPos.x;
					tmpInfoSlot.z = tmpPos.z;
					tmpInfoSlot.player = e_player.PLAYER2;
					tmpInfoSlot.refSlotObject = tmpSlot;
					if (!isBorder)
						slotsListPlayer2.Add(tmpInfoSlot);
				}
				if (!isBorder){
					tmpSlot.name = player.ToString() + "_" + tmpInfoSlot.id.ToString();
					tmpSlot.GetComponent<FocusingSlot>().setID(tmpInfoSlot.id, this, player);
				}
				else
					tmpSlot.name = "Border";
			}
		}
	}

	//renvoie un infoSlot de la case, renvoi la premiere case en cas de fail
	public infoSlot getSlotOnBoardID(int id, e_player player){
		List<infoSlot> theList;

		if (player == e_player.PLAYER1)
			theList = slotsListPlayer1;
		else
			theList = slotsListPlayer2;
		foreach (infoSlot slot in theList){
			if (slot.id == id)
				return slot;
		}
		return theList[0];
	}


	public void setSlotOnBoardID(int id, e_player player, infoSlot newSlot){
		List<infoSlot> theList;
		
		if (player == e_player.PLAYER1)
			theList = slotsListPlayer1;
		else
			theList = slotsListPlayer2;
		for (int i = 0; i < theList.Count; i++){
			if (theList[i].id == id){
				theList[i] = newSlot;
				return;
			}
		}
	}

	//pour activer l'affichage du menu put turret
	public void wannaEditTower(infoSlot slot){
		InterfaceInGame.setInterfaceInGameMode (InterfaceInGameScript.e_ig_interface_mode.EDITSENTRY, slot);
	}

	//pour activer l'affichage du menu put turret
	public void wannaPutTower(infoSlot slot){
		InterfaceInGame.setInterfaceInGameMode (InterfaceInGameScript.e_ig_interface_mode.PUTSENTRY, slot);
	}

	//pose une tourelle
	public void putTower(int slotID, e_player player, e_tower tower){
		infoSlot slot = getSlotOnBoardID (slotID, player);
		if (slot.tower == e_tower.NONE){
			//load une tower a la con pour test
			GameObject tmpTower = new GameObject();
			if (tower == e_tower.STANDARD){
				tmpTower = Instantiate (Resources.Load ("Prefabs/Towers/TurretBasic", typeof(GameObject))) as GameObject;
				slot.tower = e_tower.STANDARD;
			}
			else if (tower == e_tower.GATLING){
				tmpTower = Instantiate (Resources.Load ("Prefabs/Towers/TurretGatling", typeof(GameObject))) as GameObject;
				slot.tower = e_tower.GATLING;
			}
			slot.refTower = tmpTower;
			Vector3 tmpPos;
			tmpPos.y = tmpTower.transform.position.y;
			tmpPos.x = slot.x;
			tmpPos.z = slot.z;
			tmpTower.transform.position = tmpPos;
			setSlotOnBoardID(slotID, player, slot);
			Debug.Log ("tourelle : " + tower.ToString() + " posee sur le slot " + slot.id.ToString() + " par le joueur : " + player.ToString());
		}
	}

	//revend une tourelle
	public void sellTower(int slotID, e_player player){
		infoSlot slot = getSlotOnBoardID (slotID, player);
		if (slot.tower != e_tower.NONE){
			slot.tower = e_tower.NONE;
			setSlotOnBoardID(slotID, player, slot);
			Destroy (slot.refTower);
		}
	}

	//change la couleur d'une tourelle
	public void changeColorTower(int slotID, e_player player, e_color newColor){
		infoSlot slot = getSlotOnBoardID (slotID, player);

		if (slot.color != newColor) {

		}
	}
}

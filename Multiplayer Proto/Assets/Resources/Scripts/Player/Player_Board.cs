using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player_Board: NetworkBehaviour {

	//configuration des boards et variables éditeur :
	public int LEN_BOARD_X;
	public int LEN_BOARD_Y;
	public int POS_BOARD1_X;
	public int POS_BOARD1_Y;
	public int POS_BOARD2_X;
	public int POS_BOARD2_Y;
	public Material redTowerMaterial; //material utilise pour la couleur red
	public Material greenTowerMaterial; //material utilise pour la couleur green
	public Material blueTowerMaterial; //material utilise pour la couleur blue

	//infos :
	public enum e_tower {NONE, BASIC, GATLING, AA, CAC, SNIPER, MORTAR, DETECTOR};
	public enum e_color {NONE, RED, GREEN, BLUE};
	public enum e_player {PLAYER1, PLAYER2};
	public List<t_infoSlot> slotsListPlayer1 = new List<t_infoSlot>();
	public List<t_infoSlot> slotsListPlayer2 = new List<t_infoSlot>();

	//environnement:
	private Player_ID playerIdentity;
	
	//structure contenue sur chaque case de la board qui contient toutes les infos d'une case :
	public struct t_infoSlot{
		public int							id;
		public float 						x;
		public float						z;
		public e_tower 						tower;
		public string 						name;
		public int							level;
		public e_color 						color;
		public e_player						player;
	}

	public void CreateBoards(Player_ID ThePlayer){
		if (isLocalPlayer) {
			playerIdentity = ThePlayer;
			createBoard (POS_BOARD1_X, POS_BOARD1_Y, LEN_BOARD_X, LEN_BOARD_Y, e_player.PLAYER1);
			createBoard (POS_BOARD2_X, POS_BOARD2_Y, LEN_BOARD_X, LEN_BOARD_Y, e_player.PLAYER2);
		}
	}

	[Client]
	void createBoard(int x, int z, int dimx, int dimz, e_player player){
		Vector3 tmpPos;
		bool isSlot;
		int id = 0;

		dimx += 2;
		dimz += 2;
		tmpPos.y = 0;
		tmpPos.x = 0;
		tmpPos.z = 0;
		for (int dim1 = 0; dim1 < dimx; ++dim1) {
			for (int dim2 = 0; dim2 < dimz; ++dim2){
				GameObject objectSlot;
				if (dim1 == 0 || dim2 == 0 || dim1 == dimx - 1 || dim2 == dimz - 1){
					objectSlot = Instantiate (Resources.Load ("Prefabs/Border", typeof(GameObject))) as GameObject;
					isSlot = false;
				}
				else{
					objectSlot = Instantiate (Resources.Load ("Prefabs/Slot", typeof(GameObject))) as GameObject;
					isSlot = true;
				}
				tmpPos.x = x + dim1;
				tmpPos.z = z + dim2;
				tmpPos.y = objectSlot.transform.position.y;
				objectSlot.transform.position = tmpPos;
				objectSlot.transform.name = player.ToString() + "-Border";
				if (isSlot){
					t_infoSlot newInfosSLot = new t_infoSlot();
					newInfosSLot.player = player;
					newInfosSLot.x = tmpPos.x;
					newInfosSLot.z =  tmpPos.z;
					newInfosSLot.tower = e_tower.NONE;
					newInfosSLot.color = e_color.NONE;
					newInfosSLot.level = 0;
					newInfosSLot.name = player.ToString() + "-" + id.ToString();
					objectSlot.GetComponent<FocusingSlot>().setInfos(newInfosSLot);
					if (playerIdentity.playerTeam != player)
						objectSlot.GetComponent<FocusingSlot>().enableSlot(false);
					objectSlot.transform.name = newInfosSLot.name;
					id++;
				}
			}
		}
	}

	[Client]
	public void WannaPutTower(e_tower tower, t_infoSlot slot){
		Debug.Log ("WannaPutTower : " + tower.ToString ());
		CmdCLientWantsPutTower (tower, slot);
	}

	[Command]
	void CmdCLientWantsPutTower(e_tower tower, t_infoSlot slot){
		PutTower (tower, slot);
	}

	[Client]
	public void PutTower(e_tower tower, t_infoSlot slot){
		GameObject NewTowerObject = new GameObject ();
		switch (tower) {
		case e_tower.BASIC:
			NewTowerObject = Instantiate (Resources.Load ("Prefabs/Towers/Turret B (standard)", typeof(GameObject))) as GameObject;
			break;
		case e_tower.GATLING:
			NewTowerObject = Instantiate (Resources.Load ("Prefabs/Towers/Turret E (gatling)", typeof(GameObject))) as GameObject;
			break;
		}
		Vector3 NewPos = new Vector3 ();
		NewPos.x = slot.x;
		NewPos.z = slot.z;
		NewPos.y = NewTowerObject.transform.position.y;
		NewTowerObject.transform.position = NewPos;
	}

	/*
	[Client]
	void connectToServer(){
		if (isLocalPlayer) {
			//debug += " send ";
			CmdNewClientConnected (GetComponent<Player_ID>().PlayerNetID);
		}
	}

	[Command]
	void CmdNewClientConnected(NetworkInstanceId PlayerNetID){
		Debug.Log ("commandCalled from :" + PlayerNetID.ToString());
		//debug += " commandCalled ";
		receive ();
	}

	[Client]
	void receive(){
		transform.name += " received";
	}
	*/
}

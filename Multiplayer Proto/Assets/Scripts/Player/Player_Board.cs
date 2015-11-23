using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player_Board: NetworkBehaviour {

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
		public GameObject					refTower;
	}

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
	public GameObject mobSpawnCubeAllied; //bloc de spawn des mobs
	public GameObject mobSpawnCubeEnemy;
	public GameObject slotObject; //go pour le focusing slot
	public GameObject borderObjectAllied; //go pour la bordure
	public GameObject borderObjectEnemy;
	public GameObject borderNexus;
	public GameObject towerStandard;
	public GameObject towerGatling;
	public GameObject towerAntiAir;
	public GameObject towerSplash;
	public GameObject towerSniper;
	public GameObject towerMelee;
	public int money = 500;
	public TimerAndIncome timers;

	//infos :
	public enum e_tower {NONE, STANDARD, GATLING, ANTIAIR, MELEE, SNIPER, SPLASH};
	public enum e_color {NONE, RED, GREEN, BLUE};
	public enum e_player {PLAYER1, PLAYER2};
	public List<t_infoSlot> slotsListPlayer1 = new List<t_infoSlot>();
	public List<t_infoSlot> slotsListPlayer2 = new List<t_infoSlot>();

	//environnement:
	public Player_ID playerIdentity;

	private e_player playerTeam;

	void Start(){
		timers = GameObject.Find ("GameInterfaces").GetComponent<TimerAndIncome> ();
	}

	public void CreateBoards(Player_ID ID){
		if (isLocalPlayer) {
			playerIdentity = ID;
			playerTeam = playerIdentity.playerTeam;
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
				tmpPos.x = x + dim1;
				tmpPos.z = z + dim2;
				tmpPos.y = 0f;
				if (dim1 == 0 || dim2 == 0 || dim1 == dimx - 1 || dim2 == dimz - 1){
					if (dim2 == dimz - 1 && dim1 == (dimx - 1) / 2){
						if (playerIdentity.playerTeam == player)
							objectSlot = Instantiate(mobSpawnCubeEnemy, tmpPos, Quaternion.identity) as GameObject;
						else
							objectSlot = Instantiate(mobSpawnCubeAllied, tmpPos, Quaternion.identity) as GameObject;
						objectSlot.transform.name = player.ToString() + "-MOBSPAWN";
					}
					else if (dim2 == 0 && dim1 == (dimx - 1) / 2){
						objectSlot = Instantiate(borderNexus, tmpPos, Quaternion.identity) as GameObject;
						objectSlot.transform.name = player.ToString() + "-NEXUS";
					}
					else{
						if (playerIdentity.playerTeam == player)
							objectSlot = Instantiate(borderObjectAllied, tmpPos, Quaternion.identity) as GameObject;
						else
							objectSlot = Instantiate(borderObjectEnemy, tmpPos, Quaternion.identity) as GameObject;
						objectSlot.transform.name = player.ToString() + "-Border";

					}
					isSlot = false;
				}
				else{
					objectSlot = Instantiate(slotObject, tmpPos, Quaternion.identity) as GameObject;
					isSlot = true;
				}
				if (isSlot){
					t_infoSlot newInfosSLot = new t_infoSlot();
					newInfosSLot.player = player;
					newInfosSLot.x = tmpPos.x;
					newInfosSLot.z =  tmpPos.z;
					newInfosSLot.tower = e_tower.NONE;
					newInfosSLot.color = e_color.NONE;
					newInfosSLot.level = 0;
					newInfosSLot.name = player.ToString() + "-" + id.ToString();
					newInfosSLot.id = id;
					newInfosSLot.refTower = null;
					objectSlot.GetComponent<FocusingSlot>().setInfos(newInfosSLot);
					if (playerIdentity.playerTeam != player)
						objectSlot.GetComponent<FocusingSlot>().enableSlot(false);
					objectSlot.transform.name = newInfosSLot.name;
					id++;
				}
			}
		}
	}

	public void SellTower(int value){
		if (playerIdentity != null)
			money += value;
	}

	[Client]
	public void WannaPutTower(e_tower tower, t_infoSlot slot){
		if (slot.tower == e_tower.NONE) {
			slot.tower = tower;
			CmdPutTower (tower, slot);
		}
	}

	[Command]
	void CmdPutTower(e_tower tower, t_infoSlot slot){
		RpcPutTower(tower, slot);
	}

	[ClientRpc]
	void RpcPutTower(e_tower tower, t_infoSlot slot){
		GameObject newTowerObject = new GameObject ();
		Vector3 newPos = new Vector3 ();
		newPos.x = slot.x;
		newPos.z = slot.z;
		newPos.y = 0f;
		switch (tower) {
		case e_tower.STANDARD:
			newTowerObject = Instantiate(towerStandard, newPos, Quaternion.identity) as GameObject;
			break;
		case e_tower.GATLING:
			newTowerObject = Instantiate(towerGatling, newPos, Quaternion.identity) as GameObject;
			break;
		case e_tower.ANTIAIR:
			newTowerObject = Instantiate(towerAntiAir, newPos, Quaternion.identity) as GameObject;
			break;
		case e_tower.MELEE:
			newTowerObject = Instantiate(towerMelee, newPos, Quaternion.identity) as GameObject;
			break;
		case e_tower.SPLASH:
			newTowerObject = Instantiate(towerSplash, newPos, Quaternion.identity) as GameObject;
			break;
		case e_tower.SNIPER:
			newTowerObject = Instantiate(towerSniper, newPos, Quaternion.identity) as GameObject;
			break;
		}
		slot.tower = tower;
		slot.refTower = newTowerObject;
		FocusingSlot SlotScript = GameObject.Find(slot.player.ToString() + "-" + slot.id.ToString()).GetComponent<FocusingSlot>();
		SlotScript.setInfos(slot);
		if (playerIdentity != null)
			money -= 100;
	}
	
	[Client]
	public void WannaEditColorTower(e_color newColor, t_infoSlot slot){
		if (slot.color != newColor){
			slot.color = newColor;
			CmdEditColorTower(slot);
		}
	}

	[Command]
	void CmdEditColorTower(t_infoSlot slot){
		RpcEditColorTower (slot);
	}
	
	[ClientRpc]
	void RpcEditColorTower(t_infoSlot slot){
		e_color newColor = slot.color;
		slot = GameObject.Find (slot.player.ToString () + "-" + slot.id.ToString ()).GetComponent<FocusingSlot> ().getInfos ();
		slot.color = newColor;
		Renderer tmpMaterial;
		Light tmpLight;
		int children = slot.refTower.transform.childCount;
		for (int i = 0; i < children; ++i){
			switch (slot.color) {

			case e_color.RED:
				tmpMaterial = slot.refTower.transform.GetChild(i).GetComponent<Renderer> ();
				if (tmpMaterial)
					tmpMaterial.material = redTowerMaterial;
				tmpLight = slot.refTower.transform.GetChild(i).GetComponent<Light> ();
				if (tmpLight)
					tmpLight.color = Color.red;
				break;
				
			case e_color.GREEN:
				tmpMaterial = slot.refTower.transform.GetChild(i).GetComponent<Renderer> ();
				if (tmpMaterial)
					tmpMaterial.material = greenTowerMaterial;
				tmpLight = slot.refTower.transform.GetChild(i).GetComponent<Light> ();
				if (tmpLight)
					tmpLight.color = Color.green;
				break;
				
			case e_color.BLUE:
				tmpMaterial = slot.refTower.transform.GetChild(i).GetComponent<Renderer> ();
				if (tmpMaterial)
					tmpMaterial.material = blueTowerMaterial;
				tmpLight = slot.refTower.transform.GetChild(i).GetComponent<Light> ();
				if (tmpLight)
					tmpLight.color = Color.blue;
				break;
			}
		}
		slot.refTower.GetComponent<Tower> ().UpdateInfos (slot);
		FocusingSlot SlotScript = GameObject.Find(slot.player.ToString() + "-" + slot.id.ToString()).GetComponent<FocusingSlot>();
		SlotScript.setInfos(slot); 
	}

	[Client]
	public void WannaEditLevelUpTower(t_infoSlot slot){
		slot.level++;
		CmdEditLevelUpTower (slot);
	}

	[Command]
	void CmdEditLevelUpTower(t_infoSlot slot){
		RpcEditLevelUpTower (slot);
	}

	[ClientRpc]
	void RpcEditLevelUpTower(t_infoSlot slot){
		int newLevel = slot.level;
		slot = GameObject.Find (slot.player.ToString () + "-" + slot.id.ToString ()).GetComponent<FocusingSlot> ().getInfos ();
		slot.level = newLevel;

		//appliquer le grossisement ou les bailles de changement de niveau visuel du level up de la tour

		slot.refTower.GetComponent<Tower> ().UpdateInfos (slot);
		FocusingSlot SlotScript = GameObject.Find(slot.player.ToString() + "-" + slot.id.ToString()).GetComponent<FocusingSlot>();
		SlotScript.setInfos(slot);
	}

	[Client]
	public void wannaIncome(int incomeValue){
		CmdIncome (incomeValue);
	}

	[Command]
	void CmdIncome(int incomeValue){
		RpcIncome (incomeValue);
	}

	[ClientRpc]
	void RpcIncome(int incomeValue){
		if (playerIdentity == null) {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players){
				if (player.GetComponent<Player_ID>().playerTeam == e_player.PLAYER2)
					player.GetComponent<Player_Board>().money += incomeValue;
			}
		}
		else
			money += incomeValue;
		timers.ResetIncomeTime ();
	}
}

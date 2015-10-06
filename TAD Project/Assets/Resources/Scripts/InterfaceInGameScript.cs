using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceInGameScript : MonoBehaviour {

	public enum e_ig_interface_mode {NORMAL, PUTSENTRY, EDITSENTRY}; //enum entre les differents mode de l'HUD

	private e_ig_interface_mode e_ig_ihm_mode; //variable des differents mode d'affichage
	private BoardManager Board; //reference du gameobject BoardManager qui contient toutes les infos du plateau de jeu
	private BoardManager.infoSlot focusedSlot; //reference de la case, du slot sur le quel le joueur a clique, ce slot contient toutes infos de la case (la tour, le niveau, l'id de la case)
	public bool isSlotsLocked = false; //si ce bool est true : le joueur ne peut pas cliquer ou selectionner un autre slot

	//sert a etre appele de l'exterieur : change le mode d'affichage de ce script, c'est appele quand le joueur veut poser une tour ou en editer
	public void setInterfaceInGameMode(e_ig_interface_mode newMode, BoardManager.infoSlot slot){
		focusedSlot = slot; //du coup, on met a jour le slot concerne par cette modif
		e_ig_ihm_mode = newMode; //on change l'enum du mode d'affichage
		isSlotsLocked = true; //et on lock les autres slot pour faire comprendre au joueur qu'il doit d'abord sortir de ce menu avant de faire autre chose (et pour bien qu'il voit quel slot est conserne)
	}

	//fonction appelle automatiquement au debut du jeu, c'est ici qu'on init des bailles
	void Start(){
		Board = this.GetComponent<BoardManager>();
		resetNormalMode ();
	}

	//fonction appelle automatpiquement a chaque frame d'affichage du jeu, en fonction du mode
	void OnGUI(){
		displayHUD ();
		switch (e_ig_ihm_mode){
			case e_ig_interface_mode.NORMAL :
				displayNormal();
				break;
			case e_ig_interface_mode.PUTSENTRY :
				displayPutSentry();
				break;
			case e_ig_interface_mode.EDITSENTRY :
				displayEditSentry();
				break;
		}
	}

	//affichage constant
	void displayHUD(){
		GUI.Label(new Rect(10, 10, 1000, 20), "Tower Attack Defense Project");
	}

	//n'affiche que quand le joueur ne pose et n'edit rien du tout, le jeu "normal"
	void displayNormal(){
		GUI.Label (new Rect (500, 20, 500, 20), "Posez des tourelles pour defendre votre camp");
	}

	//affiche quand le joueur veut poser une nouvelles tour
	void displayPutSentry(){
		int len 	= Screen.width / 5;
		int x 		= 100;
		int height 	= 30;
		int y 		= 50;
		int i 		= 3;
		
		GUI.Label (new Rect (100, 50, 200, 50), "Seigneur Jean-Freude, quelle tourelle voulez vous poser ici ?");
		if (GUI.Button (new Rect (x, y * i, len, height), "Tower Basic")) {
			Board.putTower (focusedSlot.id, focusedSlot.player, BoardManager.e_tower.STANDARD);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Tower Gatling")) {
			Board.putTower (focusedSlot.id, focusedSlot.player, BoardManager.e_tower.GATLING);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Tower AA")) {
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Tower Sniper")) {
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Cancel")) {
			resetNormalMode();
		}
	}

	//affichage quand le joueur veut editer une tour
	void displayEditSentry(){
		int len 	= Screen.width / 5;
		int x 		= 100;
		int height 	= 30;
		int y 		= 50;
		int i 		= 3;
		
		GUI.Label (new Rect (100, 50, 200, 50), "Seigneur Jean-Freude, que voulez vous faire avec cette tourelle ?");
		if (GUI.Button (new Rect (x, y * i, len, height), "Level up !")) {
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Red power")) {
			Board.changeColorTower(focusedSlot.id, focusedSlot.player, BoardManager.e_color.RED);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Green power")) {
			Board.changeColorTower(focusedSlot.id, focusedSlot.player, BoardManager.e_color.GREEN);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Blue Power")) {
			Board.changeColorTower(focusedSlot.id, focusedSlot.player, BoardManager.e_color.BLUE);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Sell this shit")) {
			Board.sellTower(focusedSlot.id, focusedSlot.player);
			resetNormalMode();
		}
		i++;
		if (GUI.Button (new Rect (x, y * i, len, height), "Cancel")) {
			resetNormalMode();
		}
	}

	// remet les settings en mode normal pour ingame pret a recevoir de nouveaux ordre du joueur
	void resetNormalMode(){
		e_ig_ihm_mode = e_ig_interface_mode.NORMAL;
		isSlotsLocked = false;
		hideAllSlotSelection();
	}


	void hideAllSlotSelection(){
		List<BoardManager.infoSlot> theList;
		
		if (focusedSlot.player == BoardManager.e_player.PLAYER1)
			theList = Board.slotsListPlayer1;
		else
			theList = Board.slotsListPlayer2;
		for (int i = 0; i < theList.Count; i++){
			theList[i].refSlotObject.GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}
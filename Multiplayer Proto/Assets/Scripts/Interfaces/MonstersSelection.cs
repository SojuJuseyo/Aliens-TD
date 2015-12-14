using UnityEngine;
using System.Collections;

public class MonstersSelection : MonoBehaviour {
	
	public enum e_enemy {NONE, MONSTER1, MONSTER2, MONSTER3, MONSTER4, MONSTER5, MONSTER6, MONSTER7, MONSTER8, MONSTER9};

	private Player_MonstersSpawn PlayerMonsterScript;
	private Player_Board.e_player playerTeam;

	public void OnClickEnemy(int enemyID){
		e_enemy newEnemy = (e_enemy)enemyID;

		if (PlayerMonsterScript == null) {
			InGameInterface menu = GetComponent<InGameInterface> ();
			playerTeam = menu.playerTeam;
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<Player_ID>().playerTeam == playerTeam)
					PlayerMonsterScript = player.GetComponent<Player_MonstersSpawn>();
			}
		}
		PlayerMonsterScript.WannaCreateMob (newEnemy, playerTeam);
	}
}

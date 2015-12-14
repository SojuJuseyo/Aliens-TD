using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_MonstersSpawn : NetworkBehaviour {

	public GameObject monster1;
	public GameObject monster2;
	public GameObject monster3;
	public GameObject monster4;
	public GameObject monster5;
	public GameObject monster6;
	public GameObject monster7;
	public GameObject monster8;
	public GameObject monster9;

	[HideInInspector]
	public GameObject enemySpawn;
	[HideInInspector]
	public GameObject myForceSpawn;

	[Client]
	public void WannaCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		int money;
		UpgradeMonsters.Stats monsterInfo = GameObject.Find ("Board").GetComponent<UpgradeMonsters> ().getNewStats (mob, 1);
		money = GameObject.Find ("PlayerNetID : 1").GetComponent<Player_Board> ().money;
		Debug.Log ("money = " + money);
		Debug.Log ("cost = " + monsterInfo.cost);
		if (monsterInfo.cost <= money) {
			CmdCreateMob (mob, player);
		}
	}

	[Command]
	void CmdCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		RpcCreateMob (mob, player);
	}

	[ClientRpc]
	void RpcCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		if (enemySpawn == null || myForceSpawn == null){
			Player_Board.e_player playerTeam = GetComponent<Player_ID> ().playerTeam;
			enemySpawn = GameObject.Find(playerTeam.ToString() + "-MOBSPAWN");
			if (playerTeam == Player_Board.e_player.PLAYER1){
				myForceSpawn = GameObject.Find(Player_Board.e_player.PLAYER2.ToString() + "-MOBSPAWN");
			}
			else{
				myForceSpawn = GameObject.Find(Player_Board.e_player.PLAYER1.ToString() + "-MOBSPAWN");
			}
		}
		Vector3 PopPosition = new Vector3 ();
		if (player == GetComponent<Player_ID> ().playerTeam) {
			PopPosition = myForceSpawn.transform.position;
		}
		else {
			PopPosition = enemySpawn.transform.position;
		}
		UpgradeMonsters.Stats monsterInfo = GameObject.Find ("Board").GetComponent<UpgradeMonsters> ().getNewStats (mob, 1);
		GameObject mobGO = new GameObject();
		switch (mob) {
		case MonstersSelection.e_enemy.MONSTER1:
			mobGO = Instantiate(monster1, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER2:
			mobGO = Instantiate(monster2, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER3:
			mobGO = Instantiate(monster3, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER4:
			mobGO = Instantiate(monster4, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER5:
			mobGO = Instantiate(monster5, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER6:
			mobGO = Instantiate(monster6, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER7:
			mobGO = Instantiate(monster7, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER8:
			mobGO = Instantiate(monster8, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.MONSTER9:
			mobGO = Instantiate(monster9, PopPosition , Quaternion.identity) as GameObject;
			break;
		}
		mobGO.GetComponent<Monster> ().setPlayerBoard (GetComponent<Player_ID> ().playerTeam);
		mobGO.GetComponent<Monster> ().upgrade (1);
		GameObject.Find ("PlayerNetID : 1").GetComponent<Player_Board> ().money -= monsterInfo.cost;
	}

	public void SetSeekerPlayer () {
		gameObject.AddComponent <Seeker>();
	}
}

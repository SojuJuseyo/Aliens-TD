using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_MonstersSpawn : NetworkBehaviour {

	public GameObject enemyStandard;
	public GameObject enemyScoot;
	public GameObject enemyHeavy;
	public GameObject enemyAir;

	[HideInInspector]
	public GameObject enemySpawn;
	[HideInInspector]
	public GameObject myForceSpawn;

	[Client]
	public void WannaCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		CmdCreateMob (mob, player);
	}

	[Command]
	void CmdCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		RpcCreateMob (mob, player);
	}

	[ClientRpc]
	void RpcCreateMob(MonstersSelection.e_enemy mob, Player_Board.e_player player){
		if (enemySpawn == null || myForceSpawn == null) {
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
		GameObject mobGO = new GameObject();
		switch (mob) {
		case MonstersSelection.e_enemy.STANDARD:
			mobGO = Instantiate(enemyStandard, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.SCOOT:
			mobGO = Instantiate(enemyScoot, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.HEAVY:
			mobGO = Instantiate(enemyHeavy, PopPosition , Quaternion.identity) as GameObject;
			break;
		case MonstersSelection.e_enemy.AIR:
			mobGO = Instantiate(enemyAir, PopPosition , Quaternion.identity) as GameObject;
			break;
		}
		mobGO.GetComponent<Monster> ().setPlayerBoard (GetComponent<Player_ID> ().playerTeam);
	}

	public void SetSeekerPlayer () {
		gameObject.AddComponent <Seeker>();
	}
}

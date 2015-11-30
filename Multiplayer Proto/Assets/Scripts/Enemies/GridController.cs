using UnityEngine;
using System.Collections;
using Pathfinding;

public class GridController : MonoBehaviour {

	private AstarData data;
	private GridGraph ggPlayer1;
	private GridGraph ggPlayer2;
	private GameObject[] players;

	public Path path1;
	public Path path2;
	public Transform spawn1;
	public Transform spawn2;
	public Transform end1;
	public Transform end2;

	void Start() {
		data = AstarPath.active.astarData;
	}

	public void InitGridController (int xPlayer, int yPlayer, Vector3 centerplayer1, Vector3 centerPlayer2, GameObject spawnMobPlayer1, GameObject spawnMobPlayer2, Player_Board.e_player player) {
		if (player == Player_Board.e_player.PLAYER1) {
			spawn1 = spawnMobPlayer2.transform;
			spawn2 = spawnMobPlayer1.transform;
		}
		else {
			spawn1 = spawnMobPlayer1.transform;
			spawn2 = spawnMobPlayer2.transform;
		}
		end1 = GameObject.Find ("PLAYER1-NEXUS").transform;
		end2 = GameObject.Find ("PLAYER2-NEXUS").transform;
		createGrah (ggPlayer1, xPlayer, yPlayer, centerplayer1);
		createGrah (ggPlayer2, xPlayer, yPlayer, centerPlayer2);
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject tmpPlayer in players){
			tmpPlayer.GetComponent<Player_ID>().SetSeekerPlayer();
		}
		Debug.Log ("spawnmob : " + spawn1.transform.position.ToString () + " nexus p1 : " + end1.position.ToString ());
		findPath ();
	}

	 void createGrah (GridGraph player, int x, int y, Vector3 center) {
		player = data.AddGraph(typeof(GridGraph)) as GridGraph;
		player.width = x;
		player.depth = y;
		player.center = center;
		player.nodeSize = 1f;
		player.UpdateSizeFromWidthDepth();
		player.collision.diameter = 5;
		player.collision.mask = LayerMask.GetMask("Tower");
		player.collision.thickRaycast = true;
		//player.collision.heightCheck = true;
		player.collision.height = 0.1f;
		
		AstarPath.active.Scan();
	}
	
	public void updateGraph(GameObject obj) {
		GraphUpdateObject graphObj = new GraphUpdateObject (obj.GetComponent<Collider> ().bounds);
		graphObj.updatePhysics = true;
		AstarPath.active.UpdateGraphs (graphObj);
		findPath ();
	}

	public void findPath () {
		foreach (GameObject tmpPlayer in players){
			if (tmpPlayer.GetComponent<Player_ID>().playerTeam == Player_Board.e_player.PLAYER1)
				tmpPlayer.GetComponent<Seeker> ().StartPath (spawn1.position, end1.position, OnPathComplete1);
			else
				tmpPlayer.GetComponent<Seeker> ().StartPath (spawn2.position, end2.position, OnPathComplete2);
		}
	}

	public Path getBoardPath(Player_Board.e_player p){
		if (p == Player_Board.e_player.PLAYER1)
			return path2;
		return path1;
	}

	void OnPathComplete1(Path p) {
		Debug.Log ("OnPathComplete1");
		if (!p.error)
			path1 = p;
		else
			Debug.LogError ("OnPathComplete1 : " + p.error);
	}

	void OnPathComplete2(Path p) {
		Debug.Log ("OnPathComplete2");
		if (!p.error)
			path2 = p;
		else
			Debug.LogError ("OnPathComplete2 : " + p.error);
	}

}
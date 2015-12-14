using UnityEngine;
using System.Collections;
using Pathfinding;

public class GridController : MonoBehaviour {

	private AstarData data;
	private GridGraph ggPlayer1;
	private GridGraph ggPlayer2;

	void Start() {
		data = AstarPath.active.astarData;
	}

	public void InitGridController (int xPlayer, int yPlayer, Vector3 centerplayer1, 
	                                Vector3 centerPlayer2, GameObject spawnMobPlayer1, 
	                                GameObject spawnMobPlayer2, Player_Board.e_player player) {
		createGrah (ggPlayer1, xPlayer * 2 + 5, yPlayer * 2 + 5, centerplayer1);
		createGrah (ggPlayer2, xPlayer * 2 + 5, yPlayer * 2 + 5, centerPlayer2);
	}

	 void createGrah (GridGraph player, int x, int y, Vector3 center) {
		player = data.AddGraph(typeof(GridGraph)) as GridGraph;
		player.width = x;
		player.depth = y;
		player.center = center;
		player.nodeSize = 0.5f;
		player.UpdateSizeFromWidthDepth();
		player.neighbours = NumNeighbours.Four;
		player.collision.type = Pathfinding.ColliderType.Capsule;
		player.collision.diameter = 1f;
		player.collision.height = 1;
		player.collision.mask = LayerMask.GetMask("Ignore Raycast", "Border");
		player.collision.heightCheck = false;
		//player.collision.thickRaycast = true;
		AstarPath.active.Scan();
	}

	void updateMonstersPath() {
		GameObject [] objTab;
		objTab = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject obj in objTab) {
			obj.GetComponent<Monster> ().UpdatePath();
		}
	}

	public bool isTowerPutable (GameObject obj, Player_Board.e_player playerPlaying) {
		Transform spawn;
		Transform nexus;
		if (playerPlaying == Player_Board.e_player.PLAYER1) {
			spawn = GameObject.Find ("PLAYER1-MOBSPAWN").transform;
			nexus = GameObject.Find ("PLAYER1-NEXUS").transform;
		} else {
			spawn = GameObject.Find ("PLAYER2-MOBSPAWN").transform;
			nexus = GameObject.Find ("PLAYER2-NEXUS").transform;
		}
		GraphUpdateObject guo = new GraphUpdateObject (obj.GetComponent<Collider> ().bounds);
		GraphNode spawnNode = AstarPath.active.GetNearest (spawn.position).node;
		GraphNode nexusNode = AstarPath.active.GetNearest (nexus.position).node;
		if (GraphUpdateUtilities.UpdateGraphsNoBlock (guo, spawnNode, nexusNode, false)) {
			Debug.Log ("Tower is putable");
			return true;
		} else {
			Debug.Log ("Tower is NOT putable !!");
			return false;
		}
		
	}

	public void updateGraph(GameObject obj, bool addTower) {
		GraphUpdateObject graphObj = new GraphUpdateObject (obj.GetComponent<Collider> ().bounds);
		if (!addTower) {
			graphObj.modifyWalkability = true;
			graphObj.setWalkability = true;
		}
		AstarPath.active.UpdateGraphs (graphObj);
		updateMonstersPath ();
	}
}
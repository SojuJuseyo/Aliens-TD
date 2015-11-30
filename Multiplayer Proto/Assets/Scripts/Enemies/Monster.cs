using UnityEngine;
using System.Collections;
using Pathfinding;

public class Monster : MonoBehaviour {

	private CharacterController characterController;
	private int currentPoint;
	private Vector3 dir;
	private Path path;
	private GameObject g;
	private GridController gridController;
	private Player_Board.e_player player;

	private int health;
	private float speed;

	void Start() {
		health = 100;
		speed = 5;
		currentPoint = 0;
		characterController = GetComponent<CharacterController> ();
		g = GameObject.Find ("GridController");
		gridController = g.GetComponent<GridController>();
	}

	public void setPlayerBoard(Player_Board.e_player p){
		player = p;
	}

	void FixedUpdate() {
		Debug.Log ("ping1");
		if (path == null) {
			path = GameObject.Find ("GridController").GetComponent<GridController>().getBoardPath(player);
		}
		if (path != null) {
			if (currentPoint < path.vectorPath.Count) {
				Debug.Log ("ping2 ");
				transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentPoint], Time.deltaTime * 5);
				if (Vector3.Distance(transform.position, path.vectorPath[currentPoint]) < 0.5f)
					++currentPoint;
			}
			else {
				Debug.Log("Monster blocked");
			}
		}
	}

	public void SetPath (Path p) {
		path = p;
	}
}
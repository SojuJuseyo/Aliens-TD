using UnityEngine;
using System.Collections;
using Pathfinding;

public class Monster : MonoBehaviour {

	private CharacterController characterController;
	private int currentPoint;
	private Vector3 dir;
	private Path path;
	private GameObject g;
	private MonsterController monsterController;

	private int health;
	private float speed;

	void Start() {
		health = 100;
		speed = 5;
		currentPoint = 0;
		characterController = GetComponent<CharacterController> ();
		g = GameObject.Find ("MonsterController");
		monsterController = g.GetComponent<MonsterController>();
		path = monsterController.path;
	}

	void FixedUpdate() {
		if (path != null) {
			if (currentPoint < path.vectorPath.Count) {
				dir = (path.vectorPath[currentPoint] - transform.position).normalized * speed;
				characterController.SimpleMove(dir);
				if (Vector3.Distance(transform.position, path.vectorPath[currentPoint]) < 0.5f)
					++currentPoint;
			}
			else {
				Debug.Log("Monster blocked");
			}
		}
	}
}
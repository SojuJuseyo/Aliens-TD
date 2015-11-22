using UnityEngine;
using System.Collections;
using Pathfinding;

public class MonsterController : MonoBehaviour {
	
	private Seeker seeker;
	public Path path;

	public Transform spawn;
	public Transform end;
	
	void Start() {
		path = null;
		seeker = GetComponent<Seeker> ();
		seeker.StartPath(spawn.position, end.position, OnPathComplete);
	}
	
	public void OnPathComplete(Path p) {
		if (!p.error) {
			path = p;
		}
		else
			Debug.LogError (p.error);
	}
}

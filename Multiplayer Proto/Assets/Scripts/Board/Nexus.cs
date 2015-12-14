using UnityEngine;
using System.Collections;

public class Nexus : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyAir") {
			Debug.Log ("Monster destroy");
			Destroy(other.gameObject);
		}
	}
}

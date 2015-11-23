using UnityEngine;
using System.Collections;

public class EndReached : MonoBehaviour {
	
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Monster reached end");
		Destroy (other.gameObject);
	}
}

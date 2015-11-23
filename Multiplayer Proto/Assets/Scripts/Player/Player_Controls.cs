using UnityEngine;
using System.Collections;

public class Player_Controls : MonoBehaviour {
	
	//vitesses de déplacements de la cmaera :
	public float speedMove;
	public float speedZoom;
	//limite de la bordure (en 3D) de la camera :
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;
	
	// Update is called once per frame
	private void Update () {
		cameraControls ();
	}

	//update function, get les controles pour la camera
	private void cameraControls(){
		if (Input.GetAxis ("moveLeft") < 0 && transform.position.x > minX) {
			//gauche
			transform.Translate(Vector3.left * Time.deltaTime * speedMove, Space.World);
		}
		if (Input.GetAxis ("moveLeft") > 0 && transform.position.x < maxX) {
			//droite
			transform.Translate(Vector3.right * Time.deltaTime * speedMove, Space.World);
		}
		if (Input.GetAxis ("moveUp") < 0 && transform.position.z > minZ) {
			//bas
			transform.Translate(Vector3.back * Time.deltaTime * speedMove, Space.World);
		}
		if (Input.GetAxis ("moveUp") > 0 && transform.position.z < maxZ) {
			//haut
			transform.Translate(Vector3.forward * Time.deltaTime * speedMove, Space.World);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minY){
			transform.Translate(Vector3.down * Time.deltaTime * speedZoom, Space.World);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxY){
			transform.Translate(Vector3.up * Time.deltaTime * speedZoom, Space.World);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public GameObject menu;
    bool state;
	void Start () {
        state = false;
	}
	
	void Update () {
        menu.SetActive(state);
    }

    public void changeState()
    {
        state = !state;
    }
}

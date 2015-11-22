using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class NextIncome : MonoBehaviour {

    Text value;
    public float targetTime = 26.0f;

    void Start () {
        value = gameObject.GetComponent<Text>();
        value.text = "";
    }
	
	void Update () {
        targetTime -= Time.deltaTime;
        value.text = ((int)targetTime).ToString();
        if (targetTime <= 0) {
            targetTime = 26.0f;
        }
    }
}

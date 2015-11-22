using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimeElapsed : MonoBehaviour {

    Text value;
    DateTime start, end;
    void Start () {
        value = gameObject.GetComponent<Text>();
        value.text = "";
        start = end = DateTime.Now;
    }

    void Update () {
        end = DateTime.Now;
        TimeSpan timeElapsed = end - start;
        value.text = String.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsed.Hours, timeElapsed.Minutes, timeElapsed.Seconds);
    }
}

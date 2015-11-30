using UnityEngine;
using System.Collections;

public class PopUpMessages : MonoBehaviour {

	public GUIStyle style;

	private float DelayMessage = 3.0f;
	private bool ShoulIShowText = false;
	private bool IsTimeOver = true;
	private float TimePassed = 0.0f;
	private float CurrentTime = 0.0f;
	private string Message;
	
	public void DisplayMessage(string msg, float delay = 3.0f)
	{
		DelayMessage = delay;
		Message = msg;
		TimePassed = Time.time;
		IsTimeOver = true;
	}
	
	void Update()
	{
		CurrentTime = Time.time;
		if(IsTimeOver)
			ShoulIShowText = true;
		else
			ShoulIShowText = false;
		
		if (TimePassed != 0.0f)
		{
			if(CurrentTime - TimePassed > DelayMessage)
			{
				TimePassed = 0.0f;
				IsTimeOver = false;
			}
		}
	}
	
	void OnGUI()
	{
		if(ShoulIShowText)
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 10), Message, style);
	}
}

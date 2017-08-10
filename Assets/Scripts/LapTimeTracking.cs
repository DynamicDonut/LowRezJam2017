using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapTimeTracking : MonoBehaviour {
	public string timeDisplay;
	public float currTime, lastTime, bestTime;
	public int myLap;

	Transform myUI;
	int minutes, seconds, milliseconds;

	void Start(){
		myLap = 0;
		bestTime = 0f;
		lastTime = 9999f;
		myUI = GameObject.Find ("Canvas").transform;
	}

	// Update is called once per frame
	void Update () {
		if (myLap == 0) {
			currTime = Time.time;
		} else { 
			currTime = Time.time - lastTime;
		}
		minutes = Mathf.FloorToInt ((currTime / 60) % 60);
		seconds = Mathf.FloorToInt (currTime % 60);
		milliseconds = Mathf.FloorToInt ((currTime * 1000) % 10);

		timeDisplay = string.Format ("T:{0}:{1:00}.{2}", minutes, seconds, milliseconds);
		myUI.Find ("Current Time").GetComponent<TextMeshProUGUI> ().text = timeDisplay;
	}

	public void SaveLapTime(){
		TextMeshProUGUI myLapTime = myUI.Find ("Lap Time").GetComponent<TextMeshProUGUI> ();
		myLap++;

		lastTime = Time.time;
		if (bestTime == 0 || currTime < bestTime) {
			myLapTime.text = timeDisplay;
			bestTime = currTime;
			myLapTime.enabled = true;
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LapTimeTracking : MonoBehaviour {
	public string timeDisplay;
	public float currTime, lastTime, bestTime, setTime;
	public int myLap;

	TextMeshProUGUI myCurText, myCurText2;
	Transform myUI;
	int minutes, seconds, milliseconds;
	public float[] medalTimes;

	void Start(){
		myLap = 0;
		bestTime = 0f;
		lastTime = 9999f;
		myUI = GameObject.Find ("Canvas").transform;
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "Test") {
			myUI = GameObject.Find ("Canvas").transform;
			myCurText = myUI.Find ("Current Time").GetComponent<TextMeshProUGUI> ();
			myCurText2 = myUI.Find ("Current Time_B").GetComponent<TextMeshProUGUI> ();

			if (myLap == 0) {
				currTime = Time.time - setTime;
			} else { 
				currTime = Time.time - lastTime;
			}
			minutes = Mathf.FloorToInt ((currTime / 60) % 60);
			seconds = Mathf.FloorToInt (currTime % 60);
			milliseconds = Mathf.FloorToInt ((currTime * 1000) % 10);

			timeDisplay = string.Format ("{0}:{1:00}.{2}", minutes, seconds, milliseconds);
			myCurText.text = "T: " + timeDisplay;
			myCurText2.text = "T: " + timeDisplay;
		}
	}

	public void SaveLapTime(){
		TextMeshProUGUI myLapTime = myUI.Find ("Lap Time").GetComponent<TextMeshProUGUI> ();
		TextMeshProUGUI myLapTime2 = myUI.Find ("Lap Time_B").GetComponent<TextMeshProUGUI> ();
		myLap++;

		lastTime = Time.time;

		for (int i = 0; i < medalTimes.Length; i++) {
			if (currTime < medalTimes[i]){
				myUI.Find ("Medals").GetChild (i).GetComponent<Image> ().color = Color.white;
			}
		}

		if (bestTime == 0 || currTime < bestTime) {
			myLapTime.text = string.Format ("L: " + timeDisplay);
			myLapTime2.text = string.Format ("L: " + timeDisplay);
			bestTime = currTime;
			myLapTime.enabled = true;
			myLapTime2.enabled = true;
		}
	}
}

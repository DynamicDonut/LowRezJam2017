﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPointSystem : MonoBehaviour {
	// 0 = Start/Finish Line, 1 - XX = Checkpoints in Track

	public int currCPoint, currLap, numOfCPoints, maxLaps;
	GameObject CheckPointSys;
	bool finishedLap, finishedRace;

	void Start () {
		currCPoint = 0;
		currLap = 1;
		finishedLap = finishedRace = false;

		CheckPointSys = GameObject.Find ("CheckPointSystem");
		numOfCPoints = CheckPointSys.transform.childCount - 1;
		maxLaps = 3;
	}
	
	void Update () {
		if (!finishedRace) {
			if (currCPoint > numOfCPoints) {
				currCPoint = 0;
				currLap++;
			}

			if (currLap > maxLaps) {
				currCPoint = 0;
				currLap = 0;
				finishedRace = true;
				Debug.Log ("I've finished the race!");
			}
		}
	}

	void OnTriggerEnter (Collider col){
		int CheckPointNum = System.Int32.Parse(col.name.Substring (col.name.Length-1));
		if (col.tag == "CheckPoint") {
			if (CheckPointNum == currCPoint + 1 || CheckPointNum == currCPoint - numOfCPoints) {
				currCPoint++;
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPointSystem : MonoBehaviour {
	// 0 = Start/Finish Line, 1 - XX = Checkpoints in Track

	public int currCPoint, currLap, numOfCPoints, maxLaps;
	Transform CheckPointSys, myGM;
	bool finishedLap, finishedRace;
	Transform[] cPointChilds;

	void Start () {
		currCPoint = 0;
		currLap = 1;
		finishedLap = finishedRace = false;

		CheckPointSys = GameObject.Find ("CheckPointSystem").transform;
		myGM = GameObject.Find ("Game Manager").transform;
		numOfCPoints = CheckPointSys.childCount - 1;
		maxLaps = 3;

		cPointChilds = new Transform[CheckPointSys.childCount-1];
		for (int i = 0; i < CheckPointSys.childCount-1; i++) {
			if (CheckPointSys.GetChild (i).gameObject.name.Substring (0, 6) == "CPoint") {
				cPointChilds [i] = CheckPointSys.GetChild (i);
			}
		}
	}
	
	void Update () {
		if (!finishedRace) {
			if (currCPoint > numOfCPoints) {
				currCPoint = 0;
				currLap++;
				for (int i = 0; i < cPointChilds.Length; i++) {
					cPointChilds [i].Find("CPoint_Flag").GetComponent<LookAtCamera> ().cPointCleared = false;
				}
				myGM.GetComponent<LapTimeTracking> ().SaveLapTime ();
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

				if (col.name != "FinishLine_0") {
					col.transform.Find ("CPoint_Flag").GetComponent<LookAtCamera> ().cPointCleared = true;
				}
			}
		}
	}
}

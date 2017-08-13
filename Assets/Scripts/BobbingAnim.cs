using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnim : MonoBehaviour {
	Vector3 startPos;
	float bobSpd, bobMod;
	// Use this for initialization
	void Start () {
		startPos = this.transform.localPosition;

		bobSpd = 2.5f;
		bobMod = 0.35f;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = startPos + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time * bobSpd) * bobMod);
	}
}

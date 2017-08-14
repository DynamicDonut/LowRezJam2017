using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnim : MonoBehaviour {
	Vector3 startPos;
	public float bobSpd = 2.5f;
	public float bobMod = 0.35f;

	// Use this for initialization
	void Start () {
		startPos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "UI") {
			this.transform.localPosition = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * bobSpd) * bobMod, 0.0f);
		} else {
			this.transform.localPosition = startPos + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time * bobSpd) * bobMod);
		}
	}
}

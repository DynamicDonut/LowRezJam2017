using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	Camera cam;
	SpriteRenderer mySpRend;
	public Color myC;
	Transform myCircle, myCirMask, mainPlayer;
	CPointSystem myCheckPoints;
	int myCPoint; bool isNextCheckpoint;

	// Use this for initialization
	void Start () {
		mainPlayer = GameObject.Find ("Driver").transform;
		cam = mainPlayer.Find ("Main Camera").GetComponent<Camera> ();
		myCheckPoints = mainPlayer.Find ("ShipCollision").GetComponent<CPointSystem> ();
		myCPoint = System.Int32.Parse(transform.parent.name.Substring(transform.parent.name.Length-1));

		mySpRend = GetComponent<SpriteRenderer> ();
		myCircle = transform.Find ("Border");
		myCirMask = transform.Find ("SpriteMask");

		mySpRend.enabled = false;
		myCircle.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (cam.transform,Vector3.back);

		if (Vector3.Distance (transform.position, cam.transform.position) > 40f || !isNextCheckpoint) {
			mySpRend.enabled = false;
			myCircle.GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			mySpRend.enabled = true;
			myCircle.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (myCPoint == myCheckPoints.currCPoint + 1 || myCPoint == myCheckPoints.currCPoint - myCheckPoints.numOfCPoints) {
			isNextCheckpoint = true;
		} else {
			isNextCheckpoint = false;
		}

		if (Vector3.Distance (transform.position, cam.transform.position) > 10f && Vector3.Distance (transform.position, cam.transform.position) < 40f && isNextCheckpoint) {
			mySpRend.color = new Color (myC.r, myC.g, myC.b, Mathf.InverseLerp(40f, 10f, Vector3.Distance(transform.position, cam.transform.position)) * 0.87f);
			myCircle.GetComponent<SpriteRenderer> ().color = new Color (myC.r, myC.g, myC.b, Mathf.InverseLerp(40f, 10f, Vector3.Distance(transform.position, cam.transform.position)) * 0.87f);
		}
	}
}

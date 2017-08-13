using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrolling : MonoBehaviour {

	public GameObject myBGScroll,secondScroll;
	Vector3 orgPos; Quaternion orgRot;
	RacerMove myMovement;
	float resetVal, offset;
	public float scrollVal;
	bool isTransition;

	// Use this for initialization
	void Start () {
		myMovement = GetComponent<RacerMove> ();
		orgPos = myBGScroll.transform.position;
		resetVal = myBGScroll.GetComponent<SpriteRenderer> ().sprite.bounds.size.x / 2;
		offset = 72f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") > myMovement.dZone || Input.GetAxis ("Horizontal") < myMovement.dZone * -1f) {
			myBGScroll.transform.localPosition += Vector3.right * Input.GetAxis ("Horizontal") * -1 * scrollVal;
		}

		if (myBGScroll.transform.localPosition.x > resetVal - offset && !isTransition){
			isTransition = true;
			Debug.Log ("transition");
			secondScroll = GameObject.Instantiate (myBGScroll, this.transform);
		} else if (myBGScroll.transform.localPosition.x < resetVal * -1 + offset && !isTransition){
			isTransition = true;
			Debug.Log ("transition");
			//GameObject.Instantiate (myBGScroll, myBGScroll.transform.position, orgRot, transform.Find ("Main Camera"));
		}
		//orgPos + new Vector3((resetVal - offset) *-1f, 0f, 0f)
		//orgPos + new Vector3(resetVal + offset, 0f, 0f)
		//Debug.Log (myBGScroll.transform.rotation);
	}
}

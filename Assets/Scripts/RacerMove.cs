using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerMove : MonoBehaviour {
	public float turnSpd, spd, bobModifier, bobSpd;
	float currSpd, defDrag, currDrag;
	[Range(0.0f,0.5f)]
	public float dZone = 0.2f;
	Rigidbody2D rb;
	public Transform driverSprite;

	Vector3 startPos;
	public trackTerrain currTerrain;
	public bool isUnderwater;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
		driverSprite = transform.GetChild (0);
		defDrag = rb.drag;
		startPos = driverSprite.localPosition;
	}

	void FixedUpdate () {
		currSpd = spd * currTerrain.speedMod;
		currDrag = defDrag * currTerrain.terrainDrag;
		rb.drag = currDrag;

		Vector3 direction = transform.TransformPoint(driverSprite.localPosition) - new Vector3 (transform.position.x, transform.position.y, 20f);
		if (Input.GetAxis ("Horizontal") > dZone || Input.GetAxis ("Horizontal") < dZone * -1f) {
			//transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * turnSpd * Time.deltaTime);
			rb.MoveRotation (rb.rotation + Input.GetAxis ("Horizontal") * turnSpd * -1 * Time.deltaTime);
		}
		if (Input.GetAxis ("Vertical") > dZone || Input.GetAxis ("Vertical") < dZone * -1f) {
			//rb.MovePosition (rb.position + (Vector2) direction * Input.GetAxis ("Vertical") * spd  * Time.deltaTime);
			rb.AddForce((Vector2) direction.normalized * Input.GetAxis ("Vertical") * currSpd * Time.deltaTime);
		}

		//Bobbing Animation
		driverSprite.localPosition = startPos + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time * bobSpd) * bobModifier);

		//Diving Mechanic
		if (Input.GetButton ("Jump")) {
			isUnderwater = true;
			driverSprite.GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			isUnderwater = false;
			driverSprite.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		Debug.Log (startPos);
	}
}

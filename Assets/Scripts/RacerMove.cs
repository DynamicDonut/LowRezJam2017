using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerMove : MonoBehaviour {
	public float turnSpd, spd;
	[Range(0.0f,0.5f)]
	public float dZone = 0.2f;
	Rigidbody2D rb;
	public Transform driverSprite;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
		driverSprite = transform.GetChild (0);
	}

	void FixedUpdate () {
		Vector3 direction = transform.TransformPoint(driverSprite.localPosition) - new Vector3 (transform.position.x, transform.position.y, 20f);

		if (Input.GetAxis ("Horizontal") > dZone || Input.GetAxis ("Horizontal") < dZone * -1f) {
			//transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * turnSpd * Time.deltaTime);
			rb.MoveRotation (rb.rotation + Input.GetAxis ("Horizontal") * turnSpd * -1 * Time.deltaTime);
		}

		if (Input.GetAxis ("Vertical") > dZone || Input.GetAxis ("Vertical") < dZone * -1f) {
			//rb.MovePosition (rb.position + (Vector2) direction * Input.GetAxis ("Vertical") * spd  * Time.deltaTime);
			rb.AddForce((Vector2) direction.normalized * Input.GetAxis ("Vertical") * spd * Time.deltaTime);
		}
	}


}

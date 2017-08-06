using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerMove : MonoBehaviour {

	public float turnSpd, spd;
	[Range(0.0f,0.5f)]
	public float dZone = 0.2f;
	Rigidbody2D rb;
	public Transform driverSprite;
	public Color currTerrain;
	GameObject currTrack;
	Camera actualView;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
		driverSprite = transform.GetChild (0);
		currTrack = GameObject.Find ("RacingTrack");
		actualView = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 direction = transform.TransformPoint(driverSprite.localPosition) - new Vector3 (transform.position.x, transform.position.y, 20f);
		SpriteColorCheck ();

		if (Input.GetAxis ("Horizontal") > dZone || Input.GetAxis ("Horizontal") < dZone * -1f) {
			//transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * turnSpd * Time.deltaTime);
			rb.MoveRotation (rb.rotation + Input.GetAxis ("Horizontal") * turnSpd * -1 * Time.deltaTime);
		}

		if (Input.GetAxis ("Vertical") > dZone || Input.GetAxis ("Vertical") < dZone * -1f) {
			//rb.MovePosition (rb.position + (Vector2) direction * Input.GetAxis ("Vertical") * spd  * Time.deltaTime);
			rb.AddForce((Vector2) direction.normalized * Input.GetAxis ("Vertical") * spd * Time.deltaTime);
		}

		//Vector3 terrainCheck = new Vector3 (transform.position.x, transform.position.y, 20f) - transform.position;
		//Vector3 direction = transform.TransformPoint(driverSprite.localPosition) - new Vector3 (transform.position.x, transform.position.y, 20f);
		//Debug.DrawLine (new Vector3 (transform.position.x, transform.position.y, 20f), transform.position);
		Debug.DrawRay(transform.position, transform.forward*20f);
	}

	void SpriteColorCheck(){
		Ray terrainCheck = new Ray(transform.position, transform.forward);
		RaycastHit terrCheck;
		Texture2D trackPixels = currTrack.transform.Find ("TrackRef").GetComponent<SpriteRenderer> ().sprite.texture;

		if (Physics.Raycast (terrainCheck, out terrCheck, 20f)) {
			currTerrain = trackPixels.GetPixelBilinear (terrCheck.textureCoord2.x, terrCheck.textureCoord2.y);
			Debug.Log(currTerrain);
		}

//		Ray terrainCheck = actualView.ScreenPointToRay(new Vector3(Screen.width, Screen.height/2, 0f));
//		RaycastHit terrCheck;
//
//		if (Physics.Raycast (terrainCheck, out terrCheck, 20f)) {
//			Debug.Log(terrCheck.textureCoord);
//		}
//
//		Debug.DrawRay (terrainCheck.origin, terrainCheck.direction * 10f, Color.black);
	}
}

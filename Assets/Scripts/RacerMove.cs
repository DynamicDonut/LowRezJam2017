using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacerMove : MonoBehaviour {
	//Slide-y controls = Mass | 0.4, Linear Drag | 1
	//Stiffer controls = Mass | 0.1, Linear Drag | 3.5

	//boostMod - how fast the Jet Boost sends the driver
	//boostVelThresh - how slow does the force have to be to start the boost refill process
	//boostInterval - How long should it take for an individual bar to fill up

	public float turnSpd, spd, bobModifier, bobSpd;
	float currSpd, defDrag, currDrag;
	[Range(0.0f,0.5f)]
	public float dZone = 0.2f;
	Rigidbody2D rb;
	public Transform driverSprite;

	[Range(0.0f,0.5f)]
	public int boostLvl;
	public float boostInterval;
	float lastBoostIntvl, boostMod, boostVelThresh;

	Transform myGUI;
	Vector2 lastRBVel; Vector3 startPos;
	public trackTerrain currTerrain;
	bool isUnderwater;

	public Sprite[] boostBar;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
		driverSprite = transform.GetChild (0);
		defDrag = rb.drag;
		startPos = driverSprite.localPosition;

		boostMod = 2f; boostVelThresh = 0.15f;
		myGUI = GameObject.Find ("Canvas").transform;
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
		if (Input.GetKey(KeyCode.Z)) {
			//rb.MovePosition (rb.position + (Vector2) direction * Input.GetAxis ("Vertical") * spd  * Time.deltaTime);

			if (boostLvl > 0) {
				rb.AddForce ((Vector2) direction.normalized * spd/boostMod * boostLvl);
				boostLvl = 0;
				myGUI.Find ("BoostBar").GetComponent<Image>().sprite = boostBar [boostLvl];
			}
			rb.AddForce((Vector2) direction.normalized * currSpd * Time.deltaTime);
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

		//Water Jet Boost Mechanic
		//Values to change for WaterJetBoost - boostMod, boostVelThresh
		if (VelocityCheck (transform.position) <= boostVelThresh) {
			if (boostInterval < Time.time - lastBoostIntvl) {
				if (boostLvl < 5) {
					boostLvl++;
					myGUI.Find ("BoostBar").GetComponent<Image>().sprite = boostBar [boostLvl];
				}
				lastBoostIntvl = Time.time;
			}
		}
	}
		
	//Checks for how far you are from your last frame position - to see how quick you're moving
	float VelocityCheck (Vector2 rbV){
		float velCheck = Vector2.Distance (lastRBVel, rbV);
		lastRBVel = rbV;
		return velCheck;
	}
}

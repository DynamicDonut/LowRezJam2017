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
	public float dZone;
	Rigidbody2D rb;
	public Transform driverSprite;
	Animator myAnimator;

	[Range(0.0f,0.5f)]
	public float boostLvl;
	public float boostInterval;
	float lastBoostIntvl, boostMod, boostVelThresh;

	public AudioSource mySound;
	public AudioClip[] sfxClips;
	Transform myGUI, myGM;
	Vector2 lastRBVel; Vector3 startPos;
	public trackTerrain currTerrain;
	bool isAccelerating;

	public Sprite[] boostBar;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
		driverSprite = transform.GetChild (0);
		defDrag = rb.drag;
		startPos = driverSprite.localPosition;

		isAccelerating = false;
		mySound = driverSprite.GetComponent<AudioSource> ();
		myGM = GameObject.Find ("Game Manager").transform;
		dZone = myGM.GetComponent<MainMenuSetup> ().dZone;
		myAnimator = driverSprite.GetComponent<Animator> ();
		boostMod = 3.5f; boostVelThresh = 0.1f;
		myGUI = GameObject.Find ("Canvas").transform;

		mySound.clip = sfxClips [0];
		mySound.loop = true;
		mySound.Play ();
	}

	void Update(){
		myAnimator.SetFloat ("TurningVal", Input.GetAxis ("Horizontal"));
		if (Input.GetAxis ("Horizontal") < 0) {
			driverSprite.GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			driverSprite.GetComponent<SpriteRenderer> ().flipX = false;
		}
	}

	void FixedUpdate () {
		currSpd = spd * currTerrain.speedMod;
		currDrag = defDrag * currTerrain.terrainDrag;
		rb.drag = currDrag;

		Vector3 direction = transform.TransformPoint (driverSprite.localPosition) - new Vector3 (transform.position.x, transform.position.y, 20f);
		if (Input.GetAxis ("Horizontal") > dZone || Input.GetAxis ("Horizontal") < dZone * -1f) {
			//transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * turnSpd * Time.deltaTime);
			rb.MoveRotation (rb.rotation + Input.GetAxis ("Horizontal") * turnSpd * -1 * Time.deltaTime);
		}
		if (Input.GetButton("Accel")) {
			//rb.MovePosition (rb.position + (Vector2) direction * Input.GetAxis ("Vertical") * spd  * Time.deltaTime);
			if (!isAccelerating) {
				mySound.clip = sfxClips [0];
				mySound.loop = true;
				mySound.Play ();
			}

			isAccelerating = true;

//			if (VelocityCheck (transform.position) <= boostVelThresh) {
				if (boostInterval < Time.time - lastBoostIntvl) {
					if (boostLvl < 5) {
						boostLvl += .5f;
						myGUI.Find ("BoostBar").GetComponent<Image> ().sprite = boostBar [Mathf.FloorToInt (boostLvl)];
					}
					lastBoostIntvl = Time.time;
				}
//			}

			rb.AddForce ((Vector2)direction.normalized * currSpd * Time.deltaTime);
		} else {
			isAccelerating = false;

		}
			

		if (Input.GetButtonUp("Accel")) {
			if (boostLvl > 0) {
				mySound.volume = boostLvl * .1f + .4f;
				mySound.PlayOneShot (sfxClips [1]);
				rb.AddForce ((Vector2)direction.normalized * spd * 2 / boostMod * boostLvl);
				boostLvl = 0;
				myGUI.Find ("BoostBar").GetComponent<Image> ().sprite = boostBar [Mathf.FloorToInt (boostLvl)];
			}
		}




		//Bobbing Animation
		driverSprite.localPosition = startPos + new Vector3 (0.0f, 0.0f, Mathf.Sin (Time.time * bobSpd) * bobModifier);

//		//Diving Mechanic
//		if (Input.GetButton ("Jump")) {
//			isUnderwater = true;
//			driverSprite.GetComponent<SpriteRenderer> ().color = Color.red;
//		} else {
//			isUnderwater = false;
//			driverSprite.GetComponent<SpriteRenderer> ().color = Color.white;
//		}

		//Water Spray Particles

		//Water Jet Boost Mechanic
	}

//		//Values to change for WaterJetBoost - boostMod, boostVelThresh
//		if (!isAccelerating) {
////			if (VelocityCheck (transform.position) <= boostVelThresh) {
//			rb.angularDrag = 0f;
//		if (Input.GetKeyUp (KeyCode.X)) {
////				if (boostInterval < Time.time - lastBoostIntvl) {
//					if (boostLvl < 5) {
//						boostLvl += .5f;
//						myGUI.Find ("BoostBar").GetComponent<Image> ().sprite = boostBar [Mathf.FloorToInt (boostLvl)];
//					}
//					//lastBoostIntvl = Time.time;
//			}
////			}
//		}
//		else {
//			rb.angularDrag = 5f;
////			if (Input.GetKeyUp (KeyCode.X)) {
////				if (boostInterval < Time.time - lastBoostIntvl) {
////					if (boostLvl < 3) {
////						boostLvl += .15f;
////						myGUI.Find ("BoostBar").GetComponent<Image> ().sprite = boostBar [Mathf.FloorToInt (boostLvl)];
////					}
////					//lastBoostIntvl = Time.time;
////				}
////			}
//	}
//	}

	void OnCollisionEnter2D(){
		mySound.PlayOneShot (sfxClips [4], 0.4f);
	}
		
	//Checks for how far you are from your last frame position - to see how quick you're moving
	float VelocityCheck (Vector2 rbV){
		float velCheck = Vector2.Distance (lastRBVel, rbV);
		lastRBVel = rbV;
		return velCheck;
	}
}

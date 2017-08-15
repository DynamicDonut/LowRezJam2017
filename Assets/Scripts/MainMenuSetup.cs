using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSetup : MonoBehaviour {
	Transform myCamera, myUI, mainMenu, infoMenu;
	bool isOnInfoScreen;
	int menuMoving;
	public float rotSpd = 2f;
	public float transitionSpd = 4f;
	[System.NonSerialized]
	public float dZone = 0.2f;

	// Use this for initialization
	void Start () {
		myCamera = GameObject.Find ("Main Camera").transform;
		myUI = GameObject.Find ("Canvas").transform;

		mainMenu = myUI.Find ("Start Menu");
		infoMenu = myUI.Find ("Info Menu");
		isOnInfoScreen = false;
		menuMoving = 0;
	}

	void Awake(){
		Object.DontDestroyOnLoad (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			myCamera.RotateAround (myCamera.position, Vector3.up, Time.deltaTime * rotSpd);

			if (!isOnInfoScreen) {
				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					myUI.GetComponent<AudioSource> ().Play ();
					menuMoving = -1;
				}
			} else {
				if (Input.GetKeyUp (KeyCode.UpArrow)) {
					myUI.GetComponent<AudioSource> ().Play ();
					menuMoving = 1;
				}
			}

			if (menuMoving != 0) {
				if (menuMoving == -1) {
					mainMenu.localPosition = Vector3.MoveTowards (mainMenu.localPosition, Vector3.up * 64f, transitionSpd);
					infoMenu.localPosition = Vector3.MoveTowards (infoMenu.localPosition, Vector3.zero, transitionSpd);

					if (infoMenu.localPosition == Vector3.zero) {
						menuMoving = 0;
						isOnInfoScreen = true;
					}
				} else if (menuMoving == 1) {
					mainMenu.localPosition = Vector3.MoveTowards (mainMenu.localPosition, Vector3.zero, transitionSpd);
					infoMenu.localPosition = Vector3.MoveTowards (infoMenu.localPosition, Vector3.down * 64f, transitionSpd);

					if (mainMenu.localPosition == Vector3.zero) {
						menuMoving = 0;
						isOnInfoScreen = false;
					}
				} 
			}

			if (Input.GetKeyUp (KeyCode.Z) && !isOnInfoScreen) {
				GetComponent<LapTimeTracking> ().setTime = Time.time;
				SceneManager.LoadScene ("Test");
				GetComponent<MusicManagement> ().musicChange (1);
			}
		}
	}
}

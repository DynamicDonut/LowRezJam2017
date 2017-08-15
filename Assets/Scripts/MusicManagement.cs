using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagement : MonoBehaviour {
	
	AudioSource myMusic;
	public bool isPlayingMusic;
	public AudioClip[] musicClips;

	// Use this for initialization
	void Start () {
		myMusic = GetComponent<AudioSource> ();
		musicChange (0);
	}
	
	// Update is called once per frame
//	void Update () {
//		musicChange ();
//	}
//
	public void musicChange(int m){
		if (m == 0) {
			myMusic.clip = musicClips [0];
			myMusic.Play ();
		} 
		if (m == 1) {
			myMusic.clip = musicClips [1];
			myMusic.Play ();
		}
	}
}

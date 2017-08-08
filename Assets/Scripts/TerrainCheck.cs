using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheck : MonoBehaviour {
	public Color currTerrain;
	GameObject currTrack;
	Texture2D trackPixels;

	void Start () {
		currTrack = GameObject.Find ("RacingTrack");
		trackPixels = currTrack.transform.Find ("TrackRef").GetComponent<SpriteRenderer> ().sprite.texture;
	}
	
	void Update () {
		Ray terrainCheck = new Ray(transform.position, transform.forward);
		RaycastHit terrCheck;

		if (Physics.Raycast (terrainCheck, out terrCheck, 20f)) {
			currTerrain = trackPixels.GetPixelBilinear (terrCheck.textureCoord2.x, terrCheck.textureCoord2.y);
		}
	}
}

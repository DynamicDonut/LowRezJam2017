using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class trackTerrain {
	public string terrainName;
	public Color terrainCol;
	public float terrainDrag;
	public float speedMod;


	public trackTerrain (string tN, Color tC, float tD, float sM){
		terrainName = tN;
		terrainCol = tC;
		terrainDrag = tD;
		speedMod = sM;
	}

	public trackTerrain() {
		terrainName = "Default";
		terrainCol = Color.blue;
		terrainDrag = 1f;
		speedMod = 1f;
	}
}

public class TerrainCheck : MonoBehaviour {
	public Color currTerrain;
	public trackTerrain[] terrainTypeList;
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
			for (int i = 0; i < terrainTypeList.Length; i++) {
				if (terrainTypeList [i].terrainCol == currTerrain) {
					GetComponent<RacerMove> ().currTerrain = terrainTypeList [i];
				}
			}
		}
	}
}

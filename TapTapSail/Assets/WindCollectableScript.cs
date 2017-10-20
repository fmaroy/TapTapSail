using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCollectableScript : MonoBehaviour {

	public float WindDirAngle;
	public buildMesh meshData;
	public GameController gameData;


	// Use this for initialization
	void Start () {
		meshData = FindObjectOfType<buildMesh> ();
		gameData= FindObjectOfType<GameController> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 currentPosition = transform.position;
		transform.position = currentPosition + new Vector3 (0.0f, 0.0f, - gameData.windPace * Time.deltaTime);

		if (transform.position.z < meshData.player.transform.position.z + meshData.worldZBoundary[0]) {
			Destroy (this.gameObject);
		}
	}
}


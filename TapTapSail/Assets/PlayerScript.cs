using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float pace = 1.0f;
	public float windDir = 180f;
	public float playerDir;
	public Camera cam;
	bool starboard = true;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (starboard) {
				starboard = false;
			} else {
				starboard = true;
			}
		}
		if (starboard) {
			playerDir = windDir - 180f + 45f;
		} else {
			playerDir = windDir - 180f - 45f;
		}
		float forwardPos = transform.position.z + Time.deltaTime * pace * Mathf.Cos(playerDir * Mathf.Deg2Rad);
		float sidePos = transform.position.x + Time.deltaTime * pace * Mathf.Sin(playerDir * Mathf.Deg2Rad);
		Vector3 playerPosition = new Vector3 (sidePos, 0.0f, forwardPos);

		transform.position = playerPosition;
		transform.forward = new Vector3 (Mathf.Sin(playerDir * Mathf.Deg2Rad), 0f, Mathf.Cos(playerDir * Mathf.Deg2Rad));
	}
}

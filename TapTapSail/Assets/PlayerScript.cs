using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float pace = 1.0f;
	public Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		float forwardPos = transform.position.z + Time.deltaTime * pace;
		Vector3 playerPosition = new Vector3 (0.0f, 0.0f, forwardPos);
		transform.position = playerPosition;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	float thrustFactor = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float MotionInZ = Input.GetAxis ("Vertical") * thrustFactor;
		Vector3 currentPosition = this.transform.position;
		this.transform.position = currentPosition + new Vector3 (0f, 0f, MotionInZ);
	}
}

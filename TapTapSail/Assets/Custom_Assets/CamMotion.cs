using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotion : MonoBehaviour {
	public float camVelocity = 1;
	public float camXPosition = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		camXPosition = camXPosition + Time.deltaTime * camVelocity;
		this.transform.position = new Vector3 (0f, this.transform.position.y, camXPosition);
	}
}

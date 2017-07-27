using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour {
	
	public GameObject player;
	public Vector3 initPlayerPos;
	public Vector3 initCamPos;
	public Vector3 DeltaPos;
	// Use this for initialization
	void Start () {
		initPlayerPos = player.transform.position;
		initCamPos = this.transform.position;
		DeltaPos = initCamPos - initPlayerPos;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (player.transform.position.z);

		this.transform.position = initCamPos + new Vector3(0, 0, player.transform.position.z) - new Vector3(0, 0, initPlayerPos.z);
	}
}

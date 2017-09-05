﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindModifierHandler : MonoBehaviour {
	public GameObject WindArrowContainer;
	public GameObject player;
	public buildMesh environement;
	public GameObject WindArrowBasicPrefab;
	public List<GameObject> WindArrowsList;
	public float releasePace = 1f;
	public float currentReleasePace = 1f;
	public float currentTimer = 0f;
	public float currentPlayerPosZ;
	public Vector2 WindArrowPosDelta = new Vector2 (5f, 10f);


	// Use this for initialization
	void Start () {
		currentReleasePace = releasePace;
	}

	public void releaseWind(GameObject obj, float angle, float xPos)
	{
		Debug.Log ("Instantiating");
		GameObject temp = Instantiate(obj, new Vector3(xPos, 0, currentPlayerPosZ + environement.worldZBoundary[1] - WindArrowPosDelta[1]), Quaternion.identity);
		temp.transform.SetParent (this.transform);
		temp.transform.eulerAngles = new Vector3 (90f, angle, 0);
		WindArrowsList.Add (temp);
	}

	// Update is called once per frame
	void Update () {
		currentPlayerPosZ = player.transform.position.z;
		currentTimer = currentTimer + Time.deltaTime;
		if (currentTimer > releasePace) {
			currentTimer = 0f;
			float positionInX = Random.Range(WindArrowPosDelta[0] - (environement.waterwidth / 2), -1 * WindArrowPosDelta[0] + (environement.waterwidth / 2));
			releaseWind (WindArrowBasicPrefab, Random.Range(-10f, 10f), positionInX);
		}
	}
}

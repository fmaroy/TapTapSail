using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject player;
	public float currentPace = 5.0f;
	public float paceIncreaseRatio = 1f;

	// Use this for initialization
	void Start () {
		player.GetComponent<PlayerScript> ().pace = currentPace;
	}
	
	// Update is called once per frame
	void Update () {
		currentPace = currentPace + Time.deltaTime * paceIncreaseRatio / 50f;
		player.GetComponent<PlayerScript> ().pace = currentPace;
	}
}

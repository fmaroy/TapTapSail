using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject player;
	public float currentPace = 5.0f;
	public float paceIncreaseRatio = 1f;
	public float currentSmallerWaterWidth = 0.5f;


	public Vector2 shoreAmplitudeRange = new Vector2 (0.02f, 0.1f);
	public float shoreVariabilityAmplitudeTarget;
	public float shoreVaraibilityPace = 5.0f;
	public Vector2 shoreFrequencyRange = new Vector2 (5f, 0.5f);
	public float shoreVaraibilityFrequencyTarget;

	// Use this for initialization
	void Start () {
		player.GetComponent<PlayerScript> ().pace = currentPace;
		shoreVariabilityAmplitudeTarget = shoreAmplitudeRange [0];
		shoreVaraibilityFrequencyTarget = shoreFrequencyRange [0];
	}
	
	// Update is called once per frame
	void Update () {
		currentPace = currentPace + Time.deltaTime * paceIncreaseRatio / 50f;
		player.GetComponent<PlayerScript> ().pace = currentPace;

	}
}

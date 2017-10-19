using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject player;
	public GameObject ennemy;
	public float currentPace = 5.0f;
	public float paceIncreaseRatio = 0f;
	public float currentSmallerWaterWidth = 0.5f;

	public float currentEnnemyPosZ = 0f;
	public float currentEnnemyPace = 1.0f;
	public float currentEnnemyPaceLinear = 1f;
	public float currentEnnemyPaceExponential = 1f;
	public float ennemyPaceLinearModifier = 0.01f;
	//public float ennemyPaceExponenetialModifier = 0.01f;


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

		currentEnnemyPosZ = ennemy.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		currentPace = currentPace + Time.deltaTime * paceIncreaseRatio / 50f;
		player.GetComponent<PlayerScript> ().pace = currentPace;

		//Ennemy pace handler
		Vector3 currentEnnemyPosition = ennemy.transform.position;

		currentEnnemyPaceLinear = currentEnnemyPaceLinear + Time.deltaTime * currentEnnemyPaceExponential;
		currentEnnemyPace = currentEnnemyPace + currentEnnemyPaceLinear * Time.deltaTime ;
		currentEnnemyPosZ = currentEnnemyPosition.z + Time.deltaTime * currentEnnemyPace ;

		ennemy.transform.position = new Vector3 (0f, 0f, currentEnnemyPosZ);

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public GameController gameControls; 

	public Vector2 currentShorePosition; //define the position on left shore and the right shore between 0 to 1 onf the total width
	//public Vector2 targetXBoundary = new Vector2(); // water boundary target
	public Vector2 variationAmount = new Vector2 (0.2f, 0.2f);

	private float inpNoise = 0.0f;
	public Vector2 zNoisePosition = new Vector2 (0.01f, 0.01f);
	public float testZoffset = 0.01f;

	//randomization of shored motions
	public float changeRightShoreTimer = 5.0f;
	public float changeLeftShoreTimer = 2.0f;
	public float currentRightShoreTimer = 5.0f;
	public float currentLeftShoreTimer = 2.0f;
	private float righttimer = 0.0f;
	private float lefttimer = 0.0f;


	//sinus motion controller
	Vector3 startPos;
	public float rightamplitude = 0.1f;
	public float rightperiod = 5f;
	public float leftamplitude = 0.1f;
	public float leftperiod = 5f;
	public float rightShoreXOffset = 0f;
	public float leftShoreXOffset = 0f;

	private float currentrightamplitude = 0.0f;
	private float currentrightperiode = 0.0f;
	public float currentleftamplitude = 0.0f;
	private float currentleftperiode = 0.0f;

	private float oldrightamplitude = 0.0f;
	private float oldrightperiode = 0.0f;
	public float oldleftamplitude = 0.0f;
	private float oldleftperiode = 0.0f;

	// Use this for initialization
	void Start () {

		righttimer = 0f;
		righttimer = 0f;

		rightShoreXOffset = 0f;
		leftShoreXOffset = 0f;

		rightamplitude = gameControls.shoreAmplitudeRange[0];
		rightperiod = gameControls.shoreFrequencyRange[0];
		leftamplitude = gameControls.shoreAmplitudeRange[0];
		leftperiod = gameControls.shoreFrequencyRange[0];

		currentrightamplitude = rightamplitude;
		currentrightperiode = rightperiod;
		currentleftamplitude = leftamplitude;
		currentleftperiode = leftperiod;

		oldrightamplitude = currentrightamplitude;
		oldrightperiode = currentrightperiode;
		oldleftamplitude = currentleftamplitude;
		oldleftperiode = currentleftperiode;
	}

	public float GetShoreNoise(int n, Vector2 zoffset)
	{
		float noise; 
		if (n == 0) {
			noise = variationAmount [0] * (Mathf.PerlinNoise (zNoisePosition [0], 0f) - 0.5f);
		} else {
			noise = variationAmount[1] * (Mathf.PerlinNoise(0f, zNoisePosition[1]) - 0.5f);
		}
		return noise;
	}

	public void updateRightShoreSinusValues()
	{
		//modifiy Amplitude
		oldrightamplitude = currentrightamplitude;
		oldrightperiode = currentrightperiode;
		rightamplitude = Mathf.Lerp(gameControls.shoreAmplitudeRange[0], gameControls.shoreAmplitudeRange[1],Random.value);
		rightperiod = Mathf.Lerp(gameControls.shoreFrequencyRange[0], gameControls.shoreFrequencyRange[1],Random.value);
		//modify period
		//rightperiod
	}

	public void updateLeftShoreSinusValues()
	{
		//modifiy Amplitude
		oldleftamplitude = currentleftamplitude;
		oldleftperiode = currentleftperiode;
		leftamplitude = Mathf.Lerp(gameControls.shoreAmplitudeRange[0], gameControls.shoreAmplitudeRange[1],Random.value);
		leftperiod = Mathf.Lerp(gameControls.shoreFrequencyRange[0], gameControls.shoreFrequencyRange[1],Random.value);
		//modify period
		//rightperiod
	}

	// Update is called once per frame
	void Update () {

		righttimer = righttimer + Time.deltaTime;
		lefttimer = lefttimer + Time.deltaTime;

		if (righttimer > currentRightShoreTimer) {
			updateRightShoreSinusValues ();
			currentRightShoreTimer = changeRightShoreTimer + (Random.value - 0.5f) * 1f;
			righttimer = 0f;
		} else {
			currentrightamplitude = Mathf.Lerp(oldrightamplitude, rightamplitude, righttimer / currentRightShoreTimer);
			currentrightperiode = Mathf.Lerp(oldrightperiode, rightperiod, righttimer / currentRightShoreTimer);
		}
		if (lefttimer > currentLeftShoreTimer)
		{
			updateLeftShoreSinusValues ();
			currentLeftShoreTimer = changeLeftShoreTimer + (Random.value - 0.5f) * 2f;
			lefttimer = 0f;
		}
		else {
			currentleftamplitude = Mathf.Lerp(oldleftamplitude, leftamplitude, lefttimer / currentLeftShoreTimer);
			currentleftperiode = Mathf.Lerp(oldleftperiode, leftperiod, lefttimer / currentLeftShoreTimer);
		}

		//Debug.Log ("input : " + inpNoise + ", noiseTester(inpNoise) : " + noiseTester(inpNoise));
		inpNoise = inpNoise + 0.01f;
		float lefttheta = Time.timeSinceLevelLoad / currentleftperiode;
		float leftdistance = currentleftamplitude * Mathf.Sin(lefttheta);
		float righttheta = Time.timeSinceLevelLoad / currentrightperiode;
		float rightdistance = currentrightamplitude * Mathf.Sin(righttheta);
		rightShoreXOffset = rightdistance;
		leftShoreXOffset = leftdistance;

	}
}

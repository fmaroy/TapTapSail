using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public Vector2 currentShorePosition; //define the position on left shore and the right shore between 0 to 1 onf the total width
	//public Vector2 targetXBoundary = new Vector2(); // water boundary target
	public float variability = 0f;

	private float inpNoise = 0.0f;
	private Vector2 zNoisePosition = new Vector2 (0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}

	public Vector2 GetShoreWidth(Vector2 zoffset ,Vector2 variationAmount)
	{
		Vector2 shoreline = currentShorePosition;
		variationAmount = variationAmount * variability;
		float right = 0f;
		float left = 1f;
		zNoisePosition =  zNoisePosition + zoffset;
		shoreline = shoreline + new Vector2 ( 0.2f + variationAmount[0] *  Mathf.PerlinNoise(zNoisePosition[0], 0f), 0.8f + variationAmount[1] * Mathf.PerlinNoise(0f, zNoisePosition[1]));

		return shoreline;
	}

	public float noiseTester(float xinput)
	{
		float noiseoutput = Mathf.PerlinNoise (xinput, 0f);
		return noiseoutput;
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("input : " + inpNoise + ", noiseTester(inpNoise) : " + noiseTester(inpNoise));
		inpNoise = inpNoise + 0.01f;
	}
}

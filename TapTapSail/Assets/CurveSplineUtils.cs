using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveSplineUtils : MonoBehaviour {

	int curveTypeInt = 0;

	Vector3 startPos;

	public float amplitude = 10f;
	public float period = 5f;

	protected void Start() {
		startPos = transform.position;
	}

	protected void Update() {
		if (curveTypeInt == 0) {
			float theta = Time.timeSinceLevelLoad / period;
			float distance = amplitude * Mathf.Sin (theta);
			transform.position = startPos + Vector3.up * distance;
		}
	}
}

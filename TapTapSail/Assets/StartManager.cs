using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartManager : MonoBehaviour {

	public GameObject startupSequenceObj;
	public float unScaledtimer = 0f;
	public float startUnscaledTimer = 3f;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
		startupSequenceObj.SetActive (true);
		unScaledtimer = 0f;
		startupSequenceObj.GetComponent<PlayableDirector> ().Play();
	}
	
	// Update is called once per frame
	void Update () {
		unScaledtimer = unScaledtimer + Time.unscaledDeltaTime;
		if (unScaledtimer >= startUnscaledTimer)
		{
			startupSequenceObj.SetActive (false);
			Time.timeScale = 1f;
		}
	}
}

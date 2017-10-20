using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void reloadScene ()
	{
		SceneManager.LoadScene("PlayScene");
	}

	// Update is called once per frame
	void Update () {
		
	}
}

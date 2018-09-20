using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class StartManager : MonoBehaviour {

    public GameObject finishScreen;
    public GameObject startupSequenceObj;
    public GameObject gamePlayObj;
    public GameObject displayFinalScoreObj;
    public bool pause = true;
    public bool started = false;
    public bool running;
	public float unScaledtimer = 0f;
	public float startUnscaledTimer = 3f;

	// Use this for initialization
	void Start () {
        enablePause(true);
        pause = true;
        started = false;

        startupSequenceObj.SetActive (true);
		unScaledtimer = 0f;
		startupSequenceObj.GetComponent<PlayableDirector> ().Play();
	}

    public void displayFinishScreen()
    {
        Debug.Log("Displaying finish screen");
        pause = true;
        //enablePause(pause);
        finishScreen.SetActive(true);

        Debug.Log("Total Distance : " + Mathf.FloorToInt(gamePlayObj.GetComponent<DisplayHandler>().totalDistance));
        displayFinalScoreObj.GetComponent<TextMeshProUGUI>().SetText(Mathf.FloorToInt(gamePlayObj.GetComponent<DisplayHandler>().totalDistance).ToString());

    }

    public void reload()
    {
        FindObjectOfType<SceneManagerScript> ().reloadScene ();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void enablePause(bool pausing)
    {
        if (pausing)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

	// Update is called once per frame
	void Update () {
        if (!started)
        {
            if (Time.timeScale > 0f) { running = true; } else { running = false; }
            unScaledtimer = unScaledtimer + Time.unscaledDeltaTime;
            if (unScaledtimer >= startUnscaledTimer)
            {
                startupSequenceObj.SetActive(false);
                //enablePause(false);
                started = true;
                pause = false;

            }
        }
        enablePause(pause);

    }
}

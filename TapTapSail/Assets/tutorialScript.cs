using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class tutorialScript : MonoBehaviour {

    public StartManager startData;
    public GameObject startManager;
    public int tutorialStatusInt = 1;
    public bool isTutorialDisplayed = false;

    public GameObject tutoObj;
    public GameObject tutoText;


    public List<Sprite> tutoSpriteList;

	// Use this for initialization
	void Start () {
		
	}

    public void enableTutorial(int i)
    {
        GameObject playerObj = this.GetComponent<GameController>().player;

        if (i == 1)
        {
            if ((playerObj.transform.position.x < -10) || (playerObj.transform.position.x > 10))
            {
                Debug.Log("Enabling tutorial 1");

                tutoObj.SetActive(true);
                tutoObj.GetComponent<Image>().sprite = tutoSpriteList[0];
                tutoText.GetComponent<TextMeshProUGUI>().SetText("Tap to change boat direction");


                isTutorialDisplayed = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startData.started == true)
        {
            if (isTutorialDisplayed) // managing the pausing based on the status of bool isTutorialDisplayed
            {
                startManager.GetComponent<StartManager>().pause = true;

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Exiting Tutorial");
                    tutorialStatusInt = tutorialStatusInt + 1; // updating tutorial to handle the next one
                    tutoObj.SetActive(false);
                    isTutorialDisplayed = false;
                    startManager.GetComponent<StartManager>().pause = false;
                }
            }
            else
            {
                //startManager.GetComponent<StartManager>().pause = false;
            }
            enableTutorial(tutorialStatusInt);
        }
    }
}

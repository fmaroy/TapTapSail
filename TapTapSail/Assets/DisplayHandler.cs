using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHandler : MonoBehaviour {

	public float monsterDistance;
	public float totalDistance = 0;
	private float initPlayerPos = 0;

   
	public GameObject monsterDistanceObj;
	public GameObject totalDistanceObj;

	// Use this for initialization
	void Start () {
		initPlayerPos = this.GetComponent<GameController> ().player.transform.position.z;
	}

	// Update is called once per frame
	void Update () {
        
		monsterDistance = this.GetComponent<GameController> ().player.transform.position.z - this.GetComponent<GameController> ().ennemy.transform.position.z;
		totalDistance = this.GetComponent<GameController> ().player.transform.position.z - initPlayerPos;

		monsterDistanceObj.GetComponent<TextMeshProUGUI> ().SetText ("Monster : " + Mathf.RoundToInt(monsterDistance-3f) + " m");
		totalDistanceObj.GetComponent<TextMeshProUGUI> ().SetText ("Total : " + Mathf.RoundToInt(totalDistance) + " m");
	}
}
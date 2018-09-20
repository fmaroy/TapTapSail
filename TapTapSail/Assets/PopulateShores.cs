using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateShores : MonoBehaviour {

    public buildMesh meshBuilderObj;
    public float density = 0.0001f;
    public float zOffset = 80f;
    //public bool isStarted = false;
    public StartManager startHandler;
    public List<GameObject> shoreObjectCatalog = new List<GameObject>();

    public List<GameObject> RightShoreObjectsList = new List<GameObject>();
    public List<GameObject> LeftShoreObjectsList = new List<GameObject>();

    public GameObject addShoreObject (bool isLeftShore)
    {
        GameObject currentPrefab;
        // Choose prefab
        int i = Mathf.RoundToInt(Random.Range(0f, 1f) * shoreObjectCatalog.Count);
        Debug.Log("shoreObjectCatalog chosen Id : " + (i-1));
        currentPrefab = shoreObjectCatalog[i-1];
        Debug.Log(currentPrefab.name);
        Vector3 position = new Vector3 (0,0,0);
        if (isLeftShore)
        {
            position = new Vector3(Random.Range (-1*meshBuilderObj.width /2,meshBuilderObj.currentShorePosition[0]), 2f, meshBuilderObj.player.transform.position.z + zOffset);
        }
        else
        {
            position = new Vector3(Random.Range(meshBuilderObj.currentShorePosition[1], meshBuilderObj.width / 2), 2f, meshBuilderObj.player.transform.position.z + zOffset);
        }
        Vector3 prefabRotation = currentPrefab.transform.eulerAngles;
        GameObject shoreObj = Instantiate(currentPrefab,position, Quaternion.identity);
        shoreObj.transform.eulerAngles = prefabRotation;

        return shoreObj;
    }

    // Use this for initialization
    void Start () {
        meshBuilderObj = this.GetComponent<buildMesh>();
        startHandler = FindObjectsOfType<StartManager>()[0];
    }

    public void cleanupShoreObjects()
    {
        for (int i = 0; i < RightShoreObjectsList.Count; i++)
        {
            if (RightShoreObjectsList[i].transform.position.z < meshBuilderObj.player.transform.position.z + meshBuilderObj.worldZBoundary[0])
                {
                    Destroy(RightShoreObjectsList[i]);
                    RightShoreObjectsList.RemoveAt(i);
                }
        }
        for (int i = 0; i < LeftShoreObjectsList.Count; i++)
        {
            if (LeftShoreObjectsList[i].transform.position.z < meshBuilderObj.player.transform.position.z + meshBuilderObj.worldZBoundary[0])
            {
                Destroy(LeftShoreObjectsList[i]);
                LeftShoreObjectsList.RemoveAt(i);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (startHandler.running)
        {
            // add object on left shore
            if (Random.Range(0f, 1f) < density) { LeftShoreObjectsList.Add(addShoreObject(true)); };
            // add object on right shore
            if (Random.Range(0f, 1f) < density) { RightShoreObjectsList.Add(addShoreObject(false)); };
        }
        cleanupShoreObjects();
    }
}

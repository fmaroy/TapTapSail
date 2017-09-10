using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMesh : MonoBehaviour {

	public GameObject player;
	public Vector2 worldZBoundary = new Vector2(); // Z offset of the player from which the world is getting created or destroyed

	public List<Material> floorMaterial = new List<Material>();
	public List<Material> underwaterMaterial = new List<Material>();
	public List<Material> cliffMaterial = new List<Material>();
	public List<Material> sandMaterial = new List<Material>();
	public int currentAeraCategory = 0;

	public float width = 40f;
	public Vector2 verticalBoundaries; // top and bottom boundaries relative to player
	public float height = 1f;
	public float meshSize = 1f;
	public float RandomizeFloorFactor = 0.3f;
	//Animation data
	public float startheight = 10;
	public float RandomizeHeightFactor = 3f;
	public float fallTime = 1;
	public int stripInt;
	private Vector2[] oldPointArray;
	private Vector2[] newPointArray;
	public Vector2 globalShorePosition;
	public Vector2 currentShorePosition; //define the position on left shore and the right shore between 0 to 1 onf the total width
	public Vector3 meshNumbPerSection = new Vector3();
	public Vector3 meshSizePerSection = new Vector3();
	public float variability = 0f;

	public float seaDepth = -2f;
	//public List<GameObject[]> PlanesList = new List<GameObject[]>();
	private GameObject currentPlane;
	public List<List<GameObject>> PlanesList = new List<List<GameObject>>();
	public List<GameObject[]> WaterPlanesList = new List<GameObject[]> ();
	public float WaterTileSize = 5f;
	public float waterwidth = 30f;
	public GameObject WaterContainerPrefab;
	public GameObject WaterTilePrefab;
	public int stripWaterInt;

	// Use this for initialization
	void Start () 
	{
		stripInt = 0;
		stripWaterInt = 0;
		globalShorePosition = new Vector2 (-1 * waterwidth /2, waterwidth / 2);
		newPointArray = CreatePointArray (width, meshSize, RandomizeFloorFactor);
		globalShorePosition = new Vector2(0.2f,0.8f);
		meshNumbPerSection = new Vector3 (Mathf.RoundToInt(width * (globalShorePosition[0])/meshSize), Mathf.RoundToInt(width * (globalShorePosition[1] - globalShorePosition[0])/meshSize), Mathf.RoundToInt(width * (1.0f - globalShorePosition[1])/meshSize));
		Debug.Log ("meshNumbPerSection : " + meshNumbPerSection);
		meshSizePerSection = new Vector3 (Mathf.RoundToInt(width * meshNumbPerSection[0]), Mathf.RoundToInt(width / meshNumbPerSection[1]) , Mathf.RoundToInt(width / meshNumbPerSection[2]));
		meshSizePerSection = new Vector3 (meshSize, meshSize, meshSize);
		meshSizePerSection = new Vector3 (width * globalShorePosition[0]/meshNumbPerSection[0], width * (globalShorePosition[1]-globalShorePosition[0])/meshNumbPerSection[1], width * (1.0f - globalShorePosition[1])/meshNumbPerSection[2]);
		Debug.Log ("meshSizePerSection : " + meshSizePerSection);
		initWorld ();
	}

	public float calculateHeight(float xPos)
	{
		float depth = 0f;
		float dimlessXPos = 0.5f + (xPos / width) ;

		//currentShorePosition = this.GetComponent<WorldManager>().GetShoreWidth(new Vector2 (0.01f, 0.01f), new Vector2 (0.1f, 0.1f));

		if ((currentShorePosition [0] < dimlessXPos) && (dimlessXPos < currentShorePosition [1])) {
			depth = seaDepth;
		} else {
			depth = 0f;
		}
		return depth;
	}

	public Vector3 updateMeshSizePerSection(Vector2 shore)
	{
		Vector3 meshSizeVector = new Vector3 (meshSize, meshSize, meshSize);

		meshSizeVector = new Vector3 (width * shore[0]/meshNumbPerSection[0], width * (shore[1]-shore[0])/meshNumbPerSection[1], width * (1.0f - shore[1])/meshNumbPerSection[2]);
		//Debug.Log ("meshSizePerSection : " + meshSizePerSection);

		return meshSizeVector;
	}

	public Vector2[] CreatePointArray (float widthLine, float step, float randomFactor)
	{
		int numbOfSteps = Mathf.FloorToInt (widthLine / step) + 1;
		float currentMeshSize = step;
		//Debug.Log (numbOfSteps);
		Vector2 [] lineDefinition = new Vector2[numbOfSteps];
		float sumedXPos = 0f;
		for (int i = 0; i< numbOfSteps; i++){
			if (i < meshNumbPerSection[0]){
				currentMeshSize = meshSizePerSection [0];
				//Debug.Log ("meshSize on right shore : " + currentMeshSize);
			}
			if ((i >= meshNumbPerSection[0])&&(i < (meshNumbPerSection[0] + meshNumbPerSection[1]))){
				currentMeshSize = meshSizePerSection [1];
				//Debug.Log ("meshSize on middle : " + currentMeshSize);
			}
			if (i >= (meshNumbPerSection[0]+meshNumbPerSection[1])){
				currentMeshSize = meshSizePerSection [2];
				//Debug.Log ("meshSize on right shore : " + currentMeshSize);
			}
			//Debug.Log("Create : i " + i + ", step : " + step);
			//float currentxPos = i * step - width/2;
			float currentxPos = sumedXPos - width /2;

			float currentheight = calculateHeight(currentxPos);
			//Debug.Log ("xpos: " + currentxPos +", depth : "+currentheight);
			lineDefinition[i] = new Vector2 (currentxPos, currentheight + Random.Range((-1*randomFactor/2),(1*randomFactor/2)));
			sumedXPos = sumedXPos + currentMeshSize;
		}
		return lineDefinition;
	}

	public Material getFloorMaterial(Vector3 [] v)
	{
		Material localmat = floorMaterial [0];
		int aeraType = 0;
		if ((Mathf.Abs(v[0][1] - v[1][1])) > 0.5f)
		{
			// Area is a cliff
			aeraType = 1;
		}
		if ((v[0][1] < -0.5f) && (v[1][1] < -0.5f))
		{
			// Area is underwater
			//Debug.Log("test");
			aeraType = 2;
		}
		switch (aeraType) {
		case 0:
			//Debug.Log ("floor : " + v [0] [1] + ", " + v [1] [1]);
			localmat = floorMaterial [0];
			break;
		case 1:
			//Debug.Log ("cliff : " + v [0] [1] + ", " + v [1] [1]);
			localmat = cliffMaterial [0];
			break;
		case 2: 
			//Debug.Log ("underwater : " + v [0] [1] + ", " + v [1] [1]);
			localmat = underwaterMaterial [0];
			break;
		}
		return localmat;
	}

	IEnumerator AnimWaterTile (int id, GameObject parentObj, GameObject tileObj, Vector3 initPos, float time, float initialHeight, float randomHeight)
	{
		float startHeight = initialHeight + Random.Range((-1*randomHeight/2),(1*randomHeight/2));
		GameObject waterTile = Instantiate(tileObj, initPos, Quaternion.identity);
		//waterTile.transform.SetParent (parentObj);
		waterTile.transform.localPosition = initPos + new Vector3 (0f, initialHeight, 0f);
		float currentHeight = startHeight;
		float i = 0;
		float rate = 1 / time;
		while (i < 1){
			currentHeight = Mathf.Lerp (startHeight, 0, i);
			waterTile.transform.localPosition = initPos + new Vector3 (0f, currentHeight, 0);
			i += Time.deltaTime * rate;
			yield return 0;
		}
		waterTile.transform.localPosition = initPos;
		WaterPlanesList [WaterPlanesList.Count - 1] [id] = waterTile;
	}

	public void BuildWaterStripe (int stripID, float width, float tileWidth)
	{
		float leftWaterPos = -1 * Mathf.FloorToInt(width/tileWidth) / 2 * tileWidth;
		Debug.Log (leftWaterPos);
		GameObject[] objInStripe = new GameObject[Mathf.FloorToInt(width/tileWidth)];
		WaterPlanesList.Add (objInStripe);
		for (int i = 0; i < Mathf.FloorToInt(width/tileWidth); i ++){
			float zPos = stripID * tileWidth;
			StartCoroutine(AnimWaterTile (i, WaterContainerPrefab,WaterTilePrefab, new Vector3 (leftWaterPos + i * tileWidth, -0.5f, zPos), fallTime,startheight, RandomizeHeightFactor));
		}
	}

	IEnumerator AnimPlane (GameObject obj, float time, float initialHeight, float randomHeight)
	{
		float startHeight = initialHeight + Random.Range((-1*randomHeight/2),(1*randomHeight/2));
		obj.transform.localPosition = new Vector3 (0f, initialHeight, 0f);
		float currentHeight = startHeight;
		float i = 0;
		float rate = 1 / time;
		while (i < 1){
			currentHeight = Mathf.Lerp (startHeight, 0, i);
			obj.transform.localPosition = new Vector3 (0f, currentHeight, 0);
			i += Time.deltaTime * rate;
			yield return 0;
		}
		obj.transform.localPosition = new Vector3 (0f, 0, 0);
	}

	public void BuildStripe (int stripeID)
	{
		//Debug.Log ("Array length : " + oldPointArray.Length);

		List<GameObject> objInStripe = new List<GameObject>();

		currentPlane = null;
		for (int i = 0; i < oldPointArray.Length - 1; i ++) {
			// generate one quad per point
			Vector3[] coordList = new Vector3[] {
				new Vector3 (oldPointArray[i][0], oldPointArray[i][1], stripeID * meshSize),
				new Vector3 (oldPointArray[i+1][0], oldPointArray[i+1][1], stripeID * meshSize),
				new Vector3 (newPointArray[i+1][0], newPointArray[i+1][1], stripeID * meshSize + meshSize),
				new Vector3 (newPointArray[i][0], newPointArray[i][1], stripeID * meshSize + meshSize)
			};
			Material floorMat = getFloorMaterial(coordList);
			currentPlane = meshCreator.CreatePlaneByCoordinates ("world", coordList, floorMat);
			currentPlane.transform.SetParent (this.transform);
			float startHeight = startheight + Random.Range((-1*RandomizeHeightFactor/2),(1*RandomizeHeightFactor/2));


			StartCoroutine(AnimPlane(currentPlane, fallTime,startheight, RandomizeHeightFactor));
			//Debug.Log (currentPlane.name);
			objInStripe.Add (currentPlane);
		}
		PlanesList.Add (objInStripe);
	}

	public void initWorld ()
	{
		float playerposZ = player.transform.position.z;
		float stripPositionZ = 0;

		while (stripInt * meshSize < playerposZ + worldZBoundary[1]) {
			//Vector2 sinusShorePosition = new Vector2 (globalShorePosition[0] + this.GetComponent<WorldManager>().shoreXOffset, globalShorePosition[1] + this.GetComponent<WorldManager>().shoreXOffset);
			Vector2 shoreNoise = new Vector2 (this.GetComponent<WorldManager>().GetShoreNoise(0, new Vector2 (0.01f, 0.01f)), this.GetComponent<WorldManager>().GetShoreNoise(1, new Vector2 (0.01f, 0.01f)));

			currentShorePosition = globalShorePosition + shoreNoise;
			meshSizePerSection = updateMeshSizePerSection (currentShorePosition);
			oldPointArray = newPointArray;
			newPointArray = CreatePointArray (width, meshSize, RandomizeFloorFactor);
			BuildStripe (stripInt);
			//BuildWaterStripe (stripInt, meshSize, 30f);
			stripInt++;
		}

	}

	public void destroyWorldAgent()
	{
			foreach (GameObject obj in PlanesList[0]) {
				Destroy (obj);
			}
			PlanesList.Remove (PlanesList [0]);
	}

	// Update is called once per frame
	void Update () {
		float playerposZ = player.transform.position.z;
		WaterTilePrefab.transform.position = new Vector3 (0, 0, 45f) + new Vector3 (0, 0, playerposZ);
		Vector2 sinusShorePosition = new Vector2 (globalShorePosition[0] + this.GetComponent<WorldManager>().rightShoreXOffset, globalShorePosition[1] + this.GetComponent<WorldManager>().leftShoreXOffset);
		//
		Vector2 shoreNoise = new Vector2 (this.GetComponent<WorldManager>().GetShoreNoise(0, new Vector2 (0.01f, 0.01f)), this.GetComponent<WorldManager>().GetShoreNoise(1, new Vector2 (0.01f, 0.01f)));
		currentShorePosition = sinusShorePosition + shoreNoise;

		meshSizePerSection = updateMeshSizePerSection (currentShorePosition);
		//Debug.Log ("ShoreNoise : " + shoreNoise);
		//Debug.Log ("meshNumb per section: " + meshNumbPerSection);
		//Debug.Log ("current Shore Position : " + currentShorePosition);
		//Debug.Log ("meshSize per section : " + meshSizePerSection);
		//Debug.Log ("SumCheck : " + (meshSizePerSection[0]*meshNumbPerSection[0] + meshSizePerSection[1]*meshNumbPerSection[1] + meshSizePerSection[2]*meshNumbPerSection[2]));
		if (stripInt * meshSize < playerposZ + worldZBoundary[1]) {
			oldPointArray = newPointArray;
			newPointArray = CreatePointArray (width, meshSize, RandomizeFloorFactor);
			BuildStripe (stripInt);
			stripInt++;
		}
		//Debug.Log("current Size : " + meshSize * PlanesList.Count);
		if (meshSize * PlanesList.Count > worldZBoundary [1] - worldZBoundary [0]) {
			destroyWorldAgent ();
		}
	}
}

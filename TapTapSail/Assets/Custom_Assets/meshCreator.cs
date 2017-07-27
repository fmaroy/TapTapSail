using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class meshCreator{

	public static GameObject CreatePlane (float width, float height)
	{
		GameObject go = new GameObject("Plane");
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		Mesh m = new Mesh ();
		m.vertices = new Vector3[]
		{
			new Vector3 (0,0,0),
			new Vector3 (width,0,0),
			new Vector3 (width,height,0),
			new Vector3 (0,height,0)
		};
		m.uv = new Vector2[]
		{
			new Vector2 (0,0),
			new Vector2 (0,1),
			new Vector2 (1,1),
			new Vector2 (1,0)
		};

		m.triangles = new int[] {0,1,2,0,2,3};

		mf.mesh = m;
		m.RecalculateBounds ();
		m.RecalculateNormals ();
	
		return go;
			
	}

	public static GameObject CreateHorizPlane (float width, float height, bool collider, Material mat)
	{
		GameObject go = new GameObject("Plane");
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		Mesh m = new Mesh ();
		m.vertices = new Vector3[]
		{
			new Vector3 (0,0,0),
			new Vector3 (width,0,0),
			new Vector3 (width,0,height),
			new Vector3 (0,0,height)
		};
		m.uv = new Vector2[]
		{
			new Vector2 (0,0),
			new Vector2 (0,1),
			new Vector2 (1,1),
			new Vector2 (1,0)
		};

		m.triangles = new int[] {0,2,1,0,3,2};

		mf.mesh = m;

		if (collider) {
			(go.AddComponent (typeof(MeshCollider)) as MeshCollider).sharedMesh = m;
		}
		mr.material = mat;
		m.RecalculateBounds ();
		m.RecalculateNormals ();

		return go;
	}

	public static GameObject CreatePlaneByCoordinates (string name, Vector3 [] vert, Material mat)
	{
		GameObject go = new GameObject(name);
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		Mesh m = new Mesh ();
		m.vertices = new Vector3[]
		{
			new Vector3 (vert[0][0],vert[0][1],vert[0][2]),
			new Vector3 (vert[1][0],vert[1][1],vert[1][2]),
			new Vector3 (vert[2][0],vert[2][1],vert[2][2]),
			new Vector3 (vert[3][0],vert[3][1],vert[3][2])
		};

		m.uv = new Vector2[]
		{
			new Vector2 (0,0),
			new Vector2 (0,1),
			new Vector2 (1,1),
			new Vector2 (1,0)
		};

		m.triangles = new int[] {0,2,1,0,3,2};

		mf.mesh = m;

		/*if (collider) {
			(go.AddComponent (typeof(MeshCollider)) as MeshCollider).sharedMesh = m;
		}*/
		mr.material = mat;
		m.RecalculateBounds ();
		m.RecalculateNormals ();

		return go;

	}
	/// <summary>
	/// Creates the world stripe mesh.
	/// </summary>
	/// <returns>The world stripe mesh.</returns>
	/// <param name="oldMeshPos">Old mesh position. This is an array of Vector 2 representing the X (width) and Y (depth) position from right to left</param>
	/// <param name="newMeshPos">New mesh position.</param>
	/// <param name="mat">Mat.</param>
	/*public static GameObject CreateWorldStripeMesh (Vector2 [] oldMeshPos, Vector2 [] newMeshPos, Material mat)
	{
		GameObject go = new GameObject("Stripe");
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		Mesh m = new Mesh ();

		m.vertices = new Vector3[]();
		for (int i = 0; i <= oldMeshPos.GetLength -1 ; i++) {
				m.vertices.Add(oldMeshPos[i][0], oldMeshPos[i][1],  0);
				m.vertices.Add(oldMeshPos[i+1][0], oldMeshPos[i+1][1],  0);
				m.vertices.Add(newMeshPos[i+1][0], newMeshPos[i+1][1],  1);
				m.vertices.Add(newMeshPos[i][0], newMeshPos[i][1],  1);

		}
			
	}*/
}

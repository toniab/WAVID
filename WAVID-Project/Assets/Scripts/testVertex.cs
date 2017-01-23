using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testVertex : MonoBehaviour {
	Mesh mesh;
	Vector3[] vertices;
	public float freq=10f;
	public float amp = 1f;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		Debug.Log ("Amount of vertices: "+vertices.Length.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
		int i = 0;
		/*
		while (i < vertices.Length) {
			vertices[i] += Vector3.up * Time.deltaTime;
			i++;
		}
		*/
		vertices[20].y = amp * Mathf.Sin(freq*Time.time)*Time.deltaTime;
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
	}
}

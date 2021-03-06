﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class wavePattern : MonoBehaviour {
	public int x_size= 3;
	public int z_size=2;

	public int rowDist=2;
	public int columnDist=10;


	public float amp = 1;
	public float freq = 2;

	bool paused;

	Vector3 newpos = new Vector3();
	Vector2 currpos = new Vector2 ();
	float pointdist;
	float dist;
	int row = 0;
	int col = 0;
	int rand;
	//public int rand_i=0;
	//public int rand_f=5;

	public bool waveAlive;
	Vector2 travellingPoint;
	Vector2 oppositeGridPoint;
	Vector2 initialGridPoint;
	float initialDist;
	float finalDist;

	public float waveSpeed=4f;
	public int travelAngle = 45;
	public float circleRadius=10;
	public float influence = 0.01f;

	enum waveType{ circle, line}
	waveType currentWave;

	public float minimumDist=10f;
	public GameObject[] waterVertexes;

	// Use this for initialization
	void Start () {
		initialGridPoint = new Vector2();
		row = 0;
		col = 0;
		for (int i = 0; i <= z_size*x_size-1; i++) {			
			
			waterVertexes [i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			waterVertexes [i].transform.position = new Vector3 (columnDist * row, 0f, rowDist * col);
			row++;
			if (row >= x_size) {
				col++;
				row = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused) {
				paused = false;
				Time.timeScale = 1;
			} else {
				paused = true;
				Time.timeScale = 0;
			}
		}
	}
	void FixedUpdate(){
		if (waveAlive) {
			if (currentWave == waveType.line) {
				lineTravel();			
			}
			if(currentWave == waveType.circle){
				waveTravel();
			}
		}
		else{
			BasicVertexOscillation ();
			if (Input.GetKeyDown (KeyCode.R)) {
				//spawn new straight wave
				spawnNewLine();
			}	
			if (Input.GetKeyDown (KeyCode.C)) {
				//spawn new straight wave
				spawnNewWave();
			}
		}
	}

	void BasicVertexOscillation(){
		row = 0;
		col = 0;

		for (int i = 0; i <= z_size*x_size-1; i++) {
			//rand = Random.Range (rand_i,rand_f);
			newpos = waterVertexes [i].transform.position;
		
			newpos.y += amp*Mathf.Sin((col+5)*freq*Time.time)*Time.deltaTime;
			waterVertexes [i].transform.position = newpos;
			row++;
			if (row >= x_size) {
				col++;
				row = 0;
			}
		}
	}

	void spawnNewLine(){
		
		//choose initial spawn point and initialize it
		//first quadrant
		if(travelAngle >= 0 && travelAngle < 90){
			
			travellingPoint = new Vector2(columnDist*x_size+minimumDist*Mathf.Sin(travelAngle*Mathf.Deg2Rad),-minimumDist*Mathf.Cos(travelAngle*Mathf.Deg2Rad));
			oppositeGridPoint = new Vector2(-minimumDist*Mathf.Sin(travelAngle*Mathf.Deg2Rad),rowDist*z_size+minimumDist*Mathf.Cos(travelAngle*Mathf.Deg2Rad));
			initialGridPoint = travellingPoint;
			currentWave = waveType.line;
			waveAlive = true;
		}
		//second quadrant
		if(travelAngle >= 90 && travelAngle < 180){

			travellingPoint = new Vector2(columnDist*x_size+minimumDist*Mathf.Sin((180-travelAngle)*Mathf.Deg2Rad),rowDist*z_size+minimumDist*Mathf.Cos((180-travelAngle)*Mathf.Deg2Rad));
			oppositeGridPoint = new Vector2(-minimumDist*Mathf.Cos((travelAngle-90)*Mathf.Deg2Rad),-minimumDist*Mathf.Sin((travelAngle-90)*Mathf.Deg2Rad));
			initialGridPoint = travellingPoint;
			currentWave = waveType.line;
			waveAlive = true;
		}
		//third quadrant
		if(travelAngle >= 180 && travelAngle < 270){
			
			travellingPoint = new Vector2(-minimumDist*Mathf.Sin((travelAngle-180)*Mathf.Deg2Rad),rowDist*z_size+minimumDist*Mathf.Cos((travelAngle-180)*Mathf.Deg2Rad));
			oppositeGridPoint = new Vector2(columnDist*x_size+minimumDist*Mathf.Sin((travelAngle-180)*Mathf.Deg2Rad),rowDist*z_size-minimumDist*Mathf.Cos((travelAngle-180)*Mathf.Deg2Rad));
			initialGridPoint = travellingPoint;
			currentWave = waveType.line;
			waveAlive = true;
		}
		//forth quadrant
		if(travelAngle >= 270 && travelAngle <= 360){

			travellingPoint = new Vector2(-minimumDist*Mathf.Cos((travelAngle-270)*Mathf.Deg2Rad),-minimumDist*Mathf.Sin((travelAngle-270)*Mathf.Deg2Rad));
			oppositeGridPoint = new Vector2(columnDist*x_size+minimumDist*Mathf.Cos((travelAngle-270)*Mathf.Deg2Rad),rowDist*z_size+minimumDist*Mathf.Sin((travelAngle-270)*Mathf.Deg2Rad));
			initialGridPoint = travellingPoint;
			currentWave = waveType.line;
			waveAlive = true;
		}

		//create the newLine
	}
	void lineTravel(){
		//calculate distance to opposite point
		//initialDist = ;
		//make corner point travel
		travellingPoint.x += waveSpeed*Mathf.Cos(travelAngle*Mathf.Deg2Rad)*Time.deltaTime;
		travellingPoint.y += waveSpeed*Mathf.Sin(travelAngle*Mathf.Deg2Rad)*Time.deltaTime;
		//calculate distance to opposite point
		//finalDist=;

		//check if new point can still influence grid
		if (finalDist >= initialDist) {
			//stop the travel, waveAlive = false
			waveAlive = false;	
		} 
		else {
			//loop through all points in the grid (all water vertexes)
			//and check their distance to the line
			//apply different heights proportional to their distance 		
		}
	}

	void spawnNewWave(){
		
		//choose initial spawn point and initialize it
		travellingPoint = new Vector2(-minimumDist*Mathf.Cos((travelAngle-270)*Mathf.Deg2Rad),-minimumDist*Mathf.Sin((travelAngle-270)*Mathf.Deg2Rad));
		initialGridPoint = travellingPoint;
		oppositeGridPoint = new Vector2(columnDist*x_size+minimumDist*Mathf.Cos((travelAngle-270)*Mathf.Deg2Rad),rowDist*z_size+minimumDist*Mathf.Sin((travelAngle-270)*Mathf.Deg2Rad));
		currentWave = waveType.circle;
		waveAlive = true;

	}
	void waveTravel(){
		//it is a circle travel
		//calculate distance to opposite point
		initialDist = Vector2.Distance(travellingPoint,oppositeGridPoint);
		//make corner point travel
		travellingPoint.x += waveSpeed*Mathf.Cos(travelAngle*Mathf.Deg2Rad)*Time.deltaTime;
		travellingPoint.y += waveSpeed*Mathf.Sin(travelAngle*Mathf.Deg2Rad)*Time.deltaTime;
		//calculate distance to opposite point
		finalDist=Vector2.Distance(travellingPoint,oppositeGridPoint);

		//check if new point can still influence grid
		if (finalDist >= initialDist) {
			//stop the travel, waveAlive = false
			waveAlive = false;	
		} 
		else {
			//loop through all points in the grid (all water vertexes)
			//and check their distance to the line
			//apply different heights proportional to their distance 
			row = 0;
			col = 0;

			for (int i = 0; i <= z_size*x_size-1; i++) {
				//rand = Random.Range (rand_i,rand_f);
				newpos = waterVertexes [i].transform.position;
				currpos.x = newpos.x;
				currpos.y = newpos.z;

				pointdist = Vector2.Distance(travellingPoint,currpos);
				if (pointdist <= circleRadius + minimumDist) {
					
					if (Vector2.Angle(oppositeGridPoint - initialGridPoint, currpos-travellingPoint) > 90) {
						newpos.y -= influence * pointdist * Time.deltaTime;
						//Debug.Log ("sometimes is smaller");
					} 
					else {
						newpos.y += influence * pointdist * Time.deltaTime;
					}				
				}
				waterVertexes [i].transform.position = newpos;
				row++;
				if (row >= x_size) {
					col++;
					row = 0;
				}
			}
		}
		
	}
}

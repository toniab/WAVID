﻿using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {


	public bool isFound = false;
	int approaching;
	bool againstPlayer = false;
	Rigidbody rb;
	public float thrust = 6f;

	public PlayerController playerControl;

	public float veryClose = 0.5f;
	public float smoothRot = 5.0f;
	Transform babySeat;

	Vector3 relativePos = new Vector3();
	Vector3 lookAhead = new Vector3();
	float xtrmSeatsDist;

	BabyEntrance entrance;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		rb = GetComponent<Rigidbody> ();
		entrance = GetComponent<BabyEntrance> ();
		xtrmSeatsDist = Vector3.Distance(playerControl.xtrmSeatL.position,playerControl.xtrmSeatR.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (isFound) {
			if (Vector3.Distance (transform.position, babySeat.position) <= veryClose) {				
				if (approaching == 0) {
					babySeat = playerControl.xtrmSeatBack;
					approaching++;
					Debug.Log ("going to back seat");
				}
				else {
					if (approaching == 1) {					
						babySeat = playerControl.babySeats [playerControl.seat_i];
						playerControl.seat_i++;
						approaching++;
						Debug.Log("going to proper seat");
					}
				}
			}
			relativePos = babySeat.position - transform.position;
			if(approaching >= 2 && relativePos.magnitude < veryClose)
				lookAhead = playerControl.transform.forward;
			else
				lookAhead = Vector3.ProjectOnPlane (relativePos, Vector3.up);

			relativePos = relativePos.normalized;
			rb.AddForce(relativePos * thrust);				
			
			transform.forward = Vector3.Lerp (transform.forward, lookAhead, Time.deltaTime * smoothRot);
			//rb.MovePosition(babySeat.transform.position * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("Player") && !isFound) {
			isFound = true;

			approaching = 0;
			playerControl.foundBabies.Add (GetComponent<Rigidbody> ());

			if (playerControl.foundBabies.Count == 5) {
				entrance.lastBaby = true;
			}
			entrance.StartCoroutine ("FoundSequence");

			Transform opositeXtrmSeat;
			if (playerControl.seat_i % 2 == 0) {
				babySeat = playerControl.xtrmSeatL;
				opositeXtrmSeat = playerControl.xtrmSeatR;
			} else {
				babySeat = playerControl.xtrmSeatR;
				opositeXtrmSeat = playerControl.xtrmSeatL;
			}
			Debug.Log("going to xtrem seat");
			//checking if it will bump into player:
			if(Vector3.Distance(transform.position, babySeat.position) >= xtrmSeatsDist/2f){
				babySeat = opositeXtrmSeat;
				Debug.Log("going to oposite seat");
			}
		}
	}
}

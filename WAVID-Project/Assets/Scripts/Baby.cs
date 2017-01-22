using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {


	public bool isFound = false;
	bool approaching;
	bool againstPlayer = false;
	Rigidbody rb;
	public float thrust = 6f;

	public PlayerController playerControl;

	public float veryClose = 0.5f;
	Transform babySeat;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFound) {
			if (Vector3.Distance (transform.position, babySeat.position) <= veryClose) {
				if (approaching) {					
					babySeat = playerControl.babySeats [playerControl.seat_i];
					playerControl.seat_i++;
					approaching = false;
				}
			}
			Vector3 relativePos = babySeat.position - transform.position;
			if (!againstPlayer) //DO LINECAST OR SOMETHING TO SEE IF WOULD PUSH AGAINST PLAYER
			rb.AddForce(relativePos * thrust);
			//rb.MovePosition(babySeat.transform.position * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("Player") && !isFound) {
			//isFound = true;
			approaching = true;
			playerControl.foundBabies.Add (GetComponent<Rigidbody> ());
			if (playerControl.seat_i % 2 == 0) {
				babySeat = playerControl.xtrmSeatL;
			} else {
				babySeat = playerControl.xtrmSeatR;
			}
		}
	}
}

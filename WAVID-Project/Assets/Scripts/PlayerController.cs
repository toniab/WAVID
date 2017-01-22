using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public AudioClip bump;
	public List<AudioClip> squeaks = new List<AudioClip> ();
	public AudioSource audio;

	public Transform rightProp;
	public Transform leftProp;
	public float thrust = 2f;
	public float turnThrust = .2f;
	public float torque = 1f;

	public bool leftHand, rightHand;

	public List<Rigidbody> foundBabies = new List<Rigidbody>();

	int minTurnCount = 3;
	int turnCount = 0;
	enum Paddle{right, left};
	Paddle lastPressed = Paddle.left;

	Rigidbody rb;
	Rigidbody rightRB;
	Rigidbody leftRB;

	public Transform xtrmSeatL;
	public Transform xtrmSeatR;
	public Transform[] babySeats;
	public int seat_i;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		rightRB = rightProp.GetComponent<Rigidbody> ();
		leftRB = leftProp.GetComponent<Rigidbody> ();

		leftHand = false;
		rightHand = false;
	}
	
	// Update is called once per frame
	void Update () {
		float currThrust = thrust;
		float direction;

		if (Input.GetKeyDown ("l") || rightHand) { //RIGHT HAND DOWN

			if (lastPressed != Paddle.right && turnCount <= -minTurnCount) { //WERE YOU PREVIOUSLY TURNING LEFT
				turnCount = 0;
			} 
			turnCount++;

			if (turnCount > minTurnCount) {
				currThrust = turnThrust;
			}

			MovePlayer (rightProp.up * torque, transform.forward * currThrust);
			lastPressed = Paddle.right;

			rightHand = false;

		} else if (Input.GetKeyDown ("a") || leftHand) { //LEFT HAND DOWN

			if (lastPressed != Paddle.left && turnCount >= minTurnCount) {
				turnCount = 0;
			} 

			turnCount--;
			
			if (turnCount <= -minTurnCount) {
				currThrust = turnThrust;
			}

			MovePlayer (leftProp.up * -torque, transform.forward * currThrust);
			//rb.AddTorque(leftProp.up * -torque);
			//rb.AddForce (transform.forward * currThrust);
			lastPressed = Paddle.left;

			leftHand = false;
		}
			
	}

	void MovePlayer(Vector3 torqueVector, Vector3 forceVector) {
		rb.AddTorque(torqueVector);
		rb.AddForce (forceVector);

		/* REUSE THIS IF WE CAN'T GET WAYFINDING AROUND PARENT OR NAV MESH TO WORK.
		foundBabies.ForEach (delegate(Rigidbody babyRB)
			{
				babyRB.AddTorque(torqueVector / 2f);
				babyRB.AddForce (forceVector / 2f);
			});
			*/
				
	}

	void OnCollisionEnter(Collision other) {
		print (other.gameObject.name);
		if (other.gameObject.layer == 8) { // SQUEAK NOISE
			if (!audio.isPlaying) {
				audio.clip = squeaks[Random.Range(1, 4) - 1];
				audio.Play ();
			}
		} else if (other.gameObject.layer == 9) { // BUMP NOISE
			if (!audio.isPlaying) {
				audio.clip = bump;
				audio.Play ();
			}
		}
	}
		
}

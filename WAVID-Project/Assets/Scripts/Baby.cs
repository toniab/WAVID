using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {

	public GameObject babySeat;
	public bool isFound = false;
	Rigidbody rb;
	public float thrust = 6f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFound) {
			Vector3 relativePos = babySeat.transform.position - transform.position;
			rb.AddForce(relativePos * thrust);
			//rb.MovePosition(babySeat.transform.position * Time.deltaTime);
		}
	}
}

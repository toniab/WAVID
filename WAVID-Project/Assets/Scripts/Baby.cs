using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Baby : MonoBehaviour {

	public GameObject babySeat;
	bool isFound = false;
	Rigidbody rb;
	public float thrust = 6f;

	public Image foundName;


	public PlayerController playerControl;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFound) {
			Vector3 relativePos = babySeat.transform.position - transform.position;
			rb.AddForce(relativePos * thrust);
			//rb.MovePosition(babySeat.transform.position * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("Player") && !isFound) {
			isFound = true;
			playerControl.foundBabies.Add (GetComponent<Rigidbody> ());
			StartCoroutine ("FoundSequence");
		}
	}

	IEnumerator FoundSequence() {
		// play its sound
		// animate its catchphrase
		// play its particles

		//stop its particles
		//remove its catchphrase

		//play its name
		foundName.enabled = true;
		yield return new WaitForSeconds(5f);
		foundName.enabled = false;

		yield return null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyEntrance : MonoBehaviour {
	public Image foundName;
	public AudioSource audio;

	public PlayerController playerControl;

	Baby baby;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		baby = GetComponent<Baby> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("Player") && !baby.isFound) {
			baby.isFound = true;
			playerControl.foundBabies.Add (GetComponent<Rigidbody> ());
			StartCoroutine ("FoundSequence");

			if (!audio.isPlaying) {
				audio.Play();
			}
		}
	}

	IEnumerator FoundSequence() {
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

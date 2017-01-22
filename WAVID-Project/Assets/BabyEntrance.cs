using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyEntrance : MonoBehaviour {
	public Image foundName;
	public AudioSource audio;
	public AudioSource BGaudio;

	public PlayerController playerControl;

	Baby baby;

	bool crossfade = false;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		baby = GetComponent<Baby> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (crossfade) {
			if (audio.isPlaying) {
				BGaudio.volume -= Time.deltaTime;
				if (BGaudio.volume < 0) {
					BGaudio.volume = 0;
				}
			} else {
				BGaudio.volume += Time.deltaTime;
				if (BGaudio.volume > 1) {
					BGaudio.volume = 1;
					crossfade = false;
				}
			}
		} 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("Player") && !baby.isFound) {
			//print ("what");
			baby.isFound = true;
			//playerControl.foundBabies.Add (GetComponent<Rigidbody> ());
			StartCoroutine ("FoundSequence");

			if (!audio.isPlaying) {
				print ("playAudio");
				audio.Play();
				crossfade = true;
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

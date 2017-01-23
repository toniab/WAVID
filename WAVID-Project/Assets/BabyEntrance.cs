using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyEntrance : MonoBehaviour {
	public Image foundName;
	public ParticleSystem congratsParticles;
	public Image congratsText;
	public AudioSource audio;
	public AudioSource BGaudio;
	AudioClip winClip;
	public PlayerController playerControl;

	//Baby baby;

	bool crossfade = false;
	public bool lastBaby = false;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		//baby = GetComponent<Baby> ();
		audio = GetComponent<AudioSource> ();
		winClip = BGaudio.gameObject.GetComponent<GameSounds> ().winningClip;
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

	void CongratsBaby() {
		StartCoroutine ("FoundSequence");
		/*if (other.gameObject.name.Equals ("Player") && !baby.isFound) {
			baby.isFound = true;
			StartCoroutine ("FoundSequence");
		}*/
	}

	void PartyHome() {
		BGaudio.clip = winClip; 
	}

	IEnumerator FoundSequence() {
		// animate its catchphrase
		// play its particles

		congratsParticles.Play();

		if (!audio.isPlaying) {
			audio.Play();
			crossfade = true;
		}
			
		congratsText.enabled = true; //show its catchphrase
		yield return new WaitForSeconds (4f);
		congratsText.enabled = false; //remove its catchphrase
		foundName.enabled = true; //show its name

		yield return new WaitForSeconds(4f);
		foundName.enabled = false; //remove its name

		if (lastBaby) {
			PartyHome ();
		}

		yield return null;
	}
}

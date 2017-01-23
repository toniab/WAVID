using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeControl : MonoBehaviour {

	public Image winMsg;
	public Image almostMsg;
	public Image goHome;
	public float msgSpan = 3.0f;

	PlayerController playerControl;
	public ParticleSystem[] happyParticles;

	// Use this for initialization
	void Start () {		
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "Player") {
			Debug.Log("Hello pretty floaty Pappa!");
			StartCoroutine (KnockHome());
		}
	}

	IEnumerator KnockHome() {

		if (playerControl.foundBabies.Count >= 5) {
			goHome.enabled = false;
			winMsg.enabled = true;
			yield return new WaitForSeconds (msgSpan);
			for (int i = 0; i < happyParticles.Length; i++) {
				happyParticles [i].Play ();			
			}
			yield return new WaitForSeconds (2*msgSpan);
			SceneManager.LoadScene ("Title");
		} 
		else {
			almostMsg.enabled = true;
			yield return new WaitForSeconds (msgSpan);
			almostMsg.enabled = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeControl : MonoBehaviour {

	public Text winMsg;
	public Text almostMsg;
	public float msgSpan = 3.0f;

	PlayerController playerControl;

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
			winMsg.enabled = true;
			
		} else {
			almostMsg.enabled = true;
			yield return new WaitForSeconds (msgSpan);
			almostMsg.enabled = false;
		}
	}
}

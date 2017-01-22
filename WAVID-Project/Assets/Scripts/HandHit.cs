using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class HandHit : MonoBehaviour {

	public PlayerController pc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag.Equals("LeftHand"))
			pc.leftHand = true;
		else if(other.gameObject.tag.Equals("RightHand"))
			pc.rightHand = true;
	}

}

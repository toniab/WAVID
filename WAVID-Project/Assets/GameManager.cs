using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	bool useLeap = false;

	// Use this for initialization
	void Start () {
		if (useLeap == true) {
			//LOAD LEAP SCENE ADDITIVELY
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp ("r")) {
			SceneManager.LoadScene ("Title", LoadSceneMode.Single);
		}

		if (Input.GetKeyUp("escape")) {
			Application.Quit ();
		}
		
	}
}

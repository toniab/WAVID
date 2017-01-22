using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCam : MonoBehaviour {
	public float smoothRot = 10.0f;
	public float smoothTrans = 15.0f;
	public float freeLookLimit = 15.0f;

	private Transform myTransform;
	public Transform target;
	private Vector3 targetVirtual;
	public float camDistance = 3.0f;
	public float camDistanceClose = 1.0f;

	public float approachCameraSpeed = 200.0f;
	public float camHeight = 3.0f;
	public float targetHeight = 3.0f;


	public float effectiveCamDistance;
	public bool useMouse;
	public float XSensitivity=2f;
	public float YSensitivity=2f;

	public bool cameraOcluded = false;
	public List<string> ocludingElements;

	Vector3 newForth = new Vector3();
	Vector3 myForthProj = new Vector3();
	Vector3 newForthProj = new Vector3();

	void Start(){
		myTransform = transform;

		if (target == null) {
			GameObject targetGO = GameObject.FindGameObjectWithTag ("Player");
			if(targetGO != null)
				target = targetGO.transform;
		}
		if (target == null) {
			Time.timeScale = 0;
			Debug.Log ("Error! The target was not assigned, ThirdPersonCam script is deactivated!");
			this.enabled = false;
		}
		/*
		Vector3 auxPos = Vector3.ProjectOnPlane(myTransform.position, Vector3.up);
		camDistance = Vector3.Distance (target.position, auxPos);
		effectiveCamDistance = camDistance;
		*/
		camHeight = myTransform.position.y;
	}

	void Update ()
	{
		newForth = target.forward;
		newForthProj = Vector3.ProjectOnPlane (newForth, Vector3.up);
		myForthProj = Vector3.ProjectOnPlane (myTransform.forward, Vector3.up);


		myTransform.forward = Vector3.Lerp (myTransform.forward, newForth, Time.deltaTime * smoothRot);

		targetVirtual = target.position - effectiveCamDistance * newForth + camHeight*target.up;

		RaycastHit hit;
		Vector3 visionTarget = target.position + Vector3.up * targetHeight;
		Debug.DrawRay(myTransform.position, visionTarget - myTransform.position, new Color (0.0f, 0.5f, 0.5f));
		if (Physics.Raycast(myTransform.position, visionTarget - myTransform.position, out hit)) {
			//Debug.Log ("camera ray hiting: "+hit.transform.gameObject.name);
			if (hit.transform.gameObject.tag == "Player") {
				//check if futurePosition will be ocluded before going for it:
				float futureEffectiveCamDistance = effectiveCamDistance + approachCameraSpeed * Time.deltaTime;
				if (futureEffectiveCamDistance >= camDistance)
					futureEffectiveCamDistance = camDistance;
				Vector3 futurePos;
				futurePos = target.position - futureEffectiveCamDistance * newForth + camHeight * target.up;

				if (Physics.Raycast (futurePos, visionTarget - futurePos, out hit)) {
					if(hit.transform.gameObject.tag=="Player")
						effectiveCamDistance = futureEffectiveCamDistance;
				}
			} 
			else {
				//Time.timeScale = 0;
				effectiveCamDistance -= approachCameraSpeed*Time.deltaTime;
				if (effectiveCamDistance <= camDistanceClose)
					effectiveCamDistance = camDistanceClose;
			}
		}
		else {
			//Debug.Log ("camera ray not hitting anything!");
			//Time.timeScale = 0;
			effectiveCamDistance -= approachCameraSpeed;
			if (effectiveCamDistance <= camDistanceClose)
				effectiveCamDistance = camDistanceClose;
		}

		myTransform.position = Vector3.Lerp (myTransform.position, targetVirtual, Time.deltaTime * smoothTrans);

	}
}

using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {

	public float yMouseSensitivity = 5f;

	private float mouseY = 0f;
	private int minAngle = -90;
	private int maxAngle = 90;

	Quaternion originalRotation;

	// Use this for initialization
	void Start () {

		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (!FirstPersonController.Instance.freezeCtrls)
			LookUp(); //FirstPersonController.Instance.LookUp ();
	
	}

	public void LookUp(){
		
		mouseY -= Input.GetAxis ("Mouse Y") * yMouseSensitivity;
		Debug.Log (mouseY);
		mouseY = Mathf.Clamp(mouseY, minAngle, maxAngle);
		Quaternion yQuaternion = Quaternion.AngleAxis (mouseY, Vector3.right);
		transform.localRotation = originalRotation * yQuaternion;
		
	}

}

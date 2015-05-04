using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public static CharacterController CharCont;
	public static FirstPersonController Instance;

	public bool freezeCtrls;
	public float xMouseSensitivity = 5f;
	
	private float mouseX = 0f;
	
	Quaternion originalRotation;


	// Use this for initialization
	void Awake() {

		CharCont = GetComponent ("CharacterController") as CharacterController; 
		Instance = this;
		//FirstPersonCamera.UseExistingOrCreateCam ();
		freezeCtrls = false;
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update() {
		//checks to see if camera exists
		if (Camera.main == null)
			return;
		if (Input.GetButtonDown ("Inventory")) {
			freezeCtrls = !freezeCtrls;
		}

		if (!freezeCtrls) {
			GetMovementInput ();
			HandleActionInput();
			RotateAround();

		}

		FirstPersonMotor.Instance.UpdateMotor ();
	}

	void GetMovementInput(){
	

		//Holds dead space (the point in controllor direction where it doesn't actually count)
		var deadSpace = 0.1f;


		FirstPersonMotor.Instance.VerticalVelocity = FirstPersonMotor.Instance.MoveVector.y;
		//Zero out the Move Vector
		FirstPersonMotor.Instance.MoveVector = Vector3.zero;


		if (Input.GetAxis ("Vertical") > deadSpace || Input.GetAxis ("Vertical") < -deadSpace)
			FirstPersonMotor.Instance.MoveVector += new Vector3(0,0, Input.GetAxis("Vertical"));

		if (Input.GetAxis ("Horizontal") > deadSpace || Input.GetAxis ("Horizontal") < -deadSpace)
			FirstPersonMotor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);
		//Make sure out of dead space, and apply vert/horiz axis input when true




	}

	void RotateAround(){
	
		mouseX += Input.GetAxis("Mouse X") * xMouseSensitivity;
		Quaternion xQuaternion = Quaternion.AngleAxis (mouseX, Vector3.up);
		transform.localRotation = originalRotation * xQuaternion;

	}


	void HandleActionInput(){

		if (Input.GetButton ("Jump"))
			Jump ();

		if (Input.GetButtonDown ("Sprint"))
			Sprint(true);

		if (Input.GetButtonUp ("Sprint"))
			Sprint(false);

	}


	void Jump()
	{
		FirstPersonMotor.Instance.Jump ();
	}

	void Sprint(bool running)
	{
		FirstPersonMotor.Instance.Sprint (running);
	}

}






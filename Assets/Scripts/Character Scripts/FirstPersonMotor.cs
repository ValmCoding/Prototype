using UnityEngine;
using System.Collections;

public class FirstPersonMotor : MonoBehaviour {


	public static FirstPersonMotor Instance;

	public float MoveSpeed = 10f;
	public float gravity = 21f;
	public float terminalVelo = 20f;
	public float jumpSpeed = 10f;
	public float sprintSpeed = 2f;

	public Vector3 MoveVector { get; set;}
	public float VerticalVelocity{ get; set; }
	public float SprintVelocity { get; set;}

	// Use this for initialization
	void Awake() 
	{
		SprintVelocity = 1f;
		Instance = this;
	
	}
	
	// Update is called once per frame
	public void UpdateMotor() 
	{
		//SnapCharacterCamera();
		ProcessMotion();
	}

	void ProcessMotion()
	{ // Converts moveVector to World Space
		MoveVector = transform.TransformDirection (MoveVector);

	  // Normalize vector
		if (MoveVector.magnitude > 1) 
			MoveVector = Vector3.Normalize(MoveVector);

	  // multi moveVector by moveSPeed and Delta Time (time differenec )(frames to seconds)
		MoveVector *= MoveSpeed;


	  //Reapply vectors
		MoveVector = new Vector3 (MoveVector.x * SprintVelocity, VerticalVelocity, MoveVector.z * SprintVelocity);

	  //Apply Gravity
		AppyGravity ();

	  // move the thing
		FirstPersonController.CharCont.Move (MoveVector * Time.deltaTime );


	}


	void SnapCharacterCamera()
	{
		if (MoveVector.x != 0 || MoveVector.z != 0) 
		{
			transform.rotation = Quaternion.Euler (transform.eulerAngles.x, 
			                                       Camera.main.transform.eulerAngles.y, 
			                                       transform.eulerAngles.z);
		}
	}

	void AppyGravity(){

		if (MoveVector.y > - terminalVelo)
			MoveVector = new Vector3 (MoveVector.x, MoveVector.y - gravity * Time.deltaTime, MoveVector.z);

		if (FirstPersonController.CharCont.isGrounded && MoveVector.y < -1)
			MoveVector = new Vector3 (MoveVector.x, -1, MoveVector.z);

	}

	public void Jump(){
	
		if (FirstPersonController.CharCont.isGrounded)
			VerticalVelocity = jumpSpeed;
	
	}

	public void Sprint(bool running){


		if (FirstPersonController.CharCont.isGrounded && running)
			SprintVelocity = sprintSpeed;
		else 
			SprintVelocity = 1;

		Debug.Log ("Sprinting "+ SprintVelocity );

	}
}





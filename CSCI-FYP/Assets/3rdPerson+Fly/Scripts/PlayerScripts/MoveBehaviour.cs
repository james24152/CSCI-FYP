using UnityEngine;
using XboxCtrlrInput;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
    public XboxController joystick;
    public float walkSpeed = 0.15f;                 // Default walk speed.
    public float runSpeed = 1.0f;                   // Default run speed.
    public float sprintSpeed = 2.0f;                // Default sprint speed.
    public float animSpeedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
    public float speedDampTime = 0.1f;
    public XboxButton jumpButton;              // Default jump button.
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 5f;           // Default horizontal inertial force when jumping.

	private float speed, speedSeeker, currSpeed;    // Moving speed.
    private float speedSmoothVelocity;
    private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
    private int jumpButtonBool;
    private int idleJumpBool;
    private int flightBool;
    private bool idleJumpLeaveGround;
    private int flyBool;
    private bool jump = false;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.

	// Start is always called after any Awake functions.
	void Start() 
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
        jumpButtonBool = Animator.StringToHash("JumpButton");
        groundedBool = Animator.StringToHash("Grounded");
        flyBool = Animator.StringToHash("Fly");
        idleJumpBool = Animator.StringToHash("IdleJump");
        flightBool = Animator.StringToHash("FlightSkill");
        behaviourManager.GetAnim.SetBool (groundedBool, true);

		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour (this);
		behaviourManager.RegisterDefaultBehaviour (this.behaviourCode);
		speedSeeker = runSpeed;
	}

	// Update is used to set features regardless the active behaviour.
	void Update ()
	{
		// Get jump input.
		if (!jump && XCI.GetButtonDown(jumpButton, joystick) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			jump = true;
		}
        if (XCI.GetButton(jumpButton, joystick))
        {
            behaviourManager.GetAnim.SetBool(jumpButtonBool, true);
        }
        else
        {
            behaviourManager.GetAnim.SetBool(jumpButtonBool, false);
        }
    }

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
        // Call the basic movement manager.
        MovementManagement(behaviourManager.GetH, behaviourManager.GetV);

        // Call the jump manager.
        JumpManagement();
	}

	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
        // Is already jumping?
        /*if (behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.IsGrounded())
        {
            Debug.Log("jump1");
            // Keep forward movement while in the air.
            if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus() && !behaviourManager.GetAnim.GetBool(flyBool))
            {
                behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * sprintSpeed, ForceMode.Acceleration);
            }
        }*/
        // Has landed? (if it is a local jump and has landed)
        if (behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded() && !behaviourManager.GetAnim.GetBool(idleJumpBool))
        {
            Debug.Log("jump2");
            //never being executed
            behaviourManager.GetAnim.SetBool(groundedBool, true);
            // Change back player friction to default.
            GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
            GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
            // Set jump related parameters.
            jump = false;
            behaviourManager.GetAnim.SetBool(jumpBool, false);
            behaviourManager.UnlockTempBehaviour(this.behaviourCode);
        }
        //it is a idle jump and has landed
        else if (behaviourManager.IsGrounded() && behaviourManager.GetAnim.GetBool(idleJumpBool) && (idleJumpLeaveGround == true) /*&& !behaviourManager.GetAnim.GetBool(idleJumpBool)*/)
        {
            Debug.Log("jump3");
            //never being executed
            behaviourManager.GetAnim.SetBool(groundedBool, true);
            // Change back player friction to default.
            GetComponent<CapsuleCollider>().material.dynamicFriction = 10f;
            GetComponent<CapsuleCollider>().material.staticFriction = 10f;
            // Set jump related parameters.
            jump = false;
            //set flight skill bool to false if it is true before (using)
            if (behaviourManager.GetAnim.GetBool(flightBool))
                behaviourManager.GetAnim.SetBool(flightBool, false);
            behaviourManager.GetAnim.SetBool(idleJumpBool, false);
            behaviourManager.UnlockTempBehaviour(this.behaviourCode);
        }
        //idle jump started jump
        else if (!behaviourManager.IsGrounded() && behaviourManager.GetAnim.GetBool(idleJumpBool))
        {
            Debug.Log("jump4");
            idleJumpLeaveGround = true;
        }
        // Start a new jump.
        else if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded() && !behaviourManager.GetAnim.GetBool(flightBool) && !behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Down") && !behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Recover"))
        {
            Debug.Log("jump5");
            // Set jump related parameters.
            behaviourManager.LockTempBehaviour(this.behaviourCode);
            // Is a locomotion jump?
            if (behaviourManager.GetAnim.GetFloat(speedFloat) > 0.1)
            {
                behaviourManager.GetAnim.SetBool(jumpBool, true);
                // Temporarily change player friction to pass through obstacles.
                GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
                GetComponent<CapsuleCollider>().material.staticFriction = 0f;
                // Set jump vertical impulse velocity.
                float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
                velocity = Mathf.Sqrt(velocity);
                behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
            }
            //idle jump
            else {
                behaviourManager.GetAnim.SetBool(idleJumpBool, true);
                idleJumpLeaveGround = false;
            }

        }
	}

    void IdleJump()
    {
        // Temporarily change player friction to pass through obstacles.
        GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
        GetComponent<CapsuleCollider>().material.staticFriction = 0f;
        // Set jump vertical impulse velocity.
        float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
        velocity = Mathf.Sqrt(velocity);
        behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
    }

	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
			speed = sprintSpeed;
		}

        if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Down") || behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Recover")) {
            speed = 0f;
        }

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, animSpeedDampTime, Time.deltaTime);

        //move the character without rootmotion
        currSpeed = Mathf.SmoothDamp(currSpeed, speed, ref speedSmoothVelocity, speedDampTime);
        transform.Translate(transform.forward * currSpeed * Time.deltaTime, Space.World);
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
        if (!behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Down") && !behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("Recover"))
        {
            targetDirection = forward * vertical + right * horizontal;
        }else
            targetDirection = Vector3.zero;
        
        // Lerp current direction to calculated target direction.
        if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation (newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if(!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
	}
	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
	}
}

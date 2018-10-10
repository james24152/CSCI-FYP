using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBehaviour : GenericBehaviour
{
    public float swimSpeed = 1.0f;                 // Default flying speed.
    public Camera cam;
    public float turningSpeed = 0.01f;
    public float flapVelocity = 0.0f;
    public float drag = 5;
    private int swimBool;                          // Animator variable related to swimming.
    private bool swim = false;                     // Boolean to determine whether or not the player activated fly mode.
    private bool isInWater = false;
    private float waterSurfaceY = -2.0f;
    private Rigidbody rb;
    private Animator anim;

    // Start is always called after any Awake functions.
    void Start()
    {
        // Set up the references.
        swimBool = Animator.StringToHash("Swimming");
        // Subscribe this behaviour on the manager.
        behaviourManager.SubscribeBehaviour(this);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is used to set features regardless the active behaviour.
    void Update()
    {
        // Toggle fly by input, only if there is no overriding state or temporary transitions.
        if (isInWater && !behaviourManager.IsOverriding()
            && !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
        {
            swim = true;

            //behaviourManager.GetRigidBody.velocity = Vector3.zero;

            // Force end jump transition.
            behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

            behaviourManager.GetRigidBody.useGravity = false;
            behaviourManager.GetRigidBody.drag = drag;

            // Register this behaviour.
            behaviourManager.RegisterBehaviour(this.behaviourCode);
        }
        else if (!isInWater && !behaviourManager.IsOverriding()
           && !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
        {
            swim = false;
            behaviourManager.GetRigidBody.useGravity = true;
            behaviourManager.GetRigidBody.drag = 0;
            behaviourManager.UnregisterBehaviour(this.behaviourCode);
        }
        // Assert this is the active behaviour
        swim = swim && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

        // Set fly related variables on the Animator Controller.
        behaviourManager.GetAnim.SetBool(swimBool, swim);
    }

    bool IsUnderWater() {
        return cam.transform.position.y < waterSurfaceY;
    }

    // This function is called when another behaviour overrides the current one.

    // LocalFixedUpdate overrides the virtual function of the base class.
    public override void LocalFixedUpdate()
    {
        // Call the fly manager.
        SwimManagement(behaviourManager.GetH, behaviourManager.GetV);
    }
    // Deal with the player movement when flying.
    void SwimManagement(float horizontal, float vertical)
    {
        // Add a force player's rigidbody according to the fly direction.
        Vector3 direction = Rotating(horizontal, vertical);
        transform.Translate(direction * swimSpeed * 6 * Time.deltaTime, Space.World);
        if (transform.position.y >= (waterSurfaceY - 0.5f))
            if (!behaviourManager.IsGrounded())
                transform.position = new Vector3(transform.position.x, (waterSurfaceY - 0.5f), transform.position.z);
        //behaviourManager.GetRigidBody.AddForce((direction * flySpeed * 100 * (behaviourManager.IsSprinting() ? sprintFactor : 1)), ForceMode.VelocityChange);
    }

    // Rotate the player to match correct orientation, according to camera and key pressed.
    Vector3 Rotating(float horizontal, float vertical)
    {
        Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);
        // Camera forward Y component is relevant when flying.
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        // Calculate target direction based on camera forward and direction key.
        Vector3 targetDirection = forward * vertical + right * horizontal;

        // Rotate the player to the correct fly position.
        if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, turningSpeed);

            behaviourManager.GetRigidBody.MoveRotation(newRotation);
            behaviourManager.SetLastDirection(targetDirection);
        }

        if (!(Mathf.Abs(horizontal) > 0.2 || Mathf.Abs(vertical) > 0.2))
        {
            anim.SetBool("SwimIdle", true);
        }
        else
            anim.SetBool("SwimIdle", false);

        // Return the current fly direction.
        return targetDirection;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water")) {
            isInWater = true;
            Debug.Log("we are in water");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isInWater) {
            waterSurfaceY = other.transform.position.y;
            if (waterSurfaceY < transform.position.y) {
                if (behaviourManager.IsGrounded()) {
                    isInWater = false;
                    Debug.Log("we are out of water");
                }
            }
        }
    }
}


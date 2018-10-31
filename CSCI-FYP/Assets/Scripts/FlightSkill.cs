using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSkill : MonoBehaviour {

    public float height;
    public float stayHeight = 10f;
    public float drag = 10f;
    private Rigidbody rb;
    private MoveBehaviour moveScript;
    private Animator anim;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        moveScript = GetComponent<MoveBehaviour>();
        anim = GetComponent<Animator>();
    }

    void Activate()
    {
        // Temporarily change player friction to pass through obstacles.
        GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
        GetComponent<CapsuleCollider>().material.staticFriction = 0f;
        // Set jump vertical impulse velocity.
        float velocity = 2f * Mathf.Abs(Physics.gravity.y) * height;
        velocity = Mathf.Sqrt(velocity);
        rb.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
    }

    void Drag()
    {
        rb.drag = drag;
    }

    void Stay()
    {
        // Temporarily change player friction to pass through obstacles.
        moveScript.walkSpeed = 4f;
        moveScript.runSpeed = 4f;
        GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
        GetComponent<CapsuleCollider>().material.staticFriction = 0f;
        // Set jump vertical impulse velocity.
        float velocity = 2f * Mathf.Abs(Physics.gravity.y) * stayHeight;
        velocity = Mathf.Sqrt(velocity);
        rb.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
    }

    void Charge() {
        moveScript.walkSpeed = 0f;
        moveScript.runSpeed = 0f;
        anim.SetBool("FlightSkill", true);
    }
}

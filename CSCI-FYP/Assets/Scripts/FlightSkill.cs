using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSkill : MonoBehaviour {

    public float height = 100f;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();
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
}

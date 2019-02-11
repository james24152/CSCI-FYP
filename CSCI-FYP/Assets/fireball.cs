using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {
    Rigidbody rb;
    public float force;
    private float timer;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,0,force) , ForceMode.Force);
        timer = Time.time + 7.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer)
        {
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnitureController : MonoBehaviour {
    public Rigidbody furnitureRigidbody;
    private Vector3 zero;
    private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        furnitureRigidbody = GetComponent<Rigidbody>();
        zero = new Vector3(0.0f, 0.0f, 0.0f);
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        lastPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Earth Eve")
        {
            furnitureRigidbody.velocity = zero;
            furnitureRigidbody.angularVelocity = zero;
            transform.position = lastPosition; 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name != "Earth Eve")
        {
            furnitureRigidbody.velocity = zero;
            furnitureRigidbody.angularVelocity = zero;
            transform.position = lastPosition;
        }
    }
}

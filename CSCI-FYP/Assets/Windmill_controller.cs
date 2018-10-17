using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill_controller : MonoBehaviour {
    public GameObject fan;
    private WindmillFanSpin fanScript;
    private Rigidbody signRigidbody;
    private Vector3 location;
	// Use this for initialization
	void Start () {
        
        fanScript = fan.GetComponent<WindmillFanSpin>();
        location = new Vector3(transform.position.x-0.3f, transform.position.y - 17.3f, transform.position.z - 6.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "WindmillSign")
        {
            signRigidbody = other.gameObject.GetComponent<Rigidbody>();
            fanScript.triggered = true;
            other.gameObject.transform.position = location;
            Destroy(signRigidbody);
        }
    }
}

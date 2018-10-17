using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill_controller : MonoBehaviour {
    public GameObject sign;
    public GameObject fan;
    private WindmillFanSpin fanScript;
    private Rigidbody signRigidbody;
    private Vector3 location;
	// Use this for initialization
	void Start () {
        signRigidbody = sign.GetComponent<Rigidbody>();
        fanScript = fan.GetComponent<WindmillFanSpin>();
        location = new Vector3(transform.position.x, transform.position.y - 17.3f, transform.position.z - 6.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("collide");
        if(other.gameObject.tag == "WindmillSign")
        {
            print("is sign");
            fanScript.triggered = true;
            sign.transform.position = location;
            Destroy(signRigidbody);
        }
    }
}

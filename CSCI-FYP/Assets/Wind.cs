using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
    public float force;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            print("inside Wind Zone");
            //print(other.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f*force,0.0f,0.0f));
            
        }
    }
}

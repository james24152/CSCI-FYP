using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {
    public float step;
    private bool direction1;
    private bool direction2;
    private Vector3 location1;
    private Vector3 location2;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (getX()>107.5 && getX() < 108.5 && direction1 == true)
        {
            
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        print("enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            print("aaa");
        {
            other.gameObject.transform.parent.gameObject.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            other.gameObject.transform.parent.gameObject.transform.parent = null;
        }
    }

    public float getX()
    {
        return transform.position.x;
    }
}

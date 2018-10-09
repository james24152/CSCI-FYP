using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectControl : MonoBehaviour {
    public float RotateSpeed = 20;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime*RotateSpeed*2.0f, Time.deltaTime*RotateSpeed, Time.deltaTime*RotateSpeed*-1.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        // code of transforming element.


        Destroy(gameObject,0.1f);
    }
}

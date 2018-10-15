using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeController : MonoBehaviour {
    public GameObject button;
    public GameObject bridge;
    public float rotateSpeed = 10.0f;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        button.transform.Translate(Vector3.down * 0.1f);
    }

    private void OnTriggerStay(Collider other)
    {
        bridge.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        
        button.transform.Translate(Vector3.up * 0.1f);
    }

}

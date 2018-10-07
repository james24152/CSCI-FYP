using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeController : MonoBehaviour {
    public GameObject button;
    public GameObject bridge;
    private Vector3 targetPosition;
    // Use this for initialization
    void Start () {
        print("hello");
		targetPosition = new Vector3(button.transform.position.x, button.transform.position.y-0.1f, button.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
}

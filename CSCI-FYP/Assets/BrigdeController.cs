using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeController : MonoBehaviour {
    public GameObject button;
    public GameObject bridge;
    private Vector3 targetPosition;
    // Use this for initialization
    void Start () {
        
		targetPosition = new Vector3(transform.position.x, transform.position.y-0.1f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {

        
            print(collision.gameObject);
            print("hi");



        
           
    }

  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
    public bool hit;
    public bool hit2;
	// Use this for initialization
	void Start () {
        hit = false;
        hit2 = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;
        hit2 = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorFanSpin : MonoBehaviour {
    public float spinSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 1.0f * spinSpeed * Time.deltaTime));
	}
}

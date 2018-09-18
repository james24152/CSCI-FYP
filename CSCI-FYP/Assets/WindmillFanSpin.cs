using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillFanSpin : MonoBehaviour {
    public float spinSpeed;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
	}
}

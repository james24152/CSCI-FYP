using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillFanSpin : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 10 * Time.deltaTime);
    }

    
}

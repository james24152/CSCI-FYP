using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillFanSpin : MonoBehaviour {
    public GameObject fan;
    public float spinSpeed;
    private float accelerate;
    public bool triggered;

	// Use this for initialization
	void Start () {
        accelerate = 0;
        triggered = false;
        
    }

    // Update is called once per frame
	void Update () {
        if (triggered) {
            if (accelerate < 1.0f)
            {
                accelerate = accelerate + 0.01f;
                fan.transform.Rotate(0, 0, spinSpeed * accelerate * Time.deltaTime);
            }
            else
            {
                fan.transform.Rotate(0, 0, spinSpeed * accelerate * Time.deltaTime);
            }
        }
        
        
	}

   
}

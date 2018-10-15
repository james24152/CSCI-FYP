using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillFanSpin : MonoBehaviour {
    public GameObject fan;
    public float spinSpeed;
    private float accelerate;
    private bool triggered;
    private 
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
                accelerate = accelerate + 0.001f;
                
            }
            else
            {
                fan.transform.Rotate(0, 0, spinSpeed * accelerate * Time.deltaTime);
            }
        }
        
        
	}

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        transform.Translate(Vector3.back * 0.1f);
    }
}

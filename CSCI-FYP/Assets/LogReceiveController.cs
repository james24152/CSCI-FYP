using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogReceiveController : MonoBehaviour {
    private int woodCount = 0;
    public ParticleSystem smokeParticle;
	// Use this for initialization
	void Start () {
        smokeParticle.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void working()
    {
        smokeParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "logs")
        {
            woodCount = woodCount + 1;
            Destroy(other.gameObject);
        }
        if (woodCount == 3)
        {
            working();
        }
    }
}

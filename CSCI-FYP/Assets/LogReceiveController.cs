using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogReceiveController : MonoBehaviour {
    public int woodCount = 0;
    public ParticleSystem smokeParticle;
    private Grabber grabber;
	// Use this for initialization
	void Start () {
        smokeParticle.Stop();
        print("stoped");
	}

    private void working()
    {
        Debug.Log("working");
        smokeParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "logs")
        {
            grabber = other.gameObject.GetComponent<Grabber>();
            if (grabber.grabber != null)
                grabber.grabber.GetComponent<PickUpBehaviour>().forceEject = true;
            woodCount = woodCount + 1;
            Destroy(other.gameObject);
        }
        if (woodCount == 3)
        {
            working();
        }
    }
}

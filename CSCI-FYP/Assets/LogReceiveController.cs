using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogReceiveController : MonoBehaviour {
    public int woodCount = 0;
    public ParticleSystem smokeParticle;
    private Grabber grabber;
    private Vector3 targetPosition;
    private GameObject chimney;
    private AudioManager audioMangaer;
    // Use this for initialization
    void Start () {
        chimney = transform.GetChild(0).gameObject;
        targetPosition = chimney.transform.position;
        audioMangaer = FindObjectOfType<AudioManager>();
    }

    private void working()
    {
        Instantiate(smokeParticle, targetPosition,Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "logs")
        {
            audioMangaer.Play("RecieveLogs");
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

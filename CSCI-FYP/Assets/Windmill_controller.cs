using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill_controller : MonoBehaviour {
    public GameObject fan;
    public bool fanSpin;
    private WindmillFanSpin fanScript;
    private Rigidbody signRigidbody;
    private Vector3 location;
    private Grabber grabber;
    private AudioManager audioMangaer;
    // Use this for initialization
    void Start () {
        
        fanScript = fan.GetComponent<WindmillFanSpin>();
        location = new Vector3(1.54f, 4.07f, 0.04f);
        audioMangaer = FindObjectOfType<AudioManager>();
    }
	

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "WindmillSign")
        {
            audioMangaer.Play("PutSign");
            grabber = other.gameObject.GetComponent<Grabber>();
            if (grabber.grabber != null)
                if (grabber.grabber.transform.name == "Air Eve" || grabber.grabber.transform.name == "Air Eve(Clone)")
                    grabber.grabber.GetComponent<PickUpBehaviour>().forceEject = true;
            signRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Destroy(signRigidbody);
            fanScript.triggered = true;
            other.gameObject.transform.parent = transform.parent;
            other.gameObject.transform.localPosition = location;
            other.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            fanSpin = true;
        }
    }
}

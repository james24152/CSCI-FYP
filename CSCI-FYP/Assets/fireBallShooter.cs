using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallShooter : MonoBehaviour {
    public GameObject fireBall;
    private float nextInstTime;
    public float interval;
    public float power;
    private bool created;
    private GameObject fireBallObject;
    private Vector3 shootTarget;
    private AudioManager audioManager;
	// Use this for initialization
	void Start () {
        created = false;
        nextInstTime = Time.time + interval;
        audioManager = FindObjectOfType<AudioManager>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextInstTime && created == false)
        {
            fireBallObject = Instantiate(fireBall, transform.position, Quaternion.identity);
            fireBallObject.GetComponent<Rigidbody>().AddForce(transform.forward * power*-1);
            audioManager.Play("cannonShoot");
            nextInstTime = nextInstTime + interval;
        }
        if (created == true && Time.time > nextInstTime - 1.0f)
        {
            Destroy(fireBallObject);
        }
	}

    


}

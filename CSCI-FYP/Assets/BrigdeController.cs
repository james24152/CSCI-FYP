using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeController : MonoBehaviour {
    public GameObject button;
    public GameObject bridge;
    public float rotateSpeed = 10.0f;

    private AudioManager audioMangaer;
    // Use this for initialization
    void Start () {
        audioMangaer = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider) {
            audioMangaer.Play("StepOn"); //dun know why this also makes trigger exit have sound
            button.transform.Translate(Vector3.down * 0.1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider)
        {
            bridge.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
            button.transform.Translate(Vector3.up * 0.1f);
    }

}

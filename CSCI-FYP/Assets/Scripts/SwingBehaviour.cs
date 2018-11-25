using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : MonoBehaviour {

    public float range = 2f;
    private ChopBehaviour chopScript;
    private AudioManager audioMangaer;
    private GameObject axe;

    private void Start()
    {
        audioMangaer = FindObjectOfType<AudioManager>();
    }

    void Swing() {
        audioMangaer.Play("Swing");
    }

    void ColliderActive() {
        axe = GetComponent<PickUpBehaviour>().itemGrabbed;
        axe.GetComponent<Collider>().enabled = true;
    }
    void ColliderDeactive()
    {
        axe = GetComponent<PickUpBehaviour>().itemGrabbed;
        axe.GetComponent<Collider>().enabled = false;
    }
}

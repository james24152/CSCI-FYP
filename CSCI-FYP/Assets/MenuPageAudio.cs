using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageAudio : MonoBehaviour {
    private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BGMBigEnough");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

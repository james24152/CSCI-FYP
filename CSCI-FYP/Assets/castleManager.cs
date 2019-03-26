using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleManager : MonoBehaviour {
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {

        audioManager = FindObjectOfType<AudioManager>();
       // audioManager.Play("BGMMedieval");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

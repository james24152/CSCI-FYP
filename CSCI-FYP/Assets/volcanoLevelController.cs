using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volcanoLevelController : MonoBehaviour {
    private AudioManager audioManager;
	// Use this for initialization
	void Start () {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("volcanoBGM");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

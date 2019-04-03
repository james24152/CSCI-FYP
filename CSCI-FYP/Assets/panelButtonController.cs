using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelButtonController : MonoBehaviour {
    public Animator animButton;
    public Animator bridge;
    private bool triggered;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        triggered = false;
        audioManager = FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")&&triggered == false)
        {
            animButton.SetBool("triggered", true);
            bridge.SetBool("triggered", true);
            audioManager.Play("volcanoBridge");
            triggered = true;
            
        }

    }
}

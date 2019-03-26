using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorRight1 : MonoBehaviour {
    private Animator anim;
    public bool haveKey;
    private AudioManager audioManager;
    public bool soundPlayed;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        soundPlayed = false;

    }
	
	// Update is called once per frame
	void Update () {
		if (!soundPlayed && anim.GetBool("opened"))
        {
            audioManager.Play("doorOpen");
            soundPlayed = true;
        }
        if (soundPlayed && !anim.GetBool("opened"))
        {
            soundPlayed = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && haveKey == true)
        {
            
            anim.SetBool("opened", true);
        }
        
    }
}

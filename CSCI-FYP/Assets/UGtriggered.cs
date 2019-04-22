using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGtriggered : MonoBehaviour {
    public Animator anim;
    private AudioManager audioManager;
    private castleManager manager;
    // Use this for initialization
    void Start () {
        audioManager = FindObjectOfType<AudioManager>();
        manager = FindObjectOfType<castleManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            anim.SetBool("triggered", true);
            audioManager.Stop("castleBGM");
            audioManager.Play("castleBossBGM");
            manager.sublevel = 2;
        }
    }

}

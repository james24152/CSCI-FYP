using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorRight1 : MonoBehaviour {
    public Animator anim;
    private bool triggered;
    public string AnimName;
    public bool haveKey;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && triggered == false && haveKey == true)
        {
            anim.Play(AnimName);
            triggered = true;
        }
        
    }
}

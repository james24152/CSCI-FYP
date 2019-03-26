using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorRight1 : MonoBehaviour {
    private Animator anim;
    public bool haveKey;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && haveKey == true)
        {
            anim.SetBool("opened", true);
        }
        
    }
}

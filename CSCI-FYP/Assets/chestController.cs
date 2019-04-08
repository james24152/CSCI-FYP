using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour {
    public bool triggered;
    public bool haveKey;
    private Animator anim;
    
    
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
            triggered = true;
            anim.SetBool("chestOpen", true);
            

        }
    }

}

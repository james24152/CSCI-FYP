using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGtriggered : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            anim.SetBool("triggered", true);
            
        }
    }

}

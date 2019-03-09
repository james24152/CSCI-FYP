using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeChestController : MonoBehaviour {
    private int counter;
    public Animator anim;
	// Use this for initialization
	void Start () {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            counter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            counter--;
            if(counter == 0 && anim.GetBool("chestOpen"))
            {
                anim.SetBool("chestOpen", false);
            }
        }
    }
}

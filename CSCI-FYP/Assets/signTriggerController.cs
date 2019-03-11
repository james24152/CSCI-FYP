using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class signTriggerController : MonoBehaviour {
    public Animator anim;
    private int count;
	void Start () {
        count = 0;  
	}
	
	// Update is called once per frame
	void Update () {
       

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && anim.GetBool("toggled")==false) 
        {
            print("triggered");
            //count++;
            anim.SetBool("toggled", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            print("leave");
            //count--;
            //if(count == 0 && anim.GetBool("toggled") == true)
            if (anim.GetBool("toggled") == true)
            {
                anim.SetBool("toggled", false);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeChestController : MonoBehaviour {
    private int counter;
    public Animator anim;
    public GameObject cover;
    private chestController coverScript;
	// Use this for initialization
	void Start () {
        counter = 0;
        coverScript = cover.GetComponent<chestController>();
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
                coverScript.triggered = false;
            }
        }
    }
}

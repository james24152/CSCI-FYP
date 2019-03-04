using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.transform.parent.GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.name == "OuterCollider")
        {
            anim.SetBool("OuterCollider", true);
        }
        else if (gameObject.transform.name == "InnerCollider")
        {
            anim.SetBool("InnerCollider", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameObject.transform.name == "OuterCollider")
        {
            anim.SetBool("OuterCollider", false);
        }
        else if (gameObject.transform.name == "InnerCollider")
        {
            anim.SetBool("InnerCollider", false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

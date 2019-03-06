using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {

    private Animator anim;
    private Animator shopAnim;

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
            shopAnim = ShopScript.shopScript.gameObject.GetComponent<Animator>();
            //delay the time for conversation bubble
            shopAnim.SetBool("Open", true);
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
            shopAnim = ShopScript.shopScript.gameObject.GetComponent<Animator>();
            shopAnim.SetBool("Open", false);
            anim.SetBool("InnerCollider", false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

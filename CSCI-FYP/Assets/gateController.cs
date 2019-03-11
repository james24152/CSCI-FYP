using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour {
    public GameObject gateL;
    public GameObject gateR;
    private Vector3 gateLTarget;
    private Vector3 gateRTarget;
    private bool triggered;
    public Animator anim; 
    // Use this for initialization
    void Start () {
        triggered = false;
        gateLTarget = new Vector3(gateL.transform.position.x, gateL.transform.position.y - 3.0f, gateL.transform.position.z);
        gateRTarget = new Vector3(gateR.transform.position.x, gateR.transform.position.y - 3.0f, gateR.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && triggered == false)
        {
            gateOpen();
            print("enter");
            triggered = true;
        }
    }

    public void gateOpen()
    {
        anim.SetBool("triggered", true);
        //gateL.transform.position = Vector3.MoveTowards(gateL.transform.position, gateLTarget, 0.1f);
        //gateR.transform.position = Vector3.MoveTowards(gateR.transform.position, gateRTarget, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour {
    private bool triggered;
    public bool haveKey;
    
	// Use this for initialization
	void Start () {
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
            transform.Rotate(new Vector3(-90, 0, 0));
        }
    }

}

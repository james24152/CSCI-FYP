using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1Close : MonoBehaviour {
    private int counter;
    public Animator animD1;
    public Animator animD2;
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
            print(counter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            print(counter);
            counter--;
            print(counter);
            if(counter == 0 && animD1.GetBool("opened") == true)
            {
                animD1.SetBool("opened", false);
            }
            if (counter == 0 && animD2.GetBool("opened") == true)
            {
                animD2.SetBool("opened", false);
            }
        }
    }
}

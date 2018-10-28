using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectControl : MonoBehaviour {
    public float RotateSpeed = 20;
    private CharacterChangeController CCCScript;
    private bool sameElement;
    private Animator anim;
    
	// Use this for initialization
	void Start () { 

	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime*RotateSpeed*2.0f, Time.deltaTime*RotateSpeed, Time.deltaTime*RotateSpeed*-1.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) {
            anim = other.gameObject.GetComponent<Animator>();
            if (!anim.GetBool("PickUp") && !anim.GetBool("Grab")) {
                CCCScript = other.gameObject.GetComponent<CharacterChangeController>();
                switch (gameObject.tag)
                {
                    case "EarthCollect":

                        if (other.gameObject.tag != "EarthEve")
                        {                                       //it is not same element
                            CCCScript.Evolve(1);
                            sameElement = false;
                        }
                        else
                        {
                            sameElement = true;
                        }
                        break;

                    case "WaterCollect":
                        if (other.gameObject.tag != "WaterEve")
                        {                                       //it is not same element
                            CCCScript.Evolve(2);
                            sameElement = false;
                        }
                        else
                        {
                            sameElement = true;
                        }
                        break;

                    case "FireCollect":
                        if (other.gameObject.tag != "FireEve")
                        {                                       //it is not same element
                            CCCScript.Evolve(3);
                            sameElement = false;
                        }
                        else
                        {
                            sameElement = true;
                        }
                        break;
                    case "AirCollect":
                        if (other.gameObject.tag != "AirEve")
                        {                                       //it is not same element
                            CCCScript.Evolve(4);
                            sameElement = false;
                        }
                        else
                        {
                            sameElement = true;
                        }
                        break;

                    default:
                        break;
                }

                if (!sameElement)
                    Destroy(gameObject, 0.1f);
            }
        }
    }
}

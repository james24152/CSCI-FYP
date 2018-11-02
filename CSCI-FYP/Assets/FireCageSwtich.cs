using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCageSwtich : MonoBehaviour {
    public GameObject selfFireCage;
    public GameObject fireCage1;
    public GameObject fireCage2;
    public GameObject fireCage3;
    private burningController burningScriptSelf;
    private burningController burningScript1;
    private burningController burningScript2;
    private burningController burningScript3;
    public bool burning;
    private bool theSwitch;
	// Use this for initialization
	void Start () {
        theSwitch = false;
		burning = true;
        burningScriptSelf = selfFireCage.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript1 = fireCage1.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript2 = fireCage2.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript3 = fireCage3.transform.GetChild(0).gameObject.GetComponent<burningController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (theSwitch == true )
        {
            print("switch");
            burningScriptSelf.turn();
            theSwitch = false;
        }   
	}

    private void turn()
    {
        burningScript1.turn();
        burningScript2.turn();
        burningScript3.turn();
    }



    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("WaterAttack") && burning == true)
        {
            turn();
            burning = false;
            theSwitch = true;
        }
        if (other.gameObject.CompareTag("fireAttack") && burning == false)
        {
            turn();
            burning = true;
            theSwitch = true;
        }
    }

 /*   private void OnTriggerEnter(Collider other)
    {
        print("collide");
        if (burning == true)
        {
            turn();
            burning = false;
            theSwitch = true;
        }
        else if (burning == false)
        {
            turn();
            burning = true;
            theSwitch = true;
        }
    }*/
}

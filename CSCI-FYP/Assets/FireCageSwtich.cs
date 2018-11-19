﻿using System.Collections;
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
    private bool fire;
    private bool water;
    // Use this for initialization
    void Start () {
        theSwitch = false;
		burning = true;
        fire = true;
        water = false;
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
            if (!water)
            {
                turn();
                theSwitch = true;
                burning = false;
                water = true;
                fire = false;
            }
        }
        else if (other.gameObject.CompareTag("FireAttack") && burning == false)
        {
            if (!fire)
            {
                turn();
                burning = true;
                theSwitch = true;
                fire = true;
                water = false;
            }
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

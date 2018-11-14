using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burningController : MonoBehaviour {
    public bool burning;
	// Use this for initialization
	void Start () {
        burning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (burning == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
           
        }
        else if(burning == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
	}
    public void turn()
    {
        print("turn!");
        burning = !burning;
    }

    public void forceOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}

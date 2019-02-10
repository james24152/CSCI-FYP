using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGunController : MonoBehaviour {
    float nextSpawnTime;
    public GameObject fireGun;
	// Use this for initialization
	void Start () {
        nextSpawnTime = Time.time+5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawnTime)
        {
            Instantiate(fireGun);
            nextSpawnTime = nextSpawnTime + 5.0f;
        }
	}
    

}

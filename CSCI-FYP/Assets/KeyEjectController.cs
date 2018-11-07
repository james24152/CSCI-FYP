using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEjectController : MonoBehaviour {
    private LogReceiveController logReceiveScript1;
    private LogReceiveController logReceiveScript2;
    public GameObject door1;
    public GameObject door2;
    public GameObject key;
    private int keyEjected = 0;
    private Vector3 keyPos;
    // Use this for initialization
    void Start () {
        logReceiveScript1 = door1.GetComponent<LogReceiveController>();
        logReceiveScript2 = door2.GetComponent<LogReceiveController>();
        keyPos = new Vector3(58f, -6.35f, 50.0f);
	}
	
	// Update is called once per frame
	void Update () {
  		if(logReceiveScript1.woodCount == 3 && logReceiveScript2.woodCount == 3 && keyEjected==0)
        {
            ejectKey();
            keyEjected = 1;
        }
        
	}

    private void ejectKey()
    {
        int i = 0;
        for (i= 0;i< 2; i++)
        {
            GameObject nkey =  Instantiate(key);
            nkey.transform.position = keyPos;
            nkey.GetComponent<Rigidbody>().velocity = new Vector3(-7.0f, 5.0f, Random.Range(-2.0f,2.0f));
            
        }
        GameObject[] gameManager;
        gameManager = GameObject.FindGameObjectsWithTag("GameManager");
        Level1GameManager managerScript = gameManager[0].GetComponent<Level1GameManager>();
        managerScript.sublevel = 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeButton : MonoBehaviour {
    public GameObject button;
    public GameObject bridge;
    private Vector3 targetPos;
    private Vector3 OriginPos;
	// Use this for initialization
	void Start () {
        //print(transform.position.x);print(transform.position.y);print( transform.position.z);
        targetPos = new Vector3(23.7f,1.7f,42.0f);
        OriginPos = new Vector3(23.1f,2.0f,42.0f);
    }

	// Update is called once per frame
	void Update () {
        if(transform.position.x>22.5f && transform.position.x < 24.5f)
        {
            if(transform.position.z>41.5f && transform.position.z < 43.0f)
            {
                if (transform.position.y < 3.0f){
                    //print("onTheButton");
                   //button.transform.position = Vector3.MoveTowards(button.transform.position, targetPos, 0.005f);
                    bridge.transform.Rotate(0,Time.deltaTime*10,0);
                }
                
            }
            
        }
        
	}
}

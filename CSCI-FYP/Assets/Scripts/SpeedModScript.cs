using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players) {
                MoveBehaviour moveScript = player.GetComponent<MoveBehaviour>();
                moveScript.walkSpeed = 4.0f;
                moveScript.runSpeed = 4.0f;
                Debug.Log("Walk speed back to normal");
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                MoveBehaviour moveScript = player.GetComponent<MoveBehaviour>();
                moveScript.walkSpeed = 10.0f;
                moveScript.runSpeed = 10.0f;
                Debug.Log("Walk speed boosted");
            }
        }
    }
}

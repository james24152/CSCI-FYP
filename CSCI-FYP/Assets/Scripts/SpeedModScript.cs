using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModScript : MonoBehaviour {

    private GameObject[] players;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeSpeed(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSpeed(true);
        }
    }

    void ChangeSpeed(bool change) {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            MoveBehaviour moveScript = player.GetComponent<MoveBehaviour>();
            FireBlowController fireScript = player.GetComponent<FireBlowController>();
            if (change)
            {
                fireScript.speedMod = true;
            }
            else {
                fireScript.speedMod = false;
            }
        }
    }
}

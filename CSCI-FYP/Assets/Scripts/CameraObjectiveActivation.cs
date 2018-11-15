using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectiveActivation : MonoBehaviour {

    private GameObject[] gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectsWithTag("GameManager");
    }
    public void StartObjectiveText() {
        if (gameObject.transform.name == "Camera2") {
            Debug.Log("log objective active");
            gameManager[0].GetComponent<Level1GameManager>().logObjectiveStart = true;
        }else if (gameObject.transform.name == "Camera3")
        {
            Debug.Log("windmill objective active");
            gameManager[0].GetComponent<Level1GameManager>().signObjectiveStart = true;
        }//cam 4 has no objective text
        else if (gameObject.transform.name == "Camera5")
        {
            Debug.Log("failed objective active");
            gameManager[0].GetComponent<Level1GameManager>().missionReset = true;
        }
        else if (gameObject.transform.name == "Camera6")
        {
            Debug.Log("end game");
            gameManager[0].GetComponent<Level1GameManager>().missionSuccess = true;
        }
    }
    public void InvokeWaveStart()
    {
        if (gameObject.transform.name == "Camera4")
        {
            Debug.Log("wave objective active");
            gameManager[0].GetComponent<Level1GameManager>().defendObjectiveStart = true;
        }
    }
}

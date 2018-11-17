﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameMenuScript : MonoBehaviour {

    private int playerNum = 1;
	// Use this for initialization
	void Start () {
		
	}

    public void AssignPlayerNum(Toggle toggle) {
        switch (toggle.transform.name) {
            case "ToggleP1":
                if (toggle.isOn == true)
                    playerNum = 1;
                break;
            case "ToggleP2":
                if (toggle.isOn)
                    playerNum = 2;
                break;
            case "ToggleP3":
                if (toggle.isOn)
                    playerNum = 3;
                break;
            case "ToggleP4":
                if (toggle.isOn)
                    playerNum = 4;
                break;
            default:
                break;
        }
    }

    public void Stage1() {
        //load stage 1 (MapAncient)
        PlayerPrefs.SetInt("playerNum", playerNum);
        SceneManager.LoadScene(1);
    }

    public void Stage2() {
        //load stage 1 (MapAncient)
        PlayerPrefs.SetInt("playerNum", playerNum);
        PlayerPrefs.SetInt("saveStatus", 1);
        SceneManager.LoadScene(2);
    }
}

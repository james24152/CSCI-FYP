using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2GameManager : MonoBehaviour {

    public int sublevel = 1;
    public bool missionSuccess;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BGMMedieval");
    }

    private void Update()
    {
        if (missionSuccess)
        {
            //endgame
            SceneManager.LoadScene(0);
        }
    }
}

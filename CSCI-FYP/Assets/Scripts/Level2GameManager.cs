using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2GameManager : MonoBehaviour {

    public int sublevel = 1;
    public bool missionSuccess;

    private void Update()
    {
        if (missionSuccess)
        {
            //endgame
            SceneManager.LoadScene(0);
        }
    }
}

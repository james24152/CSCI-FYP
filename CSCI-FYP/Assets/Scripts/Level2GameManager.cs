using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2GameManager : MonoBehaviour {

    public int sublevel = 1;
    // Use this for initialization
    public int playerCount;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Text arenaObjective;
    public ParticleSystem enemySymbol;
    public bool arenaObjectiveStart;
    public DialogueTrigger dialogueTrigger;
    public bool missionSuccess;

    private bool inited;
    private bool inited2;
    private GameObject[] players;
    private AudioManager audioMangaer;
    private void Start()
    {
        audioMangaer = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        /*if (arenaObjectiveStart)
            arenaObjective.gameObject.SetActive(true);
        else
            arenaObjective.gameObject.SetActive(false);*/
        if (missionSuccess)
        {
            //endgame
        }
    }
}

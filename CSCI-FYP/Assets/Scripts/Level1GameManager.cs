using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour {

    public int sublevel = 1;
    // Use this for initialization
    public int playerCount;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Text logObjective;
    public Text signObjective;
    public GameObject enemyAcademy;
    public GameObject enemies;
    public ParticleSystem enemySymbol;
    public bool logObjectiveStart;
    public bool signObjectiveStart;
    public bool defendObjectiveStart;
    public bool missionFailed;
    public DialogueTrigger dialogueTrigger;
    public bool missionReset;
    public bool missionSuccess;

    private bool inited;
    private bool inited2;
    private GameObject[] players;
    private AudioManager audioManager;
    private void Start()
    {
        
        playerCount = PlayerPrefs.GetInt("playerNum");
        switch (playerCount)
        {
            case 1:
                player1.SetActive(true);
                break;
            case 2:
                player1.SetActive(true);
                player2.SetActive(true);
                break;
            case 3:
                player1.SetActive(true);
                player2.SetActive(true);
                player3.SetActive(true);
                break;
            case 4:
                player1.SetActive(true);
                player2.SetActive(true);
                player3.SetActive(true);
                player4.SetActive(true);
                break;
            default:
                Debug.Log("Player Activation failure");
                break;
        }
        TunePlayView(playerCount);
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BGMAncient");
    }
    private void Update()
    {
        if (logObjectiveStart)
            logObjective.gameObject.SetActive(true);
        else
            logObjective.gameObject.SetActive(false);
        if (signObjectiveStart)
            signObjective.gameObject.SetActive(true);
        else
            signObjective.gameObject.SetActive(false);
        if (defendObjectiveStart) {
            if (!inited) {
                audioManager.Stop("BGMAncient");
                audioManager.Play("BGMFighting");
                audioManager.Play("Symbol");
                enemySymbol.Play();
                Invoke("SetEnemyActive", 2f);
                inited = true;
            }
        }
        if (missionFailed) {
            audioManager.Stop("BGMFighting");
            dialogueTrigger.TriggerDialogue();
            missionFailed = false;
        }
        if (missionReset) {
            if (!inited2)
            {
                MissionReset();
            }
        }
        if (missionSuccess) {
            enemyAcademy.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    private void SetEnemyActive() {
        enemies.SetActive(true);
        enemyAcademy.SetActive(true);
    }

    private void MissionReset() {
        enemies.SetActive(false);
        enemies.SetActive(true);
        audioManager.Play("BGMFighting");
        missionReset = false;
        players = GameObject.FindGameObjectsWithTag("Player");
        Health healthScript;
        foreach (GameObject player in players) {
            healthScript = player.GetComponent<Health>();
            healthScript.Respawn();
        }
        inited2 = false;
    }

    public void TunePlayView(int playerNum)
    {
        if (playerNum == 1)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (playerNum == 2)
        {

            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
        }
        else if (playerNum == 3)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            player3.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
        }
        else if (playerNum == 4)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            player3.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
            player4.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volcanoLevelController : MonoBehaviour
{
    private AudioManager audioManager;
    public int playerCount;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject[] slimes;

    public Animator bossAnim;
    public ParticleSystem darkTornado;
    public DialogueTrigger trigger;
    public bool bridgeTrigger;
    private bool inited;
    private int enemiesLeft;
    private bool retreated;
    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("volcanoBGM");
        //playerCount = PlayerPrefs.GetInt("playerNum");
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
    }
    private void Update()
    {
        enemiesLeft = 0;
        if (bridgeTrigger) {
            if (!inited) {
                Invoke("RiseBoss", 1f);
                Invoke("SpawnMinion", 14f);
                inited = true;
            }
            foreach (GameObject slime in slimes) {
                if (slime != null) {
                    enemiesLeft++;
                }
            }
            if (enemiesLeft == 0) {
                if (!retreated) {
                    trigger.TriggerDialogue();
                    Invoke("PlayTornado", 2f);
                    Invoke("RetreatBoss", 3f);
                    retreated = true;
                }
            }
        }
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

    public void PlayTornado() {
        darkTornado.Play();
    }

    public void RiseBoss() {
        bossAnim.SetBool("Rise", true);
    }

    public void RetreatBoss()
    {
        bossAnim.SetBool("Retreat", true);
    }

    public void SpawnMinion() {
        foreach (GameObject slime in slimes)
        {
            slime.SetActive(true);
            enemiesLeft++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) {
            bridgeTrigger = true;
        }
    }
}

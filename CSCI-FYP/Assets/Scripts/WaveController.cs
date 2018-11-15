using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public GameObject[] enemies;
    public bool waveStart;
    public DialogueTrigger dialogueTrigger;
    private bool inited1;
    private bool inited2;
    private bool inited3;
    private bool finishedWave1;
    private bool finishedWave2;
    private AgentScript script1;
    private AgentScript script2;
    private AgentScript script3;
    private AgentScript script4;
    private AgentScript script5;
    private AgentScript script6;
    private AgentScript script7;
    private AudioManager audioMangaer;
    // Use this for initialization
    void Start () {
        audioMangaer = FindObjectOfType<AudioManager>();
    }

    private void OnEnable()
    {
        inited1 = false;
        inited2 = false;
        inited3 = false;
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Agent");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
        waveStart = true;
    }

    // Update is called once per frame
    void Update () {
        if (waveStart) {
            if (!inited1) {
                initWave1();
                inited1 = true;
            }
            if (CheckFinishWave1()) {
                if (!inited2)
                {
                    initWave2();
                    inited2 = true;
                }
                if (CheckFinishWave2())
                {
                    if (!inited3)
                    {
                        initWave3();
                        inited3 = true;
                    }
                    if (CheckFinishWave3()) {
                        audioMangaer.Play("Win");
                        dialogueTrigger.TriggerDialogue();
                    }
                }
            }
        }
	}

    void initWave1() {
        GameObject enemy1 = Instantiate(enemies[0], transform.position, transform.rotation);
        script1 = enemy1.GetComponent<AgentScript>();
        enemy1.SetActive(true);
        GameObject enemy2 = Instantiate(enemies[1], transform.position, transform.rotation);
        script2 = enemy2.GetComponent<AgentScript>();
        enemy2.SetActive(true);
        GameObject enemy3 = Instantiate(enemies[2], transform.position, transform.rotation);
        script3 = enemy3.GetComponent<AgentScript>();
        enemy3.SetActive(true);
    }

    bool CheckFinishWave1() {
        return script1.death && script2.death && script3.death;
    }

    void initWave2()
    {
        GameObject enemy1 = Instantiate(enemies[0], transform.position, transform.rotation);
        script1 = enemy1.GetComponent<AgentScript>();
        enemy1.SetActive(true);
        GameObject enemy2 = Instantiate(enemies[1], transform.position, transform.rotation);
        script2 = enemy2.GetComponent<AgentScript>();
        enemy2.SetActive(true);
        GameObject enemy3 = Instantiate(enemies[2], transform.position, transform.rotation);
        script3 = enemy3.GetComponent<AgentScript>();
        enemy3.SetActive(true);
        GameObject enemy4 = Instantiate(enemies[3], transform.position, transform.rotation);
        script4 = enemy4.GetComponent<AgentScript>();
        enemy4.SetActive(true);
        GameObject enemy5 = Instantiate(enemies[4], transform.position, transform.rotation);
        script5 = enemy5.GetComponent<AgentScript>();
        enemy5.SetActive(true);
    }

    bool CheckFinishWave2()
    {
        return script1.death && script2.death && script3.death && script4.death && script5.death;
    }

    void initWave3()
    {
        GameObject enemy1 = Instantiate(enemies[0], transform.position, transform.rotation);
        script1 = enemy1.GetComponent<AgentScript>();
        enemy1.SetActive(true);
        GameObject enemy2 = Instantiate(enemies[1], transform.position, transform.rotation);
        script2 = enemy2.GetComponent<AgentScript>();
        enemy2.SetActive(true);
        GameObject enemy3 = Instantiate(enemies[2], transform.position, transform.rotation);
        script3 = enemy3.GetComponent<AgentScript>();
        enemy3.SetActive(true);
        GameObject enemy4 = Instantiate(enemies[3], transform.position, transform.rotation);
        script4 = enemy4.GetComponent<AgentScript>();
        enemy4.SetActive(true);
        GameObject enemy5 = Instantiate(enemies[4], transform.position, transform.rotation);
        script5 = enemy5.GetComponent<AgentScript>();
        enemy5.SetActive(true);
        GameObject enemy6 = Instantiate(enemies[5], transform.position, transform.rotation);
        script6 = enemy6.GetComponent<AgentScript>();
        enemy6.SetActive(true);
        GameObject enemy7 = Instantiate(enemies[6], transform.position, transform.rotation);
        script7 = enemy7.GetComponent<AgentScript>();
        enemy7.SetActive(true);
    }

    bool CheckFinishWave3()
    {
        return script1.death && script2.death && script3.death && script4.death && script5.death && script6.death && script7.death;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBoxController : MonoBehaviour {
    public bool triggered;
    public GameObject allLight;
    public float power;
    public bool press;
    public Animator anim;
    public bool allEnemyKilled;
    private bool playAlarm;
    private bool playDing;
    private int enemyCount;
    private bool spawnedAll;
    public AudioManager audioManager;
    public GameObject[] slimeTeam;
    public GameObject[] ghostTeam;
    public GameObject[] batTeam;
    // Use this for initialization
    void Start () {
        triggered = false;
        press = false;
        allEnemyKilled = false;
        playAlarm = false;
        audioManager = FindObjectOfType<AudioManager>();
        playDing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (spawnedAll) {
            if (CountEnemies() <= 0)
                allEnemyKilled = true;
        }
        if (press == true && triggered == false)
        {
            triggered = true;
            audioManager.Play("alarm");
            anim.SetBool("electricBoxTriggered", true);
            LightOn();
        }

        if (allEnemyKilled) //only run once
        {
            //audioManager.Play("ding");
            anim.SetBool("allEnemyKilled", true);
        }
        if (!playAlarm && anim.GetBool("electricBoxTriggered")) //only run once
        {
            playAlarm = true;
            audioManager.Play("alarm");
            SpawnDinningEnemies();
        }

        if (playAlarm && anim.GetBool("allEnemyKilled")&& !playDing)
        {
            allEnemyKilled = false;
            audioManager.Stop("alarm");
            audioManager.Play("ding");
            playDing = true;

        }


    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("LightningAttack") && triggered == false)
        {
            triggered = true;

            anim.SetBool("electricBoxTriggered", true);
            LightOn();
        }
        

    }
    public void LightOn()
    {
        //audioManager.Play("alarm");

        for (int i = 0; i < allLight.transform.childCount; i++)
        {
            Transform nextLight = allLight.transform.GetChild(i);
            Light light = nextLight.GetComponent<Light>();
            light.intensity = power;
        }
    }

    private void SpawnDinningEnemies() {

        Invoke("SpawnSlimeTeam", 4f);
        Invoke("SpawnGhostTeam", 30f);
        Invoke("SpawnBatTeam", 60f);
    }

    private void SpawnSlimeTeam() {
        foreach (GameObject slime in slimeTeam)
        {
            slime.SetActive(true);
        }
    }

    private void SpawnGhostTeam()
    {
        foreach (GameObject ghost in ghostTeam)
        {
            ghost.SetActive(true);
        }
    }

    private void SpawnBatTeam()
    {
        foreach (GameObject bat in batTeam)
        {
            bat.SetActive(true);
        }
        spawnedAll = true;
    }

    private int CountEnemies() {
        enemyCount = 0;
        foreach (GameObject slime in slimeTeam)
        {
            if (slime != null)
                enemyCount++;
        }
        foreach (GameObject ghost in ghostTeam)
        {
            if (ghost != null)
                enemyCount++;
        }
        foreach (GameObject bat in batTeam)
        {
            if (bat != null)
                enemyCount++;
        }
        Debug.Log("Enemy Left = " + enemyCount);
        return enemyCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Use this for initialization
    public int downTime = 4;
    public int health = 5;
    public GameObject center;
    public ParticleSystem earthFx;
    public ParticleSystem waterFx;
    public ParticleSystem fireFx;
    public ParticleSystem airFx;
    private ParticleSystem tempFx;
    private Transform respawn;
    private bool invoked;
    private Animator anim;
    private float nextTimeGetHit = 0f;
    private MoveBehaviour moveScript;
    private Rigidbody rb;
    GameObject[] gameManager;
    Level1GameManager managerScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
        gameManager = GameObject.FindGameObjectsWithTag("GameManager");
        managerScript = gameManager[0].GetComponent<Level1GameManager>();
        rb = GetComponent<Rigidbody>();
        respawn = FindSpawn();
    }

    public bool GetHit() {
        if (Time.time > nextTimeGetHit) {
            health--;
            if (!invoked)
            {
                anim.SetBool("Damaged", true);
                invoked = true;
                //Invoke("SetDamagedFalse", 0.2f);
            }
            if (health == 0) {
                anim.SetBool("Death", true);
                Invoke("Respawn", 3f);
            }
            nextTimeGetHit = Time.time + downTime;
            return true;
        }
        return false;
    }

    public void Drown() {
        health = 0;
        if (!invoked)
        {
            anim.SetBool("Damaged", true);
            invoked = true;
        }
        anim.SetBool("Death", true);
        Invoke("Respawn", 3f);
    }


    private void SetDamagedFalse() {
        anim.SetBool("Damaged", false);
        invoked = false;
    }

    private void Respawn() {
        health = 2;
        respawn = FindSpawn();
        transform.position = respawn.position;
        transform.eulerAngles = respawn.eulerAngles;
        if ((gameObject.transform.name == "Earth Eve") || (gameObject.transform.name == "Earth Eve(Clone)"))
        {
            tempFx = Instantiate(earthFx, center.transform.position, center.transform.rotation);
        }
        if (gameObject.transform.name == "Water Eve" || gameObject.transform.name == "Water Eve(Clone)")
        {
            tempFx = Instantiate(waterFx, center.transform.position, center.transform.rotation);
        }
        if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
        {
            tempFx = Instantiate(fireFx, center.transform.position, center.transform.rotation);
        }
        if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
        {
            tempFx = Instantiate(airFx, center.transform.position, center.transform.rotation);
        }
        tempFx.transform.parent = center.transform;
        anim.SetBool("Death", false);
        rb.useGravity = true;
        rb.drag = 0;
    }

    private Transform FindSpawn() {
        if (managerScript.sublevel == 1)
        {
            GameObject[] spawn1 = GameObject.FindGameObjectsWithTag("Spawn1");
            if ((gameObject.transform.name == "Earth Eve") || (gameObject.transform.name == "Earth Eve(Clone)"))
            {
                return spawn1[0].transform;
            }
            if (gameObject.transform.name == "Water Eve" || gameObject.transform.name == "Water Eve(Clone)")
            {
                return spawn1[1].transform;
            }
            if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
            {
                return spawn1[2].transform;
            }
            if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
            {
                return spawn1[3].transform;
            }
        }
        else if (managerScript.sublevel == 2) {
            GameObject[] spawn2 = GameObject.FindGameObjectsWithTag("Spawn2");
            if ((gameObject.transform.name == "Earth Eve") || (gameObject.transform.name == "Earth Eve(Clone)"))
            {
                return spawn2[0].transform;
            }
            if (gameObject.transform.name == "Water Eve" || gameObject.transform.name == "Water Eve(Clone)")
            {
                return spawn2[1].transform;
            }
            if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
            {
                return spawn2[2].transform;
            }
            if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
            {
                return spawn2[3].transform;
            }
        }
        Debug.Log("level is wrong");
        return gameObject.transform;
    }
}

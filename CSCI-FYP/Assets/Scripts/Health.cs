using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    // Use this for initialization
    public int downTime = 4;
    public int health = 2;
    public int healthCap = 2;
    public GameObject center;
    public ParticleSystem earthFx;
    public ParticleSystem waterFx;
    public ParticleSystem fireFx;
    public ParticleSystem airFx;
    public Image icon;
    public Sprite earthIcon;
    public Sprite waterIcon;
    public Sprite fireIcon;
    public Sprite airIcon;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite hollowHeart;
    private ParticleSystem tempFx;
    private Transform respawn;
    private bool invoked;
    private Animator anim;
    private float nextTimeGetHit = 0f;
    private MoveBehaviour moveScript;
    private Rigidbody rb;
    private DrownBehaviour drownScript;
    GameObject[] gameManager;
    Level1GameManager managerScript;

    private void Start()
    {
        switch (transform.name) {
            case "Earth Eve":
                icon.sprite = earthIcon;
                break;
            case "Earth Eve(Clone)":
                icon.sprite = earthIcon;
                break;
            case "Water Eve":
                icon.sprite = waterIcon;
                break;
            case "Water Eve(Clone)":
                icon.sprite = waterIcon;
                break;
            case "Fire Eve":
                icon.sprite = fireIcon;
                break;
            case "Fire Eve(Clone)":
                icon.sprite = fireIcon;
                break;
            case "Air Eve":
                icon.sprite = airIcon;
                break;
            case "Air Eve(Clone)":
                icon.sprite = airIcon;
                break;
        }
        anim = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
        gameManager = GameObject.FindGameObjectsWithTag("GameManager");
        managerScript = gameManager[0].GetComponent<Level1GameManager>();
        rb = GetComponent<Rigidbody>();
        drownScript = GetComponent<DrownBehaviour>();
        respawn = FindSpawn();
        if (transform.name == "Earth Eve" || transform.name == "Water Eve" || transform.name == "Fire Eve" || transform.name == "Air Eve")
            FirstSpawn();
    }

    private void Update()
    {
        for (int i = 0; i < healthCap; i++) {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
                hearts[i].sprite = hollowHeart;
        }
    }

    public void FirstSpawn() {
        if (!invoked)
        {
            anim.SetBool("Damaged", true);
            invoked = true;
        }
        anim.SetBool("Death", true);
        Invoke("Respawn", 5f);
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
                Invoke("Respawn", 2f);
            }
            nextTimeGetHit = Time.time + downTime;
            return true;
        }
        return false;
    }

    public void Drown() {
        Debug.Log("drown");
        health = 0;
        if (!invoked)
        {
            anim.SetBool("Damaged", true);
            invoked = true;
        }
        anim.SetBool("Death", true);
        Invoke("Respawn", 2f);
    }


    private void SetDamagedFalse() {
        anim.SetBool("Damaged", false);
        invoked = false;
    }

    private void Respawn() {
        Debug.Log("respawn");
        health = 2;
        respawn = FindSpawn();
        transform.position = respawn.position;
        transform.eulerAngles = respawn.eulerAngles;
        rb.angularDrag = 10;
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
        if (drownScript != null) {
            drownScript.inited = false;
            drownScript.isInWater = false;
        }
        rb.useGravity = true;
        rb.drag = 0;
        Invoke("Wake", 2f);
    }

    private void Wake() {
        anim.SetBool("Death", false);
        rb.angularDrag = 0;
    }

    private Transform FindSpawn() {
        if (managerScript.sublevel == 1)
        {
            GameObject[] spawn1 = GameObject.FindGameObjectsWithTag("Spawn1");
            if ((gameObject.transform.name == "Earth Eve") || (gameObject.transform.name == "Earth Eve(Clone)"))
            {
                return spawn1[1].transform;
            }
            if (gameObject.transform.name == "Water Eve" || gameObject.transform.name == "Water Eve(Clone)")
            {
                return spawn1[0].transform;
            }
            if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
            {
                return spawn1[3].transform;
            }
            if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
            {
                return spawn1[2].transform;
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
                return spawn2[3].transform;
            }
            if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
            {
                return spawn2[1].transform;
            }
            if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
            {
                return spawn2[2].transform;
            }
        }
        Debug.Log("level is wrong");
        return gameObject.transform;
    }
}

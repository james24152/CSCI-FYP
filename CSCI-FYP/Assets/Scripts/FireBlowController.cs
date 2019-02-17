﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class FireBlowController : MonoBehaviour
{
    public XboxController joystick;
    public ParticleSystem fireHandL;
    public GameObject shootPoint;
    public GameObject bullet;
    public ParticleSystem fireHandR;
    public ParticleSystem fire;
    public float bulletForce;
    public float range = 10;
    public float damage = 10;
    public XboxAxis punchButton;
    public Camera cam;
    public bool speedMod;

    private MoveBehaviour moveScript;
    private bool audioInited;
    private bool audioInitedFire;
    private AudioManager audioMangaer;
    private bool isBear;
    private ElementCombineBehaviour combineScript;

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
        combineScript = GetComponent<ElementCombineBehaviour>();
        audioMangaer = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = (XCI.GetAxis(punchButton, joystick) != 0);
        animator.SetBool("Punch", shoot);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") || animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            moveScript.walkSpeed = 1.5f;
            moveScript.runSpeed = 1.5f;
        }
        else
        {
            if (audioInitedFire) {
                StopAudio();
                audioInitedFire = false;
                //audioInited = false;
            }
            if (!speedMod)
            {
                moveScript.walkSpeed = 4;
                moveScript.runSpeed = 4;
            }
            else {
                moveScript.walkSpeed = 10;
                moveScript.runSpeed = 10;
            }
        }

        if (!shoot && audioInited) {
            audioMangaer.FadeOut("Charge");
            Debug.Log("fadeout + speed");
            audioInited = false;
        }
    }

    void Cast()
    {

        audioMangaer.Play("Charge");
        audioInited = true;
        if (!combineScript.secondTierElement)
        {
            fireHandL.Play();
            fireHandR.Play();
        }
    }

    void Fire()
    {
        if (!combineScript.secondTierElement)
        {
            if (!audioInitedFire)
            {
                StartAudio();
                audioInitedFire = true;
            }
            fire.Play();
        }
        else
            combineScript.SecondTierAttack();
    }
    void FireBear()
    {
        isBear = true;
        GameObject tempBullet;
        tempBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;

        tempBullet.transform.Rotate(Vector3.back * 90);

        Rigidbody tempRigid;
        tempRigid = tempBullet.GetComponent<Rigidbody>();
        if (animator.GetBool("Aim"))
            tempRigid.AddForce(cam.transform.forward * bulletForce);
        else
            tempRigid.AddForce(transform.forward * bulletForce);

        //fire.Play();

    }

    void StartAudio() {
        if (transform.name == "Fire Eve" || transform.name == "Fire Eve(Clone)")
            audioMangaer.Play("ShootFire"); //play fire sound effect
        if (transform.name == "Water Eve" || transform.name == "Water Eve(Clone)")
            audioMangaer.Play("ShootWater"); //play fire sound effect
        if (transform.name == "Air Eve" || transform.name == "Air Eve(Clone)")
            audioMangaer.Play("ShootWind"); //play fire sound effect
    }

    void StopAudio()
    {
        if (transform.name == "Fire Eve" || transform.name == "Fire Eve(Clone)")
            audioMangaer.FadeOut("ShootFire");
        if (transform.name == "Water Eve" || transform.name == "Water Eve(Clone)")
            audioMangaer.FadeOut("ShootWater");
        if (transform.name == "Air Eve" || transform.name == "Air Eve(Clone)")
            audioMangaer.FadeOut("ShootWind");
    }
}


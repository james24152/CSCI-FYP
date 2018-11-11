﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaMissionController : MonoBehaviour {
   public GameObject[] shootTarget = new GameObject[4];
    public GameObject blockPos;
    private bool forOnce;
    public GameObject DB;
    private CommunWithDatabase DBscript;
    private int elementToShoot;
    private TargetScript[] shootTargetScript = new TargetScript[4];
    private int progress;
    // Use this for initialization
    void Start () {
        forOnce = false;
        DBscript = DB.GetComponent<CommunWithDatabase>();
        for (int i=0; i<4; i++)
        {
            shootTargetScript[i] = shootTarget[i].GetComponent<TargetScript>();
        }
        randShowBlock();
    }
	
	// Update is called once per frame
	void Update () {
        if(forOnce =shootTargetScript[elementToShoot].hit2 == true && forOnce == false)
        {
            shootTargetScript[elementToShoot].hit2 = false;
            progress = progress + 1;
            randShowBlock();
        }
        if (progress == 5)
        {
            forOnce = true;
            destroyLine();
            progress = progress + 1;
        }
	}

    void destroyLine()
    {
        int i = 0;
        while (i < 16)
        {
            StartCoroutine(DisableObject(i, transform.GetChild(15 - i).gameObject));
            i = i + 1;
        }
        DBscript.finishPuzzle(1);
    }

    void randShowBlock()
    {
        elementToShoot = Random.Range(0, 3);
        print("randomGen");
        print(elementToShoot);
        for (int j = 0; j < 4; j++)
        {
            blockPos.transform.GetChild(j).gameObject.SetActive(false);
        }
        blockPos.transform.GetChild(elementToShoot).gameObject.SetActive(true);
    }

    IEnumerator DisableObject(int i, GameObject road)
    {
        print(i);
        yield return new WaitForSeconds(i - 1);
        print("wait end");
        print(i);
        road.SetActive(false);
    }

    public void levelClear()
    {
        for (int j = 0; j < 4; j++)
        {
            blockPos.transform.GetChild(j).gameObject.SetActive(false);
        }
        for (int i =0; i < 16; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        forOnce = true;
    }
}

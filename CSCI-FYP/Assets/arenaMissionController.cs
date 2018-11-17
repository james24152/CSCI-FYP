using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaMissionController : MonoBehaviour {
   public GameObject[] shootTarget = new GameObject[4];
    public GameObject blockPos;
    private bool forOnce;
    public GameObject DB;
    public DialogueTrigger trigger;
    public AudioManager audioManager;

    private CommunWithDatabase DBscript;
    private int elementToShoot;
    private TargetScript[] shootTargetScript = new TargetScript[4];
    private int progress;
    private bool audioLock;
    // Use this for initialization
    void Start () {
        forOnce = false;
        DBscript = DB.GetComponent<CommunWithDatabase>();
        audioManager = FindObjectOfType<AudioManager>();
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
        if (progress == 5 && forOnce == false)
        {
            if (!audioLock) {
                audioManager.Play("Win");
                audioLock = true;
            }
            trigger.TriggerDialogue();
            forOnce = true;
            for (int j = 0; j < 4; j++)
            {
                blockPos.transform.GetChild(j).gameObject.SetActive(false);
            }
            destroyLine();
            
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
        elementToShoot = Random.Range(0, 4);
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
        yield return new WaitForSeconds(i/2f);
        print("wait end");
        print(i);
        road.SetActive(false);
        road.SetActive(true);
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

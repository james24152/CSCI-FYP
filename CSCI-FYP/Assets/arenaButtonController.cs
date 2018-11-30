using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaButtonController : MonoBehaviour {
    public GameObject shootTarget;
    public GameObject shootTargetGate;
    private int gateLock;
    private TargetScript shootTargetScript;
    private bool GateOpen;
    private GameObject wholeSystem;
    private AudioManager audioManager;
    private bool audioLock;
	// Use this for initialization
	void Start () {
        gateLock = 0;
        GateOpen = false;
        shootTargetScript = shootTarget.GetComponent<TargetScript>();
        wholeSystem = transform.parent.gameObject;
        audioManager = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        
		if(shootTargetScript.hit == true)
        {
            shootTargetGateClose();
            shootTargetScript.hit = false;
        }
        if (gateLock > 0)
        {
            Spin();
            print(gateLock);
            gateOpenning();
            if (!audioLock) {
                audioManager.Play("ArenaDoor");
                audioLock = true;
            }
            StartCoroutine(lockCountDown());
            //Play Sound **********************
        }
        if (gateLock < 0)
        {
            print(gateLock);
            Spin();
            gateClosing();
            if (!audioLock)
            {
                audioManager.Play("ArenaDoor");
                audioLock = true;
            }
            StartCoroutine(lockCountDown());
            //Play Sound ***********************
        }
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("WindAttack") && GateOpen == false)
        {
            shootTargetGateOpen();
        }
    }

    public void shootTargetGateOpen()
    {
       // Vector3 shootTargetGatePos = new Vector3(shootTargetGate.transform.position.x+4.0f, shootTargetGate.transform.position.y, shootTargetGate.transform.position.z);
        //shootTargetGate.transform.position = Vector3.MoveTowards(shootTargetGate.transform.position, shootTargetGatePos, 0.1f);

        //shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, -4.0f));
        GateOpen = true;
        gateLock = 1;
        
    }
    public void shootTargetGateClose()
    {
        //shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, 4.0f));
        GateOpen = false;
        gateLock = -1;
    }
    public void Spin()
    {
        wholeSystem.transform.Rotate(Vector3.up * gateLock*2.0f);
    }
    public void gateOpenning()
    {
        shootTargetGate.transform.Translate(Vector3.back * gateLock*0.1f);
    }
    public void gateClosing()
    {
        shootTargetGate.transform.Translate(Vector3.back * gateLock*0.1f);
    }
    IEnumerator lockCountDown()
    {
        yield return new WaitForSeconds(3.0f);
        gateLock = 0;
        if (audioLock)
        {
            audioManager.FadeOut("ArenaDoor");
            audioLock = false;
        }
    }
    
}

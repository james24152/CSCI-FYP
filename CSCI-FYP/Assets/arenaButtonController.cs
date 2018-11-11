using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaButtonController : MonoBehaviour {
    public GameObject shootTarget;
    public GameObject shootTargetGate;
    private bool buttonLock;
    private TargetScript shootTargetScript;
    private bool GateOpen;
	// Use this for initialization
	void Start () {
        buttonLock = false;
        GateOpen = false;
        shootTargetScript = shootTarget.GetComponent<TargetScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(shootTargetScript.hit == true)
        {
            shootTargetGateClose();
            shootTargetScript.hit = false;
        }
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("WindAttack") && GateOpen == true)
        {
            shootTargetGateOpen();
        }
    }

    public void shootTargetGateOpen()
    {
        shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, -2.0f));
    }
    public void shootTargetGateClose()
    {
        shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f));
    }
}

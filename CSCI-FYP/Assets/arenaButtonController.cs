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
        if (other.gameObject.CompareTag("WindAttack") && GateOpen == false)
        {
            
            shootTargetGateOpen();
        }
    }

    public void shootTargetGateOpen()
    {
        Vector3 shootTargetGatePos = new Vector3(shootTargetGate.transform.position.x+4.0f, shootTargetGate.transform.position.y, shootTargetGate.transform.position.z);
        shootTargetGate.transform.position = Vector3.MoveTowards(shootTargetGate.transform.position, shootTargetGatePos, 0.1f);
        
        //shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, -4.0f)*Time.deltaTime);
        if (shootTargetGate.transform.position.x == shootTargetGatePos.x)GateOpen = true;
        
    }
    public void shootTargetGateClose()
    {
        shootTargetGate.transform.Translate(new Vector3(0.0f, 0.0f, 4.0f));
        GateOpen = false;
    }
}

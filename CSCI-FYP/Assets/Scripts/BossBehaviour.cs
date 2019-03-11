using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    public ParticleSystem pushAura;
    public ParticleSystem push;
    public ParticleSystem healing;
    public ParticleSystem aoeAura;
    public ParticleSystem aoe;
    public ParticleSystem swordAura;
    public ParticleSystem proj;
    public Transform origin;
    private ParticleSystem tempAttack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireProj()
    {
        proj.Play();
    }

    public void SwordCharge() {
        swordAura.Play();
    }

    public void PushCharge() {
        tempAttack = Instantiate(pushAura, origin.transform.position, pushAura.transform.rotation);
        Destroy(tempAttack, 7f);
    }
    public void Push()
    {
        tempAttack = Instantiate(push, origin.transform.position, push.transform.rotation);
        Destroy(tempAttack, 10f);
    }

    public void Heal()
    {
        tempAttack = Instantiate(healing, origin.transform.position, healing.transform.rotation);
        Destroy(tempAttack, 10f);
    }

    public void AOECharge()
    {
        tempAttack = Instantiate(aoeAura, origin.transform.position, aoeAura.transform.rotation);
        Destroy(tempAttack, 10f);
    }

    public void AOE()
    {
        tempAttack = Instantiate(aoe, origin.transform.position, aoe.transform.rotation);
        //Destroy(tempAttack, 20f);
    }


}

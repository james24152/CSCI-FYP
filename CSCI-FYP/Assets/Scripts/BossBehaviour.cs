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
    public int health = 100;

    private ParticleSystem tempAttack;
    private SphereCollider collider;
    private Animator anim;
    private bool far;
    private bool healthLow;
	// Use this for initialization
	void Start () {
        collider = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        anim.SetBool("FarAway", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) {
            anim.SetBool("FarAway", false); //boss will initiate melee attack
        }
    }

    private void Tactics() {
        far = anim.GetBool("FarAway");
        healthLow = health < 80 ? true : false;
        if (far)
        {
            if (healthLow)
            {
                InitiatePushAndHeal();
            }
            else {
                InitiateProjectile();
                //randomly init projectile and aoe
            }
        }
        else {
            InitiateSwing();
        }
    }

    public void InitiateProjectile() {
        anim.SetBool("UseProj", true);
    }

    public void InitiateSwing()
    {
        anim.SetBool("Swing", true);
    }

    public void InitiateAOE()
    {
        anim.SetBool("UseAOE", true);
    }

    public void InitiatePushAndHeal()
    {
        anim.SetBool("NeedHeal", true);
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

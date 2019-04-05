using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FlyEnemy : SmallEnemy {

    private NavMeshAgent agent;
    private List<Transform> charTransformList = new List<Transform>();
    private Transform closestEnemy;
    private Rigidbody rbody;
    public float dashForce = 50f;
    private bool agentIsStopped;
    public ParticleSystem chargeParticle;
    private Animator anim;
    private GameObject target;
    public Transform center;
    private ParticleSystem tempCharge;
    public float rotationDamping = 10f;
    private bool continueLockOn;

    //damage system
    public float maxHealth = 10f;
    private float health = 10f;
    private bool isWet;
    public ParticleSystem wetEffect;
    public ParticleSystem deathEffect;
    private bool isDead;
    public Image healthBar;
    private ParticleSystem tempWet;
    public Transform origin;

    public override string enemyName
    {
        get
        {
            return "FlyEnemy";
        }
    }

    // Use this for initialization
    void Start () {
        Health[] scripts = FindObjectsOfType<Health>();
        foreach (Health script in scripts)
        {
            charTransformList.Add(script.gameObject.transform);
        }
        rbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        closestEnemy = GetClosestEnemy(charTransformList, gameObject.transform);
        if (!agentIsStopped) {
            agent.SetDestination(closestEnemy.position);
        }else if (continueLockOn)
            LockOnToTarget(target);
	}

    Transform GetClosestEnemy(List<Transform> enemies, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    private void LockOnToTarget(GameObject rotationTarget)
    {
        Quaternion rotation = Quaternion.LookRotation(rotationTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    public void Charge() {
        tempCharge = Instantiate(chargeParticle, center.transform.position, gameObject.transform.rotation);
        tempCharge.transform.parent = center.transform;
        tempCharge.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) {
            anim.SetBool("Move", false);
            Debug.Log("In range");
            agent.enabled = false;
            target = other.gameObject;
            agentIsStopped = true;
            continueLockOn = true;
        }
    }

    public void StopLockOn() {
        Debug.Log("stop lock on");
        continueLockOn = false;
    }

    public void Attack() {
        Vector3 heading = target.gameObject.transform.position - gameObject.transform.position;
        rbody.AddForce(heading * dashForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character")) {

            collision.collider.gameObject.GetComponent<Health>().GetHitWithKnockBack(gameObject, 500);
        }
        if (!isDead)
        {
            anim.SetBool("Die", true);
            agent.enabled = false;
            Die();
            isDead = true;
        }
    }

    //Damage System
    public override void GetHit(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            //die
            if (!isDead)
            {
                anim.SetBool("Die", true);
                agent.enabled = false;
                Invoke("Die", 2f);
                isDead = true;
            }
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, origin.transform.position, deathEffect.transform.rotation);
        Destroy(gameObject);
    }

    public override void Stun()
    {
        agent.enabled = false;
        Invoke("StunRelease", 5f);
    }
    private void StunRelease()
    {
        agent.enabled = true;
    }

    public override void GetWet()
    {
        isWet = true;
        tempWet = Instantiate(wetEffect, origin.transform.position, wetEffect.transform.rotation);
        tempWet.transform.parent = origin.transform;
        tempWet.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void StopGetWet()
    {
        tempWet.Stop();
        isWet = false;
    }

    public override bool IsWet()
    {
        return isWet;
    }
}

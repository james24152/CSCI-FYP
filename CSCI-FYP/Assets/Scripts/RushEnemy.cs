using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RushEnemy : SmallEnemy {

    private List<Transform> charTransformList = new List<Transform>();
    private Transform closestEnemy;
    private NavMeshAgent agent;
    private Animator anim;
    public float maxHealth = 10f;
    public float health = 10f;
    private bool leaveEnemy;
    private GameObject attackTarget;
    private ParticleSystem tempWet;
    private bool isWet;
    private bool isDead;

    public Transform origin;
    public ParticleSystem wetEffect;
    public ParticleSystem deathEffect;
    public Image healthBar;
    public override string enemyName
    {
        get
        {
            return "Rush";
        }
    }

    // Use this for initialization
    void Start() {
        Instantiate(deathEffect, origin.transform.position, deathEffect.transform.rotation);
        Health[] scripts = FindObjectsOfType<Health>();
        foreach (Health script in scripts)
        {
            charTransformList.Add(script.gameObject.transform);
        }
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        closestEnemy = GetClosestEnemy(charTransformList, gameObject.transform);
        agent.SetDestination(closestEnemy.position);
        Debug.Log(health);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character")) {
            attackTarget = other.gameObject;
            anim.SetBool("Attack", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            agent.isStopped = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            agent.isStopped = false;
        }
    }


    public void Attack() {
        attackTarget.GetComponent<Health>().GetHitWithKnockBack(gameObject, 300f);
    }

    public void ResetAttack() {
        anim.SetBool("Attack", false);
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

    //Damage System
    public override void GetHit(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0) {
            //die
            if (!isDead) {
                anim.SetBool("Die", true);
                agent.enabled = false;
                Invoke("Die", 2f);
                isDead = true;
            }
        }
    }

    private void Die() {
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

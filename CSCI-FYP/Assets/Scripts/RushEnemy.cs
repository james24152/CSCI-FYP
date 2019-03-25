using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushEnemy : MonoBehaviour {

    private List<Transform> charTransformList = new List<Transform>();
    private Transform closestEnemy;
    private NavMeshAgent agent;
    private Animator anim;
    private float recievedDamage = 0;
    private bool leaveEnemy;
    private GameObject attackTarget;

    // Use this for initialization
    void Start() {
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
        //attackTarget.GetComponent<Health>().GetHitWithKnockBack(gameObject, 1000f);
        attackTarget.GetComponent<Health>().GetHit();
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

    //damage system
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit lava");
        switch (other.tag) {
            case "FireAttack":
            case "WaterAttack":
                recievedDamage += 0.25f;
                break;
            case "WindAttack":
                recievedDamage += 0.32f;
                break;
            case "MudAttack":
                recievedDamage++;
                break;
            case "LavaAttack":
                recievedDamage++;
                break;
            case "LightningAttack":
                recievedDamage++;
                break;
            //Tier2Element 3 left
        }
    }
}

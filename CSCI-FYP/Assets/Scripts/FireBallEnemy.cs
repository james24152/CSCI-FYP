using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireBallEnemy : MonoBehaviour {

    private List<Transform> charTransformList = new List<Transform>();
    private Transform closestEnemy;
    private GameObject target;
    private NavMeshAgent agent;
    private Animator anim;
    private float recievedDamage = 0;
    private bool leaveEnemy;
    private bool targetFound;
    private bool onCD;
    public float cd = 5f;
    private float nextFire;
    public float rotationDamping = 10;
    public GameObject fireball;
    public Transform shootPoint;
    public float force = 100f;

    // Use this for initialization
    void Start()
    {
        Health[] scripts = FindObjectsOfType<Health>();
        foreach (Health script in scripts)
        {
            charTransformList.Add(script.gameObject.transform);
        }
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCD();
        if (!targetFound)
        {
            anim.SetBool("Move", true);
            closestEnemy = GetClosestEnemy(charTransformList, gameObject.transform);
            agent.SetDestination(closestEnemy.position);
        }
        else {
            LockOnToTarget(target);
            if (!onCD)
            {
                Fire();
                nextFire = Time.time + cd;
            }
            //shoot fireball at taget
        }
    }

    private void CheckCD()
    {
        if (nextFire < Time.time)
        { //calculate AOE CD
            onCD = false;
        }
        else
            onCD = true;
    }

    private void Fire() {
        GameObject tempFireBall = Instantiate(fireball, shootPoint.position, gameObject.transform.rotation);
        tempFireBall.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * force);
    }

    private void LockOnToTarget(GameObject rotationTarget)
    {
        Quaternion rotation = Quaternion.LookRotation(rotationTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            target = other.gameObject;
            targetFound = true;
            agent.isStopped = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            target = null;
            targetFound = false;
            agent.isStopped = false;
        }
    }

    public void ResetAttack()
    {
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
        switch (other.tag)
        {
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

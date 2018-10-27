using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class AgentWaterScript : Agent
{

    private Rigidbody rbody;
    private RayPerception rayPer;
    private Vector3 startingPos;
    private Vector3 restartPos;
    private Vector3 restartRot;
    private float distance;
    private int counter = 0;
    private int wayOfDone = 0;
    private bool attackInited = false;
    private float nextTimeGetHit = 0f;
    private Animator anim;
    //wayOfDone = 1 means collide to wall, 2 means collide to goal

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public float downTime = 2f; //player down time is 2 seconds
    public float attack;
    public float speed;
    public float rayDistance = 50f;
    public GameObject target;
    public TargetScript targetScript;
    public float rotateAmount = 150f;
    public float speedScale = 8;
    public Transform center;

    public Transform[] spawns;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception>();
        distance = Vector3.Distance(transform.position, target.transform.position);
        anim = GetComponent<Animator>();
        //attributes of first spawn
    }

    public override void CollectObservations()
    {
        float[] rayAngles = { 0f, 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f, 210f, 220f, 230f, 240f, 250f, 260f, 270f, 280f, 290f, 300f, 310f, 320f, 330f, 340f, 350f };
        string[] detectableObjects = { "EnemyWall", "Entrance", "WaterEve", "FireEve", "EarthEve", "AirEve", "Agent" };//exchanged fireeve with watereve
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0.7f, 0f));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 rotateDir = Vector3.zero;

        //if output is continuous
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            rotateDir = transform.up * Mathf.Clamp(vectorAction[0], -1f, 1f);
            speed = Mathf.Clamp(vectorAction[1], 0f, 1f) * speedScale;
            attack = Mathf.Clamp(vectorAction[2], -1f, 1f);
            //if attack is below 0, don't attack; if above 0, attack
            if (attack > 0)
            {
                if (!attackInited)
                {
                    InitAttack();
                    anim.SetBool("Attack", true);
                    attackInited = true;
                }
            }
        }

        //if output is discrete
        else
        {
            //int action = Mathf.FloorToInt(vectorAction[0]);
            var action = Mathf.FloorToInt(vectorAction[0]);

            switch (action)
            {
                case 1: //turn right
                    rotateDir = transform.up * 1f;
                    break;
                case 2: //turn left
                    rotateDir = transform.up * -1f;
                    break;
                case 3: //straight
                    rotateDir = Vector3.zero;
                    break;
            }
        }

        transform.Rotate(rotateDir, Time.deltaTime * rotateAmount);

        rbody.velocity = transform.forward * speed;

        //time punishment
        AddReward(-1f / 5000f);
        //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
        if (counter == 2)
        {
            if (distance > Vector3.Distance(transform.position, target.transform.position))
            {
                AddReward(+0.0005f);
            }
            else
            {
                AddReward(-0.0002f);
            }
            counter = 0;
        }
        distance = Vector3.Distance(transform.position, target.transform.position);
        counter++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            wayOfDone = 1;
            AddReward(-0.9f);
            Done();
        }
        if (collision.gameObject.CompareTag("Agent"))
        {
            wayOfDone = 1;
            AddReward(-1f);
            Done();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWall"))
        {
            wayOfDone = 1;
            AddReward(-1f);
            Done();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (other.gameObject.CompareTag("EarthEve"))
                AddReward(0.1f);
            if (other.gameObject.CompareTag("WaterEve"))
                AddReward(0.25f);
            if (other.gameObject.CompareTag("FireEve"))
                AddReward(0.23f);
            if (other.gameObject.CompareTag("AirEve"))
                AddReward(0.15f);
            Debug.Log("entered trigger");
        }
        if (other.CompareTag("Entrance"))
        {
            wayOfDone = 2;
            Debug.Log("reached goal");
            AddReward(1f);
            Done();
        }
    }

    public override void AgentReset()
    {
        if (wayOfDone == 1)
        { //collide wall
            transform.position = restartPos;
            transform.eulerAngles = restartRot;
        }
        if (wayOfDone == 2)
        { //collide goal
            int rand = Random.Range(0, spawns.Length);
            transform.position = spawns[rand].position;
            transform.eulerAngles = restartRot;
            //update restart attributes
            restartPos = transform.position;
            restartRot = transform.eulerAngles;
        }
        if (wayOfDone == 0)
        {
            //note to self: agent will run reset once when start
            transform.position = spawns[0].position;
            restartPos = transform.position;
            restartRot = transform.eulerAngles;
        }
        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = Vector3.zero;
        player1.SetActive(true);
        player2.SetActive(true);
        player3.SetActive(true);
        player4.SetActive(true);
        player1.GetComponent<Health>().health = 2;
        player2.GetComponent<Health>().health = 2;
        player3.GetComponent<Health>().health = 2;
        player4.GetComponent<Health>().health = 2;
    }

    void InitAttack()
    {
        AddReward(-0.002f);
        Invoke("Attack", 1f);
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.SphereCast(center.transform.position, 0.1f, transform.forward, out hit, 3f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                if (Time.time > nextTimeGetHit)
                { //this is to control the hit rate of the player
                    Debug.Log("playerhit");
                    hit.transform.gameObject.GetComponent<Health>().health--;
                    if (hit.transform.CompareTag("EarthEve"))
                        AddReward(0.42f);
                    if (hit.transform.CompareTag("WaterEve"))
                        AddReward(0.48f);
                    if (hit.transform.CompareTag("FireEve"))
                        AddReward(0.48f);
                    if (hit.transform.CompareTag("AirEve"))
                        AddReward(0.48f);
                    nextTimeGetHit = Time.time + downTime;
                }
            }
        }
        attackInited = false;
        anim.SetBool("Attack", false);
    }
}

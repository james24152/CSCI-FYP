using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour {

    public ParticleSystem pushAura;
    public ParticleSystem push;
    public ParticleSystem healing;
    public ParticleSystem aoeAura;
    public ParticleSystem aoe;
    public ParticleSystem swordAura;
    public ParticleSystem proj;
    public ParticleSystem bindAura;
    public ParticleSystem bindCharAura;
    public GameObject P2AOE;
    public ParticleSystem P2AOEAura;
    public ParticleSystem P2DashTrail;
    public Transform origin;
    public int health = 100;
    public float dashForce = 100f;
    public float rotationDamping = 6.0f;
    public float aoeCD = 20f; //for boss not spamming aoe
    public float pushKnockBack = 26f;

    private float nextAOE;
    private bool aoeOnCD;
    private ParticleSystem tempAttack;
    private SphereCollider collider;
    private Animator anim;
    private bool far;
    private bool healthLow;
    private Rigidbody rBody;
    private NavMeshAgent agent;
    private GameObject rotationLockOnTarget;
    private int minHealth;
    public int state; //determines the state of boss, eg. moving, proj, aoe...
    private bool lockOn;
    private List<Collider> tempcol;
    private int swingInited = 2; //0: not inited, 1:inited, 2: onStandby

    //success rate related variables
    private int totalAttackAmount;
    private int swingAmount = 0;
    private int aoeAmount = 0;
    private int projAmount = 0;
    public int swingSuccess = 0;
    public int aoeSuccess = 0; //controlled by particle system script
    public int projSuccess = 0; //controlled by particle system script
    private float swingSuccessRate = -1;
    private float aoeSuccessRate = -1;
    private float projSuccessRate = -1;
    private bool allAttackInited;
	// Use this for initialization
    //0: Idle/movement, 1:Swing, 2:AOE, 3:Proj
	void Start () {
        collider = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("State is: " + state);
        if (state == 0) {
            Tactics();
        }
        if (lockOn)
            LockOnToTarget(rotationLockOnTarget); //constantly lock on to target if lockOn is true
        Debug.Log(swingInited);
    }

    private List<Collider> ScanEnemiesInRange(Transform center, float radius) {
        Debug.Log("scanning enemies in range");
        List<Collider> tempList = new List<Collider>();
        Collider[] hitColliders = Physics.OverlapSphere(center.position, radius);
        foreach (Collider collider in hitColliders)
            if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
                tempList.Add(collider);
        if (tempList.Count != 0)
            anim.SetBool("FarAway", false);
        else
            anim.SetBool("FarAway", true);
        return tempList;
    }
    

    private void Tactics() {
        Debug.Log("Tactics");
        tempcol = ScanEnemiesInRange(transform, 8f);
        allAttackInited = CheckAllAttackInited();
        if (allAttackInited) {
            UpdateAttackSuccessRate();
        }
        far = anim.GetBool("FarAway");
        healthLow = health < 80 ? true : false;
        Debug.Log("Health = " + health);
        if (!far) //near
        {
            Debug.Log("Near tactics");
            rotationLockOnTarget = CheckLockOn(false);
            if (healthLow)
            {
                InitiatePushAndHeal();
            }
            else {
                if (swingSuccessRate == -1)
                    swingSuccessRate = 0;
                InitiateSwing();    
            }
        }
        else { //far away
            Debug.Log("Far tactics");
            rotationLockOnTarget = CheckLockOn(true);
            if (projSuccessRate == -1 && aoeSuccessRate == -1) //No record
            {
                Debug.Log("First time AOE");
                aoeSuccessRate = 0;
                InitiateAOE();
            }
            else if (projSuccessRate == -1)
            { //proj not yet inited
                Debug.Log("First time Projectile");
                projSuccessRate = 0;
                InitiateProjectile();
            }
            else {
                //all long range has been inited
                if (swingSuccessRate == -1) //swing is not yet perfomed
                {
                    Debug.Log("first time chase weak");
                    ChaseWeak();
                }
                else {
                    CheckAOECD();
                    if (allAttackInited) { //we can use Success Rate System
                        Debug.Log("Using Success Rate System");
                        switch (mostPreferableState()) {
                            case 1:
                                swingInited = 0;
                                ChaseWeak();
                                break;
                            case 2:
                                if (!aoeOnCD) {
                                    InitiateAOE();
                                    nextAOE = Time.time + aoeCD;
                                }
                                break;
                            case 3:
                                InitiateProjectile();
                                break;
                            default:
                                Debug.Log("something wrong with longrange tactics");
                                break;
                        }
                    }
                }
            }
        }
    }

    private bool CheckAllAttackInited() {
        return (swingSuccessRate != -1 && aoeSuccessRate != -1 && projSuccessRate != -1);
    }

    private void CheckAOECD() {
        if (nextAOE < Time.time)
        { //calculate AOE CD
            Debug.Log("AOECDing");
            aoeOnCD = false;
        }
        else
            aoeOnCD = true;
    }

    private void ChaseWeak() {
        anim.SetBool("Move", true);
        Debug.Log("Chase weak");
        agent.isStopped = false;
        GameObject target = CheckLockOn(true);
        agent.destination = target.transform.position;
    }

    private int[] AvailableStates() {//need refactor... it is now hardcoded
        if (aoeOnCD)
        {
            Debug.Log("exclude AOE");
            return new int[] { 1, 3 }; //exclude AOE
        }
        else {
            Debug.Log("include AOE");
            return new int[] { 1, 2, 3 };
        }
    }

    private int mostPreferableState() { //start invoking when all the attacks are performed
        Debug.Log("swing rate" +swingSuccessRate + "aoe rate " + aoeSuccessRate + "proj rate" + projSuccessRate);
        if (swingInited == 0) {
            Debug.Log("Swing is not completed, returning 1");
            return 1;
        }
        float[] tempRates = new float[] {swingSuccessRate, aoeSuccessRate, projSuccessRate};
        float max = Mathf.Max(tempRates);
        if (swingSuccessRate <= 0.8 && aoeSuccessRate <= 0.8 && projSuccessRate <= 0.8) {
            int[] tempAvailableAttack = AvailableStates();
            Debug.Log("atttack[1] = " + tempAvailableAttack[1]);
            int ran = Random.Range(0, tempAvailableAttack.Length);
            Debug.Log("Most preferable state = " + tempAvailableAttack[ran]);
            return tempAvailableAttack[ran];
        }
        if (max == swingSuccessRate)
            return 1;
        else if (max == aoeSuccessRate)
            if (!aoeOnCD)
                return 2;
            else
                return 3; //use proj when AOE is CDing
        else if (max == projSuccessRate)
            return 3;
        return 0;
    }

    private GameObject CheckLockOn(bool far) {
        Health[] scripts = null;
        GameObject target = null;
        if (far) //Long Range
        {
            scripts = FindObjectsOfType<Health>();
            bool equalHealth = true;
            minHealth = scripts[0].health;
            target = scripts[0].gameObject;
            foreach (Health healthScript in scripts)
            {
                if (minHealth != healthScript.health)
                {
                    equalHealth = false;
                }
                if (healthScript.health < minHealth)
                {
                    target = healthScript.gameObject;
                }
            }
        }
        else { //Close Range
            bool equalHealth = true;
            minHealth = tempcol[0].gameObject.GetComponent<Health>().health;
            target = tempcol[0].gameObject;
            foreach (Collider col in tempcol)
            {
                int tempHealth = col.gameObject.GetComponent<Health>().health;
                if (minHealth != tempHealth)
                {
                    equalHealth = false;
                }
                if (tempHealth < minHealth)
                {
                    target = col.gameObject;
                }
            }
        }
        Debug.Log(target.transform.name);
        return target;
    }

    private void LockOnToTarget(GameObject rotationTarget) {
        Debug.Log("locking on to target");
        Quaternion rotation = Quaternion.LookRotation(rotationTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void UpdateAttackSuccessRate()
    {
        swingSuccessRate = (float)swingSuccess / swingAmount;
        aoeSuccessRate = (float)aoeSuccess / aoeAmount;
        projSuccessRate = (float)projSuccess / projAmount;
        totalAttackAmount = swingAmount + aoeAmount + projAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Character")) {
            switch (state) {
                case 0://movement
                    break;
            }
        }        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("boss is hit");
        health-=10;
    }

    public void ResetState() {
        if (swingInited == 1)
            swingInited = 2;
        Debug.Log("Reset State");
        state = 0;
    }

    public void InitiateProjectile() {
        Debug.Log("Using projtile");
        agent.isStopped = true;
        lockOn = true;
        state = 3;
        anim.SetBool("UseProj", true);
        projAmount++;
    }

    public void InitiateSwing()
    {
        swingInited = 1;
        anim.SetBool("Move", false);
        Debug.Log("using swing");
        agent.isStopped = true;
        lockOn = true;
        state = 1;
        anim.SetBool("Swing", true);
        swingAmount++;
    }

    public void SwingStopLockOn() {
        Debug.Log("stop swing lock on");
        lockOn = false;
        anim.SetBool("Swing", false);
    }

    public void InitiateAOE()
    {
        Debug.Log("init aoe");
        state = 2;
        anim.SetBool("UseAOE", true);
        aoeAmount++;
    }

    public void InitiatePushAndHeal()
    {
        anim.SetBool("Move", false);
        Debug.Log("Push and Heal");
        agent.isStopped = true;
        state = 4;
        anim.SetBool("NeedHeal", true);
    }

    public void FireProj()
    {
        lockOn = false;
        proj.Play();
        anim.SetBool("UseProj", false);
    }

    public void SwordCharge() {
        anim.SetBool("Swing", false);
        swordAura.Play();
    }

    public void PushCharge() {
        tempAttack = Instantiate(pushAura, origin.transform.position, pushAura.transform.rotation);
        Destroy(tempAttack, 7f);
    }
    public void Push()
    {
        List<Collider> tempPushList = ScanEnemiesInRange(transform, 8f);
        foreach (Collider col in tempPushList) {
            Vector3 tempKnockDir = new Vector3(col.transform.position.x - transform.position.x, 10f, col.transform.position.z - transform.position.z);
            col.GetComponent<Rigidbody>().AddForce(tempKnockDir * pushKnockBack);
        }
        tempAttack = Instantiate(push, origin.transform.position, push.transform.rotation);

        Destroy(tempAttack, 10f);
    }

    public void Heal()
    {
        tempAttack = Instantiate(healing, origin.transform.position, healing.transform.rotation);
        health += 20;
        Destroy(tempAttack, 10f);
        anim.SetBool("NeedHeal", false);
    }

    public void AOECharge()
    {
        tempAttack = Instantiate(aoeAura, origin.transform.position, aoeAura.transform.rotation);
        Destroy(tempAttack, 10f);
    }

    public void AOE()
    {
        tempAttack = Instantiate(aoe, origin.transform.position, aoe.transform.rotation);
        tempAttack.GetComponentInChildren<BossParticleHOM>().master = gameObject;
        anim.SetBool("UseAOE", false);
    }

    public void P2Bind() {
        tempAttack = Instantiate(bindAura, origin.transform.position, bindAura.transform.rotation);
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
        ParticleSystem tempCharAura;
        foreach (ElementCombineBehaviour script in scripts)
        {
            
            tempCharAura= Instantiate(bindCharAura, script.origin.transform.position, bindCharAura.transform.rotation);
            tempCharAura.transform.parent = script.origin.transform;
        }
    }

    public void P2DashAttack() {
        tempAttack = Instantiate(P2DashTrail, origin.transform.position, P2DashTrail.transform.rotation);
        tempAttack.transform.parent = origin;
        rBody.AddForce(transform.forward * dashForce); //take care of residue particle?
        Debug.Log("dash attack");
    }

    public void P2AOECharge() {
        tempAttack = Instantiate(P2AOEAura, origin.transform.position, P2AOEAura.transform.rotation);
    }

    public void P2AOEAttack() {
        GameObject tempAttack = Instantiate(P2AOE, origin.transform.position, gameObject.transform.rotation);
    }

    public void P2MinionAttack() {
        //yet to implement
        Debug.Log("minionAttack");
    }

}

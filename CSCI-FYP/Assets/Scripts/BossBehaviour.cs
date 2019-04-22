using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossBehaviour : SmallEnemy {

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
    public Transform projPoint;
    public Transform origin;
    public float health = 100;
    public float dashForce = 100f;
    public float rotationDamping = 6.0f;
    public float aoeCD = 20f; //for boss not spamming aoe
    public float pushKnockBack = 26f;
    public DialogueTrigger trigger;

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
    private int dashInited = 2; //0: not inited, 1:inited, 2: onStandby
    public bool P2;
    private AudioManager audioManager;

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
    //For P2
    private int p2totalAttackAmount;
    private int dashAmount = 0;
    private int p2aoeAmount = 0;
    private int bindAmount = 0;
    public int dashSuccess = 0;
    public int p2aoeSuccess = 0; //controlled by particle system script
    public int bindSuccess = 0; //controlled by particle system script
    private float dashSuccessRate = -1;
    private float p2aoeSuccessRate = -1;
    private float bindSuccessRate = -1;
    private bool p2allAttackInited;
    private bool resetAnim;
    //Damage System
    private bool isDead;
    private bool isWet;
    public float damageTaken;
    private bool flinched;
    private bool flinchedTwice;
    private ParticleSystem tempWet;
    public ParticleSystem wetEffect;
    public Transform headPoint;
    public float maxHealth;
    public Image healthBar;
    public override string enemyName
    {
        get
        {
            return "Boss";
        }
    }

    // Use this for initialization
    //0: Idle/movement, 1:Swing, 2:AOE, 3:Proj
    void Start() {
        collider = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update() {
        CheckFlinch();
        Debug.Log("State is: " + state);
        if (state == 0) {
            Debug.Log("tactics");
            Tactics();
        }
        if (lockOn)
            LockOnToTarget(rotationLockOnTarget); //constantly lock on to target if lockOn is true
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
        if (health < 100) {
            //Phase 2 begins
            P2 = true;
            anim.SetBool("P2", true);
            if (!resetAnim) {
                ResetAnim();
                resetAnim = true;
            }
        }
        tempcol = ScanEnemiesInRange(transform, 8f);
        allAttackInited = CheckAllAttackInited();
        p2allAttackInited = CheckAllAttackInited();
        if (!P2)
        {
            if (allAttackInited)
            {
                UpdateAttackSuccessRate();
            }
        }
        else {
            if (p2allAttackInited)
                P2UpdateAttackSuccessRate();
        }
        far = anim.GetBool("FarAway");
        healthLow = health < 150 ? true : false;
        Debug.Log("Health = " + health);
        if (!far) //near
        {
            Debug.Log("Near tactics");
            rotationLockOnTarget = CheckLockOn(false);
            if (!P2)
            {
                if (healthLow)
                {
                    InitiatePushAndHeal();
                }
                else
                {
                    if (swingSuccessRate == -1)
                        swingSuccessRate = 0;
                    InitiateSwing();
                }
            }
            else {
                if (dashSuccessRate == -1)
                    dashSuccessRate = 0;
                InitiateP2Dash();
            }
        }
        else { //far away
            Debug.Log("Far tactics");
            rotationLockOnTarget = CheckLockOn(true);
            if (!P2) //Phase 1
            {
                if (projSuccessRate == -1 && aoeSuccessRate == -1) //No record
                {
                    aoeSuccessRate = 0;
                    InitiateAOE();
                }
                else if (projSuccessRate == -1)
                { //proj not yet inited
                    projSuccessRate = 0;
                    InitiateProjectile();
                }
                else
                {
                    //all long range has been inited
                    if (swingSuccessRate == -1) //swing is not yet perfomed
                    {
                        ChaseWeak();
                    }
                    else
                    {
                        CheckAOECD();
                        if (allAttackInited)
                        { //we can use Success Rate System
                            switch (mostPreferableState())
                            {
                                case 1:
                                    swingInited = 0;
                                    ChaseWeak();
                                    break;
                                case 2:
                                    if (!aoeOnCD)
                                    {
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
            else { //Entered Phase 2
                if (bindSuccessRate == -1 && p2aoeSuccessRate == -1) //No record
                {
                    Debug.Log("First time P2AOE");
                    p2aoeSuccessRate = 0;
                    InitiateP2AOE();
                }
                else if (bindSuccessRate == -1)
                { //proj not yet inited
                    Debug.Log("First time bind");
                    bindSuccessRate = 0;
                    InitiateP2Bind();
                }
                else
                {
                    //all long range has been inited
                    if (dashSuccessRate == -1) //swing is not yet perfomed
                    {
                        Debug.Log("first time chase weak and dash");
                        ChaseWeak();
                    }
                    else
                    {
                        CheckAOECD();
                        if (p2allAttackInited)
                        { //we can use Success Rate System
                            Debug.Log("Using Success Rate System");
                            switch (mostPreferableState())
                            {
                                case 1:
                                    dashInited = 0;
                                    ChaseWeak();
                                    break;
                                case 2:
                                    if (!aoeOnCD)
                                    {
                                        InitiateP2AOE();
                                        nextAOE = Time.time + aoeCD;
                                    }
                                    break;
                                case 3:
                                    InitiateP2Bind();
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
    }

    private void ResetAnim() {
        anim.SetBool("Move", false);
        anim.SetBool("NeedHeal", false);
        anim.SetBool("UseAOE", false);
        anim.SetBool("UseProj", false);
        anim.SetBool("Move", false);
    }

    private bool CheckAllAttackInited() {
        if (!P2)
            return (swingSuccessRate != -1 && aoeSuccessRate != -1 && projSuccessRate != -1);
        return (dashSuccessRate != -1 && p2aoeSuccessRate != -1 && bindSuccessRate != -1);
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

    private int[] P2AvailableStates()
    {//need refactor... it is now hardcoded
        if (aoeOnCD)
        {
            Debug.Log("exclude P2AOE");
            return new int[] { 5, 7 }; //exclude AOE
        }
        else
        {
            Debug.Log("include P2AOE");
            return new int[] { 5, 6, 7 };
        }
    }

    private int mostPreferableState() { //start invoking when all the attacks are performed
        if (!P2) //Phase 1
        {
            Debug.Log("swing rate" + swingSuccessRate + "aoe rate " + aoeSuccessRate + "proj rate" + projSuccessRate);
            if (swingInited == 0)
            {
                Debug.Log("Swing is not completed, returning 1");
                return 1;
            }
            float[] tempRates = new float[] { swingSuccessRate, aoeSuccessRate, projSuccessRate };
            float max = Mathf.Max(tempRates);
            if (swingSuccessRate <= 0.8 && aoeSuccessRate <= 0.8 && projSuccessRate <= 0.8)
            {
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
        }
        else { //Entered Phase 2
            Debug.Log("dash rate" + dashSuccessRate + "P2aoe rate " + p2aoeSuccessRate + "bind rate" + bindSuccessRate);
            if (dashInited == 0)
            {
                Debug.Log("Dash is not completed, returning 1");
                return 1;
            }
            float[] tempRates = new float[] { dashSuccessRate, p2aoeSuccessRate, bindSuccessRate };
            float max = Mathf.Max(tempRates);
            if (dashSuccessRate <= 0.8 && p2aoeSuccessRate <= 0.8 && bindSuccessRate <= 0.8)
            {
                int[] tempAvailableAttack = AvailableStates();
                int ran = Random.Range(0, tempAvailableAttack.Length);
                Debug.Log("Most preferable state = " + tempAvailableAttack[ran]);
                return tempAvailableAttack[ran];
            }
            if (max == dashSuccessRate)
                return 1;
            else if (max == p2aoeSuccessRate)
                if (!aoeOnCD)
                    return 2;
                else
                    return 3; //use proj when AOE is CDing
            else if (max == bindSuccessRate)
                return 3;
        }
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

    private void P2UpdateAttackSuccessRate()
    {
        dashSuccessRate = (float)dashSuccess / dashAmount;
        p2aoeSuccessRate = (float)p2aoeSuccess / p2aoeAmount;
        bindSuccessRate = (float)bindSuccess / bindAmount;
        p2totalAttackAmount = dashAmount + p2aoeAmount + bindAmount;
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

    public void ResetState() {
        if (swingInited == 1)
            swingInited = 2;
        Debug.Log("Reset State");
        state = 0;
    }

    public void P2ResetState()              //not yet used
    {
        if (dashInited == 1)
            dashInited = 2;
        Debug.Log("P2 Reset State");
        state = 0;
    }

    public void InitiateProjectile() {
        audioManager.Play("BossRoar");
        Debug.Log("Using projtile");
        agent.isStopped = true;
        lockOn = true;
        state = 3;
        anim.SetBool("UseProj", true);
        projAmount++;
    }

    public void InitiateSwing()
    {
        audioManager.Play("BossRoar");
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
        audioManager.Play("BossRoar");
        Debug.Log("init aoe");
        state = 2;
        lockOn = true;
        anim.SetBool("UseAOE", true);
        aoeAmount++;
    }

    public void InitiatePushAndHeal()
    {
        audioManager.Play("BossRoar");
        anim.SetBool("Move", false);
        Debug.Log("Push and Heal");
        lockOn = true;
        agent.isStopped = true;
        state = 4;
        anim.SetBool("NeedHeal", true);
    }

    public void FireProj()
    {
        audioManager.Play("BossProj");
        lockOn = false;
        ParticleSystem tempProj = Instantiate(proj, projPoint.transform.position, gameObject.transform.rotation);
        tempProj.gameObject.GetComponent<BossParticleHOM>().master = gameObject;
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
            Vector3 knockBackDir = Vector3.Normalize(col.transform.position - gameObject.transform.position);
            Vector3 knockBackForce = new Vector3(knockBackDir.x * pushKnockBack, 100, knockBackDir.z * pushKnockBack);
            col.GetComponent<Rigidbody>().AddForce(knockBackForce);
        }
        tempAttack = Instantiate(push, origin.transform.position, push.transform.rotation);

        Destroy(tempAttack, 10f);
    }

    public void Heal()
    {
        audioManager.Play("BossHeal");
        tempAttack = Instantiate(healing, origin.transform.position, healing.transform.rotation);
        health += 20;
        healthBar.fillAmount = health / maxHealth;
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
        audioManager.Play("BossAOE");
        tempAttack = Instantiate(aoe, origin.transform.position, aoe.transform.rotation);
        tempAttack.GetComponentInChildren<BossParticleHOM>().master = gameObject;
        anim.SetBool("UseAOE", false);
    }

    //P2 Initiate

    public void InitiateP2Dash() {
        audioManager.Play("BossRoar");
        Debug.Log(rotationLockOnTarget);
        dashInited = 1;
        anim.SetBool("Move", false);
        Debug.Log("using dash");
        agent.isStopped = true;
        lockOn = true;
        state = 5;
        anim.SetBool("Dash", true);
        dashAmount++;
    }

    public void DashStopLockOn()
    {
        Debug.Log("stop dash lock on");
        lockOn = false;
        anim.SetBool("Dash", false);
    }

    public void InitiateP2Bind() {
        audioManager.Play("BossRoar");
        Debug.Log("P2Bind");
        lockOn = true;
        agent.isStopped = true;
        state = 7;
        anim.SetBool("Bind", true);
        bindAmount++;
    }

    public void InitiateP2AOE()
    {
        audioManager.Play("BossRoar");
        Debug.Log("P2 aoe");
        lockOn = true;
        state = 6;
        anim.SetBool("P2AOE", true);
        p2aoeAmount++;
    }

    public void InitiateP2SpawnMinion() {   //not used
        audioManager.Play("BossRoar");
        anim.SetBool("Summon", true);
        Debug.Log("spawning minion");
        agent.isStopped = true;
        state = 8;
    }

    public void P2Bind() {
        audioManager.Play("BossP2Bind");
        anim.SetBool("Bind", false);
        tempAttack = Instantiate(bindAura, origin.transform.position, bindAura.transform.rotation);
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
        ParticleSystem tempCharAura;
        foreach (ElementCombineBehaviour script in scripts)
        {
            
            tempCharAura = Instantiate(bindCharAura, script.origin.transform.position, bindCharAura.transform.rotation);
            tempCharAura.transform.parent = script.origin.transform;
        }
    }

    public void StunPlayers() {
        bool binded = false;
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
        foreach (ElementCombineBehaviour script in scripts)
        {
            if (script.gameObject.GetComponent<Animator>().GetBool("Grounded"))
            {
                script.gameObject.GetComponent<FireBlowController>().enabled = false;
                binded = true;
            }
            else {
                script.origin.transform.Find("BossP2BindAuraChar(Clone)").transform.parent = null; //release particle effect from character
            }
        }
        if (binded) {
            Invoke("ReleaseStunPlayers", 10f);
            bindSuccess++;
        }
    }

    private void ReleaseStunPlayers() {
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
        foreach (ElementCombineBehaviour script in scripts)
        {
            script.gameObject.GetComponent<FireBlowController>().enabled = true;
        }
    }

    public void P2DashAttack() {
        //temporary disable collision between lockon target
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("BossDashPhase");
        tempAttack = Instantiate(P2DashTrail, origin.transform.position, P2DashTrail.transform.rotation);
        tempAttack.transform.parent = origin;
        audioManager.Play("BossDash");
        tempAttack.gameObject.GetComponent<BossParticleHOM>().master = gameObject;
        rBody.AddForce(transform.forward * dashForce); //take care of residue particle?
        anim.SetBool("Dash", false);
        Debug.Log("dash attack");
    }

    public void ResetCollision() {
        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Boss");
    }

    public void P2AOECharge() {
        tempAttack = Instantiate(P2AOEAura, origin.transform.position, P2AOEAura.transform.rotation);
    }

    public void P2AOEAttack() {
        audioManager.Play("BossP2AOE");
        GameObject tempAttack = Instantiate(P2AOE, origin.transform.position, gameObject.transform.rotation);
        BossParticleHOM[] homs = tempAttack.GetComponentsInChildren<BossParticleHOM>();
        foreach (BossParticleHOM hom in homs) {
            hom.master = gameObject;
        }
        anim.SetBool("P2AOE", false);
    }

    public void P2MinionAttack() {
        //yet to implement
        anim.SetBool("Summon", false);
        Debug.Log("minionAttack");
    }

    private void CheckFlinch() {
        if (damageTaken >= 30) {
            if (!flinched)
            {
                audioManager.Play("BossRoar");
                anim.SetBool("Flinch", true); //first flinch
                flinched = true;
            }
            else if (damageTaken >= 120) {
                if (!flinchedTwice) {
                    audioManager.Play("BossRoar");
                    anim.SetBool("Flinch", true); //second flinch
                    flinchedTwice = true;
                }
            }
        }
    }
    public override void GetHit(float damage)
    {
        health -= damage;
        damageTaken += damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            //die
            if (!isDead)
            {
                audioManager.Play("BossRoar");
                anim.SetBool("Die", true);
                agent.enabled = false;
                Invoke("Cutscene", 6f);
                isDead = true;
            }
        }
    }

    private void Cutscene() {
        trigger.TriggerDialogue();
    }

    public void UnFlinch() {
        anim.SetBool("Flinch", false);
    }

    public override void Stun()
    {
        Debug.Log("stun");
        agent.speed = 1f; //possibly need tweaking
        Invoke("StunRelease", 15f);
    }
    private void StunRelease()
    {
        agent.speed = 3.5f;
    }

    public override void GetWet()
    {
        isWet = true;
        tempWet = Instantiate(wetEffect, headPoint.transform.position, wetEffect.transform.rotation);
        tempWet.transform.parent = headPoint.transform;
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

    public void TakePose() {
        agent.isStopped = true;
    }
}

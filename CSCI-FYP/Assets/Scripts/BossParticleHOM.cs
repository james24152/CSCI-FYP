using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticleHOM : MonoBehaviour {

    public GameObject master;
    private bool hit;

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Character")) {
            BossBehaviour bossScript = master.GetComponent<BossBehaviour>();
            if (!hit) {
                switch (gameObject.tag) {
                    case "AOESmoke":
                        Debug.Log("AOE hit");
                        other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 600f);
                        bossScript.aoeSuccess++;
                        break;
                    case "BossProj":
                        Debug.Log("Proj hit");
                        other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 600f);
                        if (!bossScript.P2)
                            bossScript.projSuccess++;
                        else
                            bossScript.p2aoeSuccess++;
                        break;
                    case "DashTrail":
                        Debug.Log("Dash hit");
                        other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 0f);
                        bossScript.dashSuccess++;
                        break;
                }
                hit = true;
            }
        }
    }

    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}

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
                        bossScript.aoeSuccess++;
                        break;
                    case "BossProj":
                        Debug.Log("Proj hit");
                        bossScript.projSuccess++;
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

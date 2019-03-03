using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopBehaviour : MonoBehaviour {
    // Use this for initialization
    public GameObject master;
    private Animator anim;
    void OnParticleSystemStopped() {
        if (master != null) {
            ElementCombineBehaviour script = master.GetComponent<ElementCombineBehaviour>();
            script.StopAura();
            anim = master.GetComponent<Animator>();
            anim.SetBool("SecondTierAttack", false);
        }
            Destroy(gameObject);
    }
}

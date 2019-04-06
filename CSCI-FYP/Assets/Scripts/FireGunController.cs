using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunController : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Character")) {
            other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 0f);
        }
    }
}

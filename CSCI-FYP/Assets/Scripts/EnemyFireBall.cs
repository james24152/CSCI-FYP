using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit fireball");
        if (other.layer == LayerMask.NameToLayer("Character")) {
            other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 1000);
        }
        Destroy(gameObject);
    }
}

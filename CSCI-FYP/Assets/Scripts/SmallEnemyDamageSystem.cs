using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyDamageSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    //damage system
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit small enemy");
        float damage = DamageModel.DamageInflicted(other.tag, other, gameObject);
        GetComponent<SmallEnemy>().GetHit(damage);
    }
}

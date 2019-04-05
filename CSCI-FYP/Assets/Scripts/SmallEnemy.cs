using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class SmallEnemy: MonoBehaviour {

    public abstract string enemyName { get; }

    public abstract void GetHit(float damage);

    public abstract void Stun();

    public abstract void GetWet();
    public abstract void StopGetWet();
    public abstract bool IsWet();
}

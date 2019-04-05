using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordHitBox : MonoBehaviour {

    public BossBehaviour boss;
    public float knockBack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (boss.state == 1)//swinging
            {
                Debug.Log("Swing hits!");
                boss.swingSuccess++;
                GameObject tempcol = other.gameObject;
                Debug.Log("swing hit " + tempcol.gameObject);
                other.GetComponent<Health>().GetHitWithKnockBack(gameObject, 1000f);
            }
        }
    }
}

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
                Vector3 tempKnockDir = new Vector3(tempcol.transform.position.x - boss.transform.position.x, 10f, tempcol.transform.position.z - boss.transform.position.z);
                tempcol.GetComponent<Rigidbody>().AddForce(tempKnockDir * knockBack);
            }
        }
    }
}

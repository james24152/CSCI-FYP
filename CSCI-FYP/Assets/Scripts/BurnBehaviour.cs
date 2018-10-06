using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBehaviour : MonoBehaviour
{

    public GameObject fireFx;
    public GameObject firePoint;
    private GameObject tempFire;
    private bool alreadyInstantiated = false;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("FireAttack"))
        {
            if (alreadyInstantiated == false) {
                tempFire = Instantiate(fireFx, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
                alreadyInstantiated = true;
                Destroy(gameObject, 3f);
                Destroy(tempFire, 3f);
            }
        }
    }
}

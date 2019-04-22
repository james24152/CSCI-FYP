using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour {

    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            spawn();
    }
    // Use this for initialization
    public void spawn() {
        boss.gameObject.SetActive(true);
    }
}

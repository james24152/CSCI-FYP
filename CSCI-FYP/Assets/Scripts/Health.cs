using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Use this for initialization
    public int health = 2;

    private void Update()
    {
        if (health == 0)
            gameObject.SetActive(false);
    }
}

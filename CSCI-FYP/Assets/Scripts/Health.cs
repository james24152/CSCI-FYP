using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Use this for initialization
    public int downTime = 4;
    public int health = 5;
    private bool invoked;
    private Animator anim;
    private float nextTimeGetHit = 0f;
    private MoveBehaviour moveScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
    }

    public bool GetHit() {
        if (Time.time > nextTimeGetHit) {
            health--;
            if (!invoked)
            {
                anim.SetBool("Damaged", true);
                invoked = true;
                Invoke("SetDamagedFalse", 0.2f);
            }
            if (health == 0)
                gameObject.SetActive(false);
            nextTimeGetHit = Time.time + downTime;
            return true;
        }
        return false;
    }


    private void SetDamagedFalse() {
        anim.SetBool("Damaged", false);
        invoked = false;
    }
}

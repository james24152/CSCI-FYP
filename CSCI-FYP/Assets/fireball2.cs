using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball2 : MonoBehaviour {
    Rigidbody rb;
    public float force;
    private float timer;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, -1*force), ForceMode.Force);
        print("add force2");
        timer = Time.time + 7.0f;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timer)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Character"))
        {
            obj.GetComponent<Health>().GetHitWithKnockBack(gameObject, 0f);
        }
    }
}

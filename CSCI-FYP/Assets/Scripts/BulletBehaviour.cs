using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float damage;
    GameObject hit;
    GameObject impactGO;
    Rigidbody tempRigid;
    public GameObject explosion;
    public float impactForce = 1000;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Use this for initialization

    private void OnCollisionEnter(Collision collision)
    {
        hit = collision.collider.gameObject;
        /*if (hit.CompareTag("Target")) {
            Target target = hit.GetComponent<Target>();
            target.TakeDamage(damage);
            tempRigid = hit.GetComponent<Rigidbody>();
            tempRigid.AddForce(-collision.contacts[0].normal * impactForce);
        }*/
        if (hit.transform.name != "Earth Eve")
        {
            //Debug.Log(hit.gameObject.transform.name);
            audioManager.Play("ShootMud");
            Destroy(gameObject);
            
        }
        else {
            if (hit.transform.name == "Earth Eve")
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}

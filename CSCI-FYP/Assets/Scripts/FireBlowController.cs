using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlowController : MonoBehaviour
{

    public float punchSpeed = 0;
    public ParticleSystem fireHandL;
    public GameObject shootPoint;
    public GameObject bullet;
    public ParticleSystem fireHandR;
    public ParticleSystem fire;
    public float bulletForce;
    public float range = 10;
    public float damage = 10;

    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = Input.GetButton("Fire1");
        animator.SetBool("Punch", shoot);
        if (shoot)
        {
            Shoot();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            transform.Translate(transform.forward * punchSpeed * Time.deltaTime, Space.World);
        }

    }

    void Shoot()
    {
        fireHandL.Play();
        fireHandR.Play();
    }

    void Fire()
    {
        /*GameObject tempBullet;
        tempBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;

        tempBullet.transform.Rotate(Vector3.back * 90);

        Rigidbody tempRigid;
        tempRigid = tempBullet.GetComponent<Rigidbody>();

        tempRigid.AddForce(transform.forward * bulletForce);
        */
        fire.Play();

    }
}


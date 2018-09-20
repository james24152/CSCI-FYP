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
    public string punchButton;
    public Camera cam;

    private MoveBehaviour moveScript;
    

    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = (Input.GetAxis(punchButton) != 0);
        animator.SetBool("Punch", shoot);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") || animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            moveScript.walkSpeed = 1.5f;
            moveScript.runSpeed = 1.5f;
        }
        else
        {
            moveScript.walkSpeed = 4;
            moveScript.runSpeed = 4;
        }


    }

    void Cast()
    {
        fireHandL.Play();
        fireHandR.Play();
    }

    void Fire()
    {
        fire.Play();
    }
    void FireBear()
    {
        GameObject tempBullet;
        tempBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;

        tempBullet.transform.Rotate(Vector3.back * 90);

        Rigidbody tempRigid;
        tempRigid = tempBullet.GetComponent<Rigidbody>();

        tempRigid.AddForce(cam.transform.forward * bulletForce);
        
        //fire.Play();

    }
}


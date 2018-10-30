using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class FireBlowController : MonoBehaviour
{
    public XboxController joystick;
    public ParticleSystem fireHandL;
    public GameObject shootPoint;
    public GameObject bullet;
    public ParticleSystem fireHandR;
    public ParticleSystem fire;
    public float bulletForce;
    public float range = 10;
    public float damage = 10;
    public XboxAxis punchButton;
    public Camera cam;

    private MoveBehaviour moveScript;
    

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<MoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = (XCI.GetAxis(punchButton, joystick) != 0);
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
        if (animator.GetBool("Aim"))
            tempRigid.AddForce(cam.transform.forward * bulletForce);
        else
            tempRigid.AddForce(transform.forward * bulletForce);

        //fire.Play();

    }
}


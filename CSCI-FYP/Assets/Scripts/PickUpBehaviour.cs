using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour {

    private bool pickButtonPressed;
    private bool pickUp = false;
    private Animator anim;
    public string pickUpButton;
    public ParticleSystem fireHandL;
    public ParticleSystem fireHandR;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        pickButtonPressed = Input.GetButtonDown(pickUpButton);
        if (pickButtonPressed && !pickUp) {
            pickUp = true;
            anim.SetBool("PickUp", true);
        }else if (pickButtonPressed && pickUp) {
            pickUp = false;
            anim.SetBool("PickUp", false);
        }
    }

    void BurnHand()
    {
        fireHandL.Play();
        fireHandR.Play();
    }
}

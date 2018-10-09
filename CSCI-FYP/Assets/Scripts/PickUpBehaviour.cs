﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PickUpBehaviour : MonoBehaviour {

    public XboxController joystick;
    private bool pickButtonPressed;
    private bool pickUp = false;
    private bool grab = false;
    private Animator anim;
    private int layerMask;
    public XboxButton pickUpButton;
    public ParticleSystem fireHandL;
    public ParticleSystem fireHandR;
    public Camera playerCam;
    public float itemHeight;
    public Transform bindingHand;
    public Transform bindingHand2;

    private Vector3 grabPosition;
    private Vector3 grabRotation;
    private GameObject itemGrabbed;
    const float k_Spring = 100.0f;
    const float k_Damper = 5.0f;
    const float k_Drag = 5.0f;
    const float k_AngularDrag = 2.0f;
    const float k_Distance = 0.0f;
    const bool k_AttachToCenterOfMass = false;

    private SpringJoint m_SpringJoint;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Environment", "Grabs");
    }
	
	// Update is called once per frame
	void Update () {
        pickButtonPressed = XCI.GetButtonDown(pickUpButton, joystick);
        if (pickButtonPressed && !pickUp && !grab) //pressed X button and nothing holding
        {
            var mainCamera = playerCam;

            // We need to actually hit an object
            RaycastHit hit = new RaycastHit();
            if (
                !Physics.Raycast(mainCamera.transform.position,
                                 mainCamera.transform.forward, out hit, 100,
                                 layerMask))
            {
                Debug.Log("returned");
                return;
            }
            Debug.Log(hit.transform.name);
            // We need to hit a rigidbody that is not kinematic
            if (!hit.rigidbody || hit.rigidbody.isKinematic)
            {
                return;
            }
            Debug.Log(hit.transform.name);
            //determine if hit is a grabs or just environment
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grabs"))
            {
                grab = true;
                anim.SetBool("Grab", true);
                hit.transform.parent = bindingHand;
                if (hit.transform.gameObject.CompareTag("Axe"))
                {
                    if (gameObject.CompareTag("EarthEve")) {
                        grabPosition = new Vector3(-0.2438f, -0.008f, 0.1127f);
                        grabRotation = new Vector3(-66.08501f, -279.57f, 92.494f);
                    }
                    else if (gameObject.CompareTag("WaterEve"))
                    {
                        grabPosition = new Vector3(-0.079f, 0.075f, 0.133f);
                        grabRotation = new Vector3(2.926f, -193.964f, 0.58f);
                    }
                    else if (gameObject.CompareTag("FireEve"))
                    {
                        grabPosition = new Vector3(-0.641f, 0.0f, 0.196f);
                        grabRotation = new Vector3(-54.713f, 104.347f, 76.604f);
                    }
                    else if (gameObject.CompareTag("AirEve"))
                    {
                        grabPosition = new Vector3(-0.013f, 0.065f, 0.002f);
                        grabRotation = new Vector3(-2.765f, -215.814f, -6.007f);
                    }
                    itemGrabbed = hit.transform.gameObject;
                    hit.collider.enabled = false; //turn off collider
                    StartCoroutine(ItemFollowHand(hit.transform.gameObject));
                }
            }
            else if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("Environment")))
            {
                pickUp = true;
                anim.SetBool("PickUp", true);
                if (!m_SpringJoint)
                {
                    var go = new GameObject("Rigidbody dragger");
                    Rigidbody body = go.AddComponent<Rigidbody>();
                    m_SpringJoint = go.AddComponent<SpringJoint>();
                    body.isKinematic = true;
                }

                m_SpringJoint.transform.position = hit.point;
                m_SpringJoint.anchor = Vector3.zero;

                m_SpringJoint.spring = k_Spring;
                m_SpringJoint.damper = k_Damper;
                m_SpringJoint.maxDistance = k_Distance;
                m_SpringJoint.connectedBody = hit.rigidbody;

                StartCoroutine("DragObject", hit.distance);
            }
        }
        else if (pickButtonPressed && pickUp) //pressed x to drop
        { 
            pickUp = false;
            anim.SetBool("PickUp", false);
        }
        else if (pickButtonPressed && grab) {
            grab = false;
            anim.SetBool("Grab", false);
            itemGrabbed.GetComponent<Collider>().enabled = true; //turn collider back on
            itemGrabbed.transform.parent = null;
            itemGrabbed.transform.localPosition = bindingHand2.transform.position;
            //itemGrabbed.transform.localEulerAngles = bindingHand2.transform.localEulerAngles;
            itemGrabbed = null;
        }
    }

    void BurnHand()
    {
        fireHandL.Play();
        fireHandR.Play();
    }


    private IEnumerator DragObject(float distance)
    {
        var oldDrag = m_SpringJoint.connectedBody.drag;
        var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
        var mainCamera = playerCam;
        while (pickUp == true)
        {
            var ray = new Ray(transform.position, mainCamera.transform.forward);
            m_SpringJoint.transform.position = new Vector3(ray.GetPoint(distance).x, ray.GetPoint(distance).y + itemHeight, ray.GetPoint(distance).z);
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
    }

    private IEnumerator ItemFollowHand(GameObject item) {
        while (grab == true) {
            item.transform.localPosition = grabPosition;
            item.transform.localEulerAngles = grabRotation;
            yield return null;
        }
    }

}

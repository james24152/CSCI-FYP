using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour {

    private bool pickButtonPressed;
    private bool pickUp = false;
    private Animator anim;
    private int layerMask;
    public string pickUpButton;
    public ParticleSystem fireHandL;
    public ParticleSystem fireHandR;
    public Camera playerCam;
    public float itemHeight;

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
        layerMask = LayerMask.GetMask("Environment");
    }
	
	// Update is called once per frame
	void Update () {
        pickButtonPressed = Input.GetButtonDown(pickUpButton);
        if (pickButtonPressed && !pickUp)
        {
            pickUp = true;
            anim.SetBool("PickUp", true);
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
        else if (pickButtonPressed && pickUp)
        {
            pickUp = false;
            anim.SetBool("PickUp", false);
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

}

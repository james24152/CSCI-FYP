using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

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
    public bool forceEject = false;
    public Image popUp;
    public bool isInKeyTrigger;

    private Vector3 grabPosition;
    private Vector3 grabRotation;
    public GameObject itemGrabbed;
    private Grabber grabber;
    private AudioManager audioMangaer;
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
        layerMask = LayerMask.GetMask("PickUp", "Grabs");
        audioMangaer = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitCheck = new RaycastHit();
        if (
            Physics.Raycast(playerCam.transform.position,
                             playerCam.transform.forward, out hitCheck, 4,
                             layerMask) || isInKeyTrigger)
        {
            if (!grab && !pickUp)
                popUp.gameObject.SetActive(true);
        }else
            popUp.gameObject.SetActive(false);

        pickButtonPressed = XCI.GetButtonDown(pickUpButton, joystick);
        if (pickButtonPressed && !pickUp && !grab) //pressed X button and nothing holding
        {
            var mainCamera = playerCam;

            // We need to actually hit an object
            RaycastHit hit = new RaycastHit();
            if (
                !Physics.Raycast(mainCamera.transform.position,
                                 mainCamera.transform.forward, out hit, 4,
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
                audioMangaer.Play("Grab");
                hit.transform.parent = bindingHand;
                if (hit.transform.gameObject.CompareTag("Axe"))
                {
                    if (gameObject.transform.name == "Earth Eve" || gameObject.transform.name == "Earth Eve(Clone)") {
                        grabPosition = new Vector3(-0.019f, -0.109f, 0.059f);
                        grabRotation = new Vector3(-79.22f, 141.87f, 22.042f);
                    }
                    else if (gameObject.transform.name == "Water Eve" || gameObject.transform.name == "Water Eve(Clone)")
                    {
                        grabPosition = new Vector3(-0.001736604f, 0.07012841f, 0.07449407f);
                        grabRotation = new Vector3(3.849f, -192.886f, 357.794f);
                    }
                    else if (gameObject.transform.name == "Fire Eve" || gameObject.transform.name == "Fire Eve(Clone)")
                    {
                        grabPosition = new Vector3(-0.03f, 0.05f, 0.24f);
                        grabRotation = new Vector3(-13.377f, 510.878f, -377.237f);
                    }
                    else if (gameObject.transform.name == "Air Eve" || gameObject.transform.name == "Air Eve(Clone)")
                    {
                        grabPosition = new Vector3(0.068f, 0.062f, 0.009f);
                        grabRotation = new Vector3(2.552f, 190.73f, -14.744f);
                    }
                    itemGrabbed = hit.transform.gameObject;
                    hit.collider.isTrigger = true;
                    hit.collider.enabled = false; //turn off collider
                    StartCoroutine(ItemFollowHand(hit.transform.gameObject));
                }
            }
            else if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("PickUp")))
            {
                pickUp = true;
                anim.SetBool("PickUp", true);
                audioMangaer.Play("PickUp");
                if (grabber = hit.transform.GetComponent<Grabber>()) {
                    grabber.grabber = gameObject;
                }
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
        else if ((pickButtonPressed && pickUp) || forceEject) //pressed x to drop
        { 
            pickUp = false;
            anim.SetBool("PickUp", false);
            forceEject = false;
        }
        else if (pickButtonPressed && grab) {
            itemGrabbed.GetComponent<BoxCollider>().enabled = true; //turn collider back on
            itemGrabbed.GetComponent<BoxCollider>().isTrigger = false;
            grab = false;
            anim.SetBool("Grab", false);
            itemGrabbed.transform.parent = null;
            itemGrabbed.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; //dont inherit any velocity from localmotion
            itemGrabbed.transform.position = bindingHand2.transform.position;
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

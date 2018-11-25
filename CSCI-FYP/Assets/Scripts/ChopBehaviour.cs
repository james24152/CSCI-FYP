using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopBehaviour : MonoBehaviour {

    public GameObject trunk;
    public GameObject wood;
    public GameObject trunkPoint;
    public GameObject woodPoint0;
    public GameObject woodPoint1;
    public GameObject woodPoint2;
    private Rigidbody rigid;
    private bool alreadyInstantiated = false;
    private bool furtherLock = false;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void GetChopped() {
        if (alreadyInstantiated == false) {
            transform.Translate(Vector3.up * 0.6f, Space.World);
            rigid = GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            rigid.AddForceAtPosition(transform.forward * 50, woodPoint2.transform.position);
            rigid.AddForceAtPosition(-transform.forward * 10, transform.position);
            Instantiate(trunk, trunkPoint.transform.position, trunkPoint.transform.rotation);
        }
        Invoke("RealGetChopped", 3f);
    }

    public void RealGetChopped() {
        if (alreadyInstantiated == false)
        {
            Instantiate(wood, woodPoint0.transform.position, woodPoint0.transform.rotation);
            Instantiate(wood, woodPoint1.transform.position, woodPoint1.transform.rotation);
            Instantiate(wood, woodPoint2.transform.position, woodPoint2.transform.rotation);
            alreadyInstantiated = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!furtherLock)
        {
            if (other.transform.name == "CustomAxe")
            {
                audioManager.Play("WoodChop");
                GetChopped();
                furtherLock = true;
            }
        }
    }
}

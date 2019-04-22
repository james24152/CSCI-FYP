using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLinkController : MonoBehaviour
{
    private Collider meshCollider;
    
    // Use this for initialization
    void Start()
    {
        meshCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<ElementCombineBehaviour>().elementName == "Steam")
        {
            meshCollider.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        meshCollider.isTrigger = false;
    }
}

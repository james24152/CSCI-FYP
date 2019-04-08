using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLinkController : MonoBehaviour
{
    private Collider collider;
    
    // Use this for initialization
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
        //if (collision.gameObject.GetComponent<ElementCombineBehavior>().elementName == "Steam")
        //{
        //    collider.enabled = false;
        //}
    }
}

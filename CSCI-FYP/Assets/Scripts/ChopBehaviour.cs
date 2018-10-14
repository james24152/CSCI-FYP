using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopBehaviour : MonoBehaviour {

    public GameObject trunk;
    public GameObject wood;
    public GameObject trunkPoint;
    public GameObject woodPoint;
    private bool alreadyInstantiated = false;

    public void GetChopped() {
        if (alreadyInstantiated == false)
        {
            Instantiate(trunk, trunkPoint.transform.position, trunkPoint.transform.rotation);
            Instantiate(wood, woodPoint.transform.position, woodPoint.transform.rotation);
            alreadyInstantiated = true;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHairCollider : MonoBehaviour
{

    public CapsuleCollider[] colList;

    // Use this for initialization
    void Start()
    {
        foreach (CapsuleCollider col in colList)
        {
            col.gameObject.SetActive(false);
        }
        Invoke("Go", 10f);
    }

    // Update is called once per frame
    void Go()
    {
        foreach (CapsuleCollider col in colList)
        {
            col.gameObject.SetActive(true);
        }
    }

}

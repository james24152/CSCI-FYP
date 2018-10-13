﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : MonoBehaviour {

    public float range = 2f;
    private ChopBehaviour chopScript;

    void Swing() {
        Debug.Log("swinging");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.CompareTag("InteractableTrees"))
            {
                Debug.Log(hit.transform.name);
                chopScript = hit.transform.gameObject.GetComponent<ChopBehaviour>();
                chopScript.GetChopped();
            }
        }
    }
}

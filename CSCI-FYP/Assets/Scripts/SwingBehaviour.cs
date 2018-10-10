using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : MonoBehaviour {

    public float range = 2f;

    void Swing() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range)) {
            if (hit.transform.gameObject.CompareTag("InteractableTrees"))
            {
                
            }
        }
    }
}

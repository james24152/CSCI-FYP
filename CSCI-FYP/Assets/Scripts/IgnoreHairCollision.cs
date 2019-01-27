using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreHairCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(10, 14);
        Physics.IgnoreLayerCollision(8, 14);
	}
}

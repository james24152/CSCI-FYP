using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectControl : MonoBehaviour {
    public float RotateSpeed = 20;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( 0.0f, 0.0f,Time.deltaTime * RotateSpeed * 2.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}

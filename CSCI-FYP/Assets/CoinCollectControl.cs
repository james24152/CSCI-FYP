using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectControl : MonoBehaviour {
    public float RotateSpeed = 20;
    public CurrencyManager manager;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( 0.0f, 0.0f,Time.deltaTime * RotateSpeed * 2.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            audioManager.Play("BGMCollectCoin");
            manager.AddMoney(1);
            Destroy(gameObject, 0.1f);
        }
    }
}

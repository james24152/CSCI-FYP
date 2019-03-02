using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFadeOutScript : MonoBehaviour {

    bool on;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.5f);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightShift) && !on)
        {
            Debug.Log("fire2");
            gameObject.GetComponent<Image>().CrossFadeAlpha(1, 2.0f, false);
            on = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && on){
            gameObject.GetComponent<Image>().CrossFadeAlpha(0.5f, 2.0f, false);
        }
	}
}

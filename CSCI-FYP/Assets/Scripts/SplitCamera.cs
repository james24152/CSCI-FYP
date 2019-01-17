using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCamera : MonoBehaviour {
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    private float w;
    private float h;
    // Use this for initialization
    void Start () {
        cam1 = Camera.main;
        cam2 = Camera.main;
        cam3 = Camera.main;
        cam4 = Camera.main;
        w = 0.5f;
        h = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        cam1.rect = new Rect(0.0f,0.0f,0.5f,0.5f);
        cam2.rect = new Rect(0.5f,0.0f,0.5f,0.5f);
        cam3.rect = new Rect(0.0f,0.5f,0.5f,0.5f);
        cam4.rect = new Rect(0.5f,0.5f,0.5f,0.5f);
    }
}

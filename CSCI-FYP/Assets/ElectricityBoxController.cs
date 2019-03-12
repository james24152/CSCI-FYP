using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBoxController : MonoBehaviour {
    public bool triggered;
    public GameObject allLight;
    public float power;
    public bool press;
    public Animator anim;
	// Use this for initialization
	void Start () {
        triggered = false;
        press = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (press == true && triggered == false)
        {
            triggered = true;
            anim.SetBool("electricBoxTriggered", true);
            LightOn();
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("LighteningAttack") && triggered == false)
        {
            triggered = true;
            anim.SetBool("electricBoxTriggered", true);
            LightOn();
        }
        

    }
    public void LightOn()
    {
        
        for (int i = 0; i < allLight.transform.childCount; i++)
        {
            Transform nextLight = allLight.transform.GetChild(i);
            Light light = nextLight.GetComponent<Light>();
            light.intensity = power;
        }
    }
}

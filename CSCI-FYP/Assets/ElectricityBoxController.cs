using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBoxController : MonoBehaviour {
    public bool triggered;
    public GameObject allLight;
    public float power;
    public bool press;
    public Animator anim;
    public bool allEnemyKilled;
    private bool playAlarm;
    private bool playDing;
    public AudioManager audioManager;
    // Use this for initialization
    void Start () {
        triggered = false;
        press = false;
        allEnemyKilled = false;
        playAlarm = false;
        audioManager = FindObjectOfType<AudioManager>();
        playDing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (press == true && triggered == false)
        {
            triggered = true;
            audioManager.Play("alarm");
            anim.SetBool("electricBoxTriggered", true);
            LightOn();
        }

        if (allEnemyKilled)
        {
            audioManager.Play("ding");
            anim.SetBool("allEnemyKilled", true);
        }
        if (!playAlarm && anim.GetBool("electricBoxTriggered"))
        {
            playAlarm = true;
            audioManager.Play("alarm");
        }

        if (playAlarm && anim.GetBool("allEnemyKilled")&& !playDing)
        {
            audioManager.Stop("alarm");
            audioManager.Play("ding");
            playDing = true;

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
        //audioManager.Play("alarm");

        for (int i = 0; i < allLight.transform.childCount; i++)
        {
            Transform nextLight = allLight.transform.GetChild(i);
            Light light = nextLight.GetComponent<Light>();
            light.intensity = power;
        }
    }
}

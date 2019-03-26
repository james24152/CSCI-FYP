using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1Close : MonoBehaviour {
    private int counter;
    public Animator animD1;
    public Animator animD2;
    private AudioManager audioManager;
    private bool soundPlayedD1;
    private bool soundPlayedD2;
    // Use this for initialization
    void Start () {
        counter = 0;
        audioManager = FindObjectOfType<AudioManager>();
        soundPlayedD1 = false;
        soundPlayedD2 = false;

    }
	
	// Update is called once per frame
	void Update () {
		if (!soundPlayedD1 && !animD1.GetBool("opened"))
        {
            audioManager.Play("doorClose");
            soundPlayedD1 = true;
        }
        if (!soundPlayedD2 && !animD2.GetBool("opened"))
        {
            audioManager.Play("doorClose");
            soundPlayedD2 = true;
        }

        if (soundPlayedD1 && animD1.GetBool("opened"))
        {
            soundPlayedD1 = false;
        }

        if (soundPlayedD2 && animD2.GetBool("opened"))
        {
            soundPlayedD2 = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            counter++;
            print(counter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            print(counter);
            counter--;
            print(counter);
            
            if (counter == 0 && animD1.GetBool("opened") == true)
            {
                animD1.SetBool("opened", false);
            }
            if (counter == 0 && animD2.GetBool("opened") == true)
            {
                animD2.SetBool("opened", false);
            }
        }
    }
}

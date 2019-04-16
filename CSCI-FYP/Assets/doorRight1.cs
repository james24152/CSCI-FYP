using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorRight1 : MonoBehaviour {
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    private Animator anim;
    public bool haveKey;
    private AudioManager audioManager;
    public bool soundPlayed;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        soundPlayed = false;

    }
	
	// Update is called once per frame
	void Update () {
		if (!soundPlayed && anim.GetBool("opened"))
        {
            audioManager.Play("doorOpen");
            soundPlayed = true;
        }
        if (soundPlayed && !anim.GetBool("opened"))
        {
            soundPlayed = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && haveKey)
        {
            
            anim.SetBool("opened", true);
        }
     /*   else if(other.gameObject.layer == LayerMask.NameToLayer("Character") && !haveKey)
            {
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);

                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);

                    break;
                case "Player3":
                    canvas3.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);

                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);

                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }*/

    }
}

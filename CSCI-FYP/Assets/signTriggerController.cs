using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class signTriggerController : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    //public GameObject hints;
    public Animator anim;
    private int locked;
    private bool toggled;
    public float hintsToggleDistance;
    private float hintsToggleIn;
    private float hintsToggleOut;
	// Use this for initialization
	void Start () {
        toggled = false;
        hintsToggleIn = hintsToggleDistance - 0.3f;
        hintsToggleOut = hintsToggleDistance + 0.3f;
        anim = GetComponent<Animator>();
        locked = 0;
	}
	
	// Update is called once per frame
	void Update () {
        print(Vector3.Distance(Player1.transform.position, transform.position));
        //print(Player1.transform.position);
        //print(transform.position);
        //print("toggle : ");
        print(toggled);

		if ((Vector3.Distance(Player1.transform.position, transform.position) < hintsToggleIn || Vector3.Distance(Player2.transform.position, transform.position) < hintsToggleIn || Vector3.Distance(Player3.transform.position, transform.position) < hintsToggleIn || Vector3.Distance(Player4.transform.position, transform.position) < hintsToggleIn) && toggled == false && locked == 0)
        {
            print("within...");
            locked = 1;
            StartCoroutine(playAnimation());
            print(toggled);
           //hints.SetActive(true);
        }
        print(Vector3.Distance(Player1.transform.position, transform.position));
        print(locked);
        print(toggled);

        if ((Vector3.Distance(Player1.transform.position, transform.position) >= hintsToggleOut && Vector3.Distance(Player2.transform.position, transform.position) >= hintsToggleOut && Vector3.Distance(Player3.transform.position, transform.position) >= hintsToggleOut && Vector3.Distance(Player4.transform.position, transform.position) >= hintsToggleOut) && toggled == true && locked == 1)
        {
            print("without");
            locked = 0;
            print("unlocked");
            StartCoroutine(playAnimationReverse());
            //hints.SetActive(false);
        }

    }
    IEnumerator playAnimation()
    {
        yield return StartCoroutine(playForward());
    }

    IEnumerator playForward()
    {
        anim.speed = 1;
        anim.Play("HintCanvasAnimation");
        toggled = true;
        yield return new WaitForSeconds(1.0f);
    }


    IEnumerator playAnimationReverse()
    {
        print("start inside coroutine");
        yield return StartCoroutine(playReverse());
    }

    IEnumerator playReverse()
    {
        anim.SetFloat("Direction", -1.0f);
        print("enter here");
        
        print("leave here");
        
        yield return new WaitForSeconds(1.0f);
        anim.Play("HintCanvasAnimation",-1,float.NegativeInfinity);
        toggled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGDoorController : MonoBehaviour {
    public bool haveKey;
    public Animator anim;
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    // Use this for initialization
    void Start () {
        haveKey = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("printHere");
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && haveKey == true)
        {
            anim.SetBool("triggered", true);
           
        }
        else if (haveKey == false)
        {
            if (other is CapsuleCollider)
            {
                print("change to true");
                switch (other.transform.parent.name)
                {
                    case "Player1":
                        canvas1.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);
                        break;
                    case "Player2":
                        canvas2.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);
                        break;
                    case "Player3":
                        canvas4.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);
                        break;
                    case "Player4":
                        canvas4.GetComponent<CanvasLog>().Locked.gameObject.SetActive(true);
                        break;
                    default:
                        Debug.Log("save station switch error");
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("printHere");
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && haveKey == true)
        {
            anim.SetBool("triggered", false);

        }
        else if (haveKey == false)
        {
            if (other is CapsuleCollider)
            {
                print("change to true");
                switch (other.transform.parent.name)
                {
                    case "Player1":
                        canvas1.GetComponent<CanvasLog>().Locked.gameObject.SetActive(false);
                        break;
                    case "Player2":
                        canvas2.GetComponent<CanvasLog>().Locked.gameObject.SetActive(false);
                        break;
                    case "Player3":
                        canvas4.GetComponent<CanvasLog>().Locked.gameObject.SetActive(false);
                        break;
                    case "Player4":
                        canvas4.GetComponent<CanvasLog>().Locked.gameObject.SetActive(false);
                        break;
                    default:
                        Debug.Log("save station switch error");
                        break;
                }
            }
        }
    }
}

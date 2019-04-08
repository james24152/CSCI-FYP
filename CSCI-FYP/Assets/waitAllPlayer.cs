using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class waitAllPlayer : MonoBehaviour {
    public GameObject blockFront;
    public Animator anim;
    public int totalPlayer;
    public int playerIn;
    private bool triggered;
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    // Use this for initialization
    void Start () {
        triggered = false;
        totalPlayer = GameObject.FindGameObjectsWithTag("Player").Length;

	}
	
	// Update is called once per frame
	void Update () {
        if (playerIn == totalPlayer && !triggered)
        {
            triggered = true;
            canvas1.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);
            canvas2.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);
            canvas3.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);
            canvas4.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);
            blockFront.SetActive(false);
            anim.SetBool("triggered",true);
            StartCoroutine(CanvasGo());
        } 
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && checkColliderName(other.gameObject) && !triggered)
        {
            playerIn = playerIn + 1;
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasLog>().wait.gameObject.SetActive(true);

                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasLog>().wait.gameObject.SetActive(true);

                    break;
                case "Player3":
                    canvas3.GetComponent<CanvasLog>().wait.gameObject.SetActive(true);

                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasLog>().wait.gameObject.SetActive(true);

                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && checkColliderName(other.gameObject)&&!triggered)
        {
            playerIn = playerIn - 1;
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);

                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);

                    break;
                case "Player3":
                    canvas3.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);

                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasLog>().wait.gameObject.SetActive(false);

                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }

    private bool checkColliderName(GameObject other)
    {
        if (other.name == "Earth Eve" || other.name == "Fire Eve" || other.name == "Water Eve" || other.name == "Air Eve")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    IEnumerator CanvasGo()
    {
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(true);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(true);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(true);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(false);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(false);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(false);
        canvas1.GetComponent<CanvasLog>().go.gameObject.SetActive(false);

    }
    
}

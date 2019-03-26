using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollectController : MonoBehaviour {
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    private bool pickAct;
    // Use this for initialization
    void Start () {
        pickAct = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            pickAct = true;
            print("change to true");
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickAct = false;
        if (other is CapsuleCollider)
        {
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }
}

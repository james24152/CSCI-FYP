using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class basementKeyController : MonoBehaviour {
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    public XboxButton pressButton;
    private bool triggered;
    public XboxController joystick1;
    public XboxController joystick2;
    public XboxController joystick3;
    public XboxController joystick4;
    public GameObject chest;
    private chestController chestScript;
    public Collider selfCollider;
    public bool keyEnabled;
    public bool alreadyCollected;
    private GameObject keyHolder;
    private bool unlocked;
    public GameObject door;
    public UGDoorController doorScript;
    // Use this for initialization
    void Start () {
        chestScript = chest.GetComponent<chestController>();
        triggered = false;
        keyEnabled = false;
        alreadyCollected = false;
        unlocked = false;
        doorScript = door.GetComponent<UGDoorController>();
        selfCollider = GetComponent<Collider>();

    }
	
	// Update is called once per frame
	void Update () {
        if (chestScript.triggered && !keyEnabled)
        {
            selfCollider.enabled = true;
            keyEnabled = true;
        }

        if (keyHolder != null && !unlocked)
        {
            if (Vector3.Distance(keyHolder.transform.position, door.transform.position) < 5.0f )
            {
                doorScript.haveKey = true;
                unlocked = true;
                canvas1.GetComponent<CanvasLog>().key.gameObject.SetActive(false);
                canvas2.GetComponent<CanvasLog>().key.gameObject.SetActive(false);
                canvas3.GetComponent<CanvasLog>().key.gameObject.SetActive(false);
                canvas4.GetComponent<CanvasLog>().key.gameObject.SetActive(false);
                StartCoroutine(showUnlockCanvas());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(checkColliderName(other.gameObject) && other is CapsuleCollider && !triggered &&!alreadyCollected)
        {
            triggered = true;
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);

                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);

                    break;
                case "Player3":
                    canvas3.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(true);

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

    private void OnTriggerStay(Collider other)
    {
        if (checkColliderName(other.gameObject) && !alreadyCollected)
        {
            if (XCI.GetButtonDown(pressButton, joystick1) || XCI.GetButtonDown(pressButton, joystick2) || XCI.GetButtonDown(pressButton, joystick3) || XCI.GetButtonDown(pressButton, joystick4))
            {
                print("pressed");
                alreadyCollected = true;
                //                doorLScript.haveKey = true;
                //                doorRScript.haveKey = true;
                setDisable();
                switch (other.transform.parent.name)
                {
                    case "Player1":
                        canvas1.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                        canvas1.GetComponent<CanvasLog>().key.gameObject.SetActive(true);
                        keyHolder = other.gameObject;
                        break;
                    case "Player2":
                        canvas2.GetComponent<CanvasLog>().key.gameObject.SetActive(true);
                        canvas2.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                        keyHolder = other.gameObject;
                        break;
                    case "Player3":
                        canvas3.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                        canvas3.GetComponent<CanvasLog>().key.gameObject.SetActive(true);
                        keyHolder = other.gameObject;
                        break;
                    case "Player4":
                        canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);
                        canvas4.GetComponent<CanvasLog>().key.gameObject.SetActive(true);
                        keyHolder = other.gameObject;
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
        if (other is CapsuleCollider && triggered && checkColliderName(other.gameObject))
        {
            triggered = false;
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);

                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);

                    break;
                case "Player3":
                    canvas3.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);

                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasHearts>().pressX.gameObject.SetActive(false);

                    break;

                default:
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

    IEnumerator showUnlockCanvas()
    {
        switch (keyHolder.transform.parent.name)
        {
            case "Player1":
                canvas1.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(true);

                break;
            case "Player2":
                canvas2.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(true);

                break;
            case "Player3":
                canvas3.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(true);

                break;
            case "Player4":
                canvas4.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(true);

                break;
            default:
                Debug.Log("save station switch error");
                break;
        }
        yield return new WaitForSeconds(5.0f);
        switch (keyHolder.transform.parent.name)
        {
            case "Player1":
                canvas1.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(false);

                break;
            case "Player2":
                canvas2.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(false);

                break;
            case "Player3":
                canvas3.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(false);

                break;
            case "Player4":
                canvas4.GetComponent<CanvasLog>().GetBMKey.gameObject.SetActive(false);

                break;
            default:
                Debug.Log("save station switch error");
                break;
        }
    }

    private void setDisable()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
}

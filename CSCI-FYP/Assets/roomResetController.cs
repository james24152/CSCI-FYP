using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class roomResetController : MonoBehaviour {
    public Animator anim;
    public int playerNum;
    public GameObject table;
    public GameObject chair;
    public GameObject carbinet1;
    public GameObject carbinet2;
    public GameObject smallTable;
    public GameObject tableLight1;
    public GameObject tableLight2;
    private Vector3 tableP;
    private Vector3 chairP;
    private Vector3 carbinet1P;
    private Vector3 carbinet2P;
    private Vector3 smallTableP;
    private Vector3 tableLight1P;
    private Vector3 tableLight2P;
    private Quaternion tableO;
    private Quaternion chairO;
    private Quaternion carbinet1O;
    private Quaternion carbinet2O;
    private Quaternion smallTableO;
    private Quaternion tableLight1O;
    private Quaternion tableLight2O;

    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    public bool entered;
    public XboxButton resetButton;
    public XboxController joystick1;
    public XboxController joystick2;
    public XboxController joystick3;
    public XboxController joystick4; 

    // Use this for initialization
    void Start () {
        playerNum = 0;
        tableP = table.transform.position;
        chairP = chair.transform.position;
        carbinet1P = carbinet1.transform.position;
        carbinet2P = carbinet2.transform.position;
        smallTableP = smallTable.transform.position;
        tableLight1P = tableLight1.transform.position;
        tableLight2P = tableLight2.transform.position;
        tableO = table.transform.rotation;
        chairO = chair.transform.rotation;
        carbinet1O = carbinet1.transform.rotation;
        carbinet2O = carbinet2.transform.rotation;
        smallTableO = smallTable.transform.rotation;
        tableLight1O = tableLight1.transform.rotation;
        tableLight2O = tableLight2.transform.rotation;
        entered = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            entered = true;
            print("change to true");
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasLog>().pressY.gameObject.SetActive(true);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasLog>().pressY.gameObject.SetActive(true);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasLog>().pressY.gameObject.SetActive(true);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasLog>().pressY.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }

        /*if (other.gameObject.layer == LayerMask.NameToLayer("Character") && playerNum == 0)
        {
            anim.SetBool("triggered", true);
            resetRoom();
        }*/
    }

    public void resetRoom()
    {
        table.transform.position = tableP;
        chair.transform.position = chairP;
        carbinet1.transform.position = carbinet1P;
        carbinet2.transform.position = carbinet2P;
        smallTable.transform.position = smallTableP;
        tableLight1.transform.position = tableLight1P;
        tableLight2.transform.position = tableLight2P;
        table.transform.rotation = tableO;
        chair.transform.rotation = chairO;
        carbinet1.transform.rotation = carbinet1O;
        carbinet2.transform.rotation = carbinet2O;
        smallTable.transform.rotation = smallTableO;
        tableLight1.transform.rotation = tableLight1O;
        tableLight2.transform.rotation = tableLight2O;

    }


    private void OnTriggerStay(Collider other)
    {
        if (entered)
        {
            if (XCI.GetButtonDown(resetButton, joystick1) || XCI.GetButtonDown(resetButton, joystick2) || XCI.GetButtonDown(resetButton, joystick3) || XCI.GetButtonDown(resetButton, joystick4))
            {
                print("pressed");
                anim.SetBool("triggered", true);
                resetRoom();

            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            print("change to true");
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasLog>().pressY.gameObject.SetActive(false);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasLog>().pressY.gameObject.SetActive(false);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasLog>().pressY.gameObject.SetActive(false);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasLog>().pressY.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }

        /*if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            anim.SetBool("triggered", false);
            
        }*/
    }


}

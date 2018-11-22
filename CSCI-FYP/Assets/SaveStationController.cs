using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SaveStationController : MonoBehaviour {
    public XboxController joystick1;
    public XboxController joystick2;
    public XboxController joystick3;
    public XboxController joystick4;
    private Quaternion rotation;
    private CommunWithDatabase DBScript;
    private bool saveAct;
    public GameObject DB;
    public XboxButton saveButton;
    private bool saveButtonPressed;
    public GameObject cube;
    private Vector3 aniSpwanPos;
    public ParticleSystem saveAni;
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    // Use this for initialization
    void Start () {
        DBScript = DB.GetComponent<CommunWithDatabase>();
        saveAct = false;
        saveAni.Stop();
        aniSpwanPos = cube.transform.position;
        rotation = Quaternion.Euler(-90, 0, 0);
	}
	
    private void OnTriggerEnter(Collider other)
    {

        if (other is CapsuleCollider){
            saveAct = true;
            print("change to true");
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(true);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(true);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(true);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //****show in UI, tell them press Y to save
        if(saveAct)
        {

            if (XCI.GetButtonDown(saveButton, joystick1) || XCI.GetButtonDown(saveButton, joystick2) || XCI.GetButtonDown(saveButton, joystick3) || XCI.GetButtonDown(saveButton, joystick4))
            {
                print("pressed");
                playSaveAni();
                DBScript.save();
                saveAct = false;
            }
            //if player press Y, then perform save
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        saveAct = false;
        if (other is CapsuleCollider) {
            switch (other.transform.parent.name)
            {
                case "Player1":
                    canvas1.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(false);
                    break;
                case "Player2":
                    canvas2.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(false);
                    break;
                case "Player3":
                    canvas4.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(false);
                    break;
                case "Player4":
                    canvas4.GetComponent<CanvasHearts>().pressY.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("save station switch error");
                    break;
            }
        }
    }

    public void playSaveAni()
    {
        print(aniSpwanPos);
        print(cube.transform.position);
        Instantiate(saveAni,aniSpwanPos,rotation);
    }
}

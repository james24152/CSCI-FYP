using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CharacterChangeController : MonoBehaviour {

    public GameObject earthEve;
    public GameObject waterEve;
    public GameObject fireEve;
    public GameObject airEve;
    public ParticleSystem earthFx;
    public ParticleSystem waterFx;
    public ParticleSystem fireFx;
    public ParticleSystem airFx;
    public Transform player;
    public GameObject origin;
    public GameObject center;
    public Camera cam;
    public int playerNum;
    private ParticleSystem tempFx;
    private XboxController joyNum;
    private GameObject tempEve;
    private GameObject tempElement;
    private FireBlowController fireScript;
    private BasicBehaviour basicScript;
    private MoveBehaviour moveScript;
    private PickUpBehaviour pickScript;
    private CharacterChangeController changeScript;
    private SwimBehaviour swimScript;
    private ThirdPersonOrbitCamBasic camScript;
    private AimBehaviourBasic aimScript;
    private Health healthScript;
    private DrownBehaviour drownScript;
    private CanvasHearts heartScript;
    private AudioManager audioMangaer;

    public void Evolve(int element) { //find out what element collected
        audioMangaer = FindObjectOfType<AudioManager>();
        audioMangaer.Play("Respawn");
        switch (element)
        {
            case 1:
                tempFx = Instantiate(earthFx, center.transform.position, center.transform.rotation);
                tempFx.transform.parent = center.transform;
                tempElement = earthEve;
                Invoke("Go", 2f);
                Debug.Log("change to earth");
                break;
            case 2:
                tempFx = Instantiate(waterFx, center.transform.position, center.transform.rotation);
                tempFx.transform.parent = center.transform;
                tempElement = waterEve;
                Debug.Log(tempElement);
                Invoke("Go", 2f);
                Debug.Log("change to water");
                break;
            case 3:
                tempFx = Instantiate(fireFx, center.transform.position, center.transform.rotation);
                tempFx.transform.parent = center.transform;
                tempElement = fireEve;
                Invoke("Go", 2f);
                Debug.Log("change to fire");
                break;
            case 4:
                tempFx = Instantiate(airFx, center.transform.position, center.transform.rotation);
                tempFx.transform.parent = center.transform;
                tempElement = airEve;
                Invoke("Go", 2f);
                Debug.Log("change to air");
                break;
            default:
                break;
        }
    }

    void bornEve(GameObject eve) { //assign joystick variables and camera attribute
        GetJoyNum(playerNum);

        tempEve = Instantiate(eve, origin.transform.position, origin.transform.rotation) as GameObject;
        tempEve.transform.parent = player;
        tempEve.transform.position = transform.position;
        camScript = cam.GetComponent<ThirdPersonOrbitCamBasic>();
        heartScript = camScript.canvas.GetComponent<CanvasHearts>();
        fireScript = tempEve.GetComponent<FireBlowController>();
        fireScript.joystick = joyNum;
        fireScript.cam = cam;
        aimScript = tempEve.GetComponent<AimBehaviourBasic>();
        aimScript.joystick = joyNum;
        basicScript = tempEve.GetComponent<BasicBehaviour>();
        basicScript.joystick = joyNum;
        basicScript.playerCamera = cam.transform;
        moveScript = tempEve.GetComponent<MoveBehaviour>();
        moveScript.joystick = joyNum;
        pickScript = tempEve.GetComponent<PickUpBehaviour>();
        pickScript.joystick = joyNum;
        pickScript.playerCam = cam;
        pickScript.popUp = heartScript.pressX;
        changeScript = tempEve.GetComponent<CharacterChangeController>();
        changeScript.player = tempEve.transform.parent;
        changeScript.cam = cam;
        changeScript.playerNum = playerNum;
        swimScript = tempEve.GetComponent<SwimBehaviour>();
        if (swimScript != null) {
            swimScript.cam = cam;
        }
        drownScript = tempEve.GetComponent<DrownBehaviour>();
        if (drownScript != null)
        {
            drownScript.cam = cam;
        }
        camScript.player = tempEve.transform;
        healthScript = tempEve.GetComponent<Health>();
        healthScript.icon = heartScript.icon;
        healthScript.hearts = heartScript.hearts;
        Destroy(tempFx);
        tempEve.SetActive(true);
        if (transform.name == "Earth Eve" || transform.name == "Earth Eve(Clone)")
            audioMangaer.Stop("WalkEarth");
        if (transform.name == "Water Eve" || transform.name == "Water Eve(Clone)")
            audioMangaer.Stop("WalkWater");
        if (transform.name == "Fire Eve" || transform.name == "Fire Eve(Clone)")
            audioMangaer.Stop("WalkFire");
        if (transform.name == "Air Eve" || transform.name == "Air Eve(Clone)")
            audioMangaer.Stop("WalkWind");
        audioMangaer.FadeOut("Respawn");
        Destroy(gameObject);
    }

    void Go() {
        bornEve(tempElement);
    }

    void GetJoyNum(int playerNum) {
        switch (playerNum) //find out what player number this is
        {
            case 1:
                joyNum = XboxController.First;
                break;
            case 2:
                joyNum = XboxController.Second;
                break;
            case 3:
                joyNum = XboxController.Third;
                break;
            case 4:
                joyNum = XboxController.Fourth;
                break;
            default:
                break;
        }
    }
}

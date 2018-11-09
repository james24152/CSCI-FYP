using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindmillToLightController : MonoBehaviour {

    public Windmill_controller windmillScript1;
    public Windmill_controller windmillScript2;
    public Color finishColor;
    private Color initialColor;
    public Text sign1;
    public Text sign2;
    public Level1GameManager managerScript;
    public Light light1;
    public Light light2;
    // Use this for initialization
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (windmillScript1.fanSpin)
        {
            sign1.color = finishColor;
        }
        if (windmillScript2.fanSpin)
        {
            sign2.color = finishColor;
        }
        if (windmillScript1.fanSpin && windmillScript2.fanSpin) {
            managerScript.signObjectiveStart = false;
            Invoke("LightenUp", 5f);
        }
    }
    private void LightenUp() {
        light1.intensity = 1.95f;
        light2.intensity = 1.95f;
    }
}

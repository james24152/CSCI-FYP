using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class ItemFadeOutScript : MonoBehaviour {

    public XboxController joystick;
    public float timeTillFade = 5f;
    private Image image;
    private Text text;
    private bool activate;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
        image = gameObject.GetComponent<Image>();
        text = gameObject.GetComponent<Text>();
    }

    IEnumerator StartFadeWhenTimesUp() {
        yield return new WaitForSeconds(timeTillFade);
        if (image != null)
            image.CrossFadeAlpha(0.1f, 0.5f, false);
        else 
            text.CrossFadeAlpha(0.1f, 0.5f, false);
    }

	
	// Update is called once per frame
	void Update () {
        activate = XCI.GetButtonDown(XboxButton.LeftBumper, joystick) || XCI.GetButtonDown(XboxButton.RightBumper, joystick);
        if (activate)
        {
            if (image != null)
                image.CrossFadeAlpha(1, 0.5f, false);
            else
                text.CrossFadeAlpha(1, 0.5f, false);
            StartCoroutine("StartFadeWhenTimesUp");
        }
	}
}

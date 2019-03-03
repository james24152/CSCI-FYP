using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFadeOutScript : MonoBehaviour {

    public float timeTillFade = 5f;
    private Image image;
    private Text text;

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
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (image != null)
                image.CrossFadeAlpha(1, 0.5f, false);
            else
                text.CrossFadeAlpha(1, 0.5f, false);
            StartCoroutine("StartFadeWhenTimesUp");
        }
	}
}

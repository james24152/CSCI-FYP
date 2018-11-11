using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

    public Image white;
    private CameraObjectiveActivation objectiveScript;
    // Use this for initialization
    private void Start()
    {
        white.canvasRenderer.SetAlpha(0.0f);
        objectiveScript = GetComponent<CameraObjectiveActivation>();
    }

    // Update is called once per frame
    public void Fade () {
        white.canvasRenderer.SetAlpha(0.0f);
        white.CrossFadeAlpha(1, 0.25f, false);
        Invoke("FadeBack", 1f);
    }
    private void FadeBack() {
        white.CrossFadeAlpha(0, 0.25f, false);
        if (objectiveScript != null)
            objectiveScript.StartObjectiveText();
        gameObject.SetActive(false);
    }
}

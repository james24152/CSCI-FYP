using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ItemUsedScript : MonoBehaviour {

    public XboxController joystick;
    public XboxButton activateButton;
    public string itemName;
    private bool activate;
    public ParticleSystem healing;
    public ParticleSystem speedBoost;
    private ParticleSystem tmp;
    private bool usingItem;
    private UpdateItemCountUIScript uiScript;
    // Use this for initialization
    void Start() {
        uiScript = gameObject.GetComponent<UpdateItemCountUIScript>();
    }

    // Update is called once per frame
    void Update() {
        activate = XCI.GetButtonDown(activateButton, joystick);
        if (activate && !usingItem) {
            if (itemName == "Potion" && !NoItem()) {
                usingItem = true;
                ActivatePotion();
                Invoke("ResetUsingItem", 4f);
            }
            if (itemName == "SpeedBoost" && !NoItem()) {
                usingItem = true;
                ActivateSpeedBoost();
                Invoke("ResetUsingItem", 16f);
            }
        }
    }

    private bool NoItem() {
        if (uiScript.amount >= 1)
            return false;
        return true;
    }

    private void ResetUsingItem() {
        usingItem = false;
    }

    private void ActivatePotion() {
        ShopScript.shopScript.ReducePotion();
        Health healthScript;
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>(); //we use this script because it has origin in it
        foreach (ElementCombineBehaviour script in scripts)
        {
            healthScript = script.gameObject.GetComponent<Health>();
            healthScript.TryRegen();
            //todo: can limit the situation of triggering particle effect eg.when down
            tmp = Instantiate(healing, script.origin.transform.position, healing.transform.rotation);
            tmp.transform.parent = script.origin.transform;
        }
    }

    private void ActivateSpeedBoost() {
        ShopScript.shopScript.ReduceSpeedBoost();
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>(); //we use this script because it has origin in it
        FireBlowController fireScript;
        foreach (ElementCombineBehaviour script in scripts)
        {
            tmp = Instantiate(speedBoost, script.origin.transform.position, script.gameObject.transform.rotation);
            tmp.transform.parent = script.origin.transform;
            fireScript = script.gameObject.GetComponent<FireBlowController>();
            fireScript.speedMod = true;
        }
        Invoke("DeactivateSpeedBoost", 16f);
    }

    private void DeactivateSpeedBoost() {
        FireBlowController[] scripts = FindObjectsOfType<FireBlowController>();
        foreach (FireBlowController script in scripts)
        {
            script.speedMod = false;
        }
    }
}

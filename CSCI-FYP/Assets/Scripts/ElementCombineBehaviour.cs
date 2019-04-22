using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCombineBehaviour : MonoBehaviour {

    public string elementName = "null";
    public string earthEve = "Earth Eve";
    public string earthClone = "Earth Eve(Clone)";
    public string waterEve = "Water Eve";
    public string waterClone = "Water Eve(Clone)";
    public string fireEve = "Fire Eve";
    public string fireClone = "Fire Eve(Clone)";
    public string airEve = "Air Eve";
    public string airClone = "Air Eve(Clone)";
    public ParticleSystem steamTransform;
    public ParticleSystem fogTransform;
    public ParticleSystem sandTransform;
    public ParticleSystem thunderTransform;
    public ParticleSystem lifeTransform;
    public ParticleSystem lavaTransform;

    public ParticleSystem steamAura;
    public ParticleSystem fogAura;
    public ParticleSystem sandAura;
    public ParticleSystem thunderAura;
    public ParticleSystem lifeAura;
    public ParticleSystem lavaAura;

    public ParticleSystem steamAttack;
    public ParticleSystem fogAttack;
    public ParticleSystem sandAttack;
    public ParticleSystem thunderAttack;
    public ParticleSystem lifeAttack;
    public ParticleSystem lavaAttack;
    public Transform center;
    public string[] secondTierElements = { "Steam", "Fog", "Sand", "Life", "Thunder", "Lava" };
    public Transform lavaPoint;
    public Transform thunderPoint;
    public Transform steamPoint;
    public Transform origin;

    private Animator anim;
    private ParticleSystem transformFx;
    private ParticleSystem aura;
    private ParticleSystem tempAura;
    private ParticleSystem tempAttack;
    private AudioManager audioManager;
    private String auraAudioString;
    private String attackAudioString;
    public List<string[]> chart;
    private string combinedElement;
    private bool instantiated;
    private string currentElement;
    private bool lavaLock;
    public bool secondTierElement;

    //for debug functions only, should be deleted afterwards
    public string element;
    public bool change;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        CreateElementCombineLookUpList();
        currentElement = CheckElement();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (change) {
            combinedElement = element;
            secondTierElement = true;
            anim.SetBool("SecondTierAttack", true);
            Transform(element);
            Debug.Log(element);
            instantiated = true;
            change = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (CheckCollision(other)) {
            if (!instantiated)
            {
                combinedElement = "CombinedElementFailed";
                if (other.gameObject.CompareTag("FireAttack"))
                {
                    combinedElement = CombinedElement("Fire", currentElement);
                }
                if (other.gameObject.CompareTag("WaterAttack"))
                {
                    combinedElement = CombinedElement("Water", currentElement);
                }
                if (other.gameObject.CompareTag("WindAttack"))
                {
                    combinedElement = CombinedElement("Air", currentElement);
                }
                if (other.gameObject.CompareTag("MudAttack"))
                {
                    combinedElement = CombinedElement("Earth", currentElement);
                }
                if (combinedElement != "CombinedElementFailed") {
                    secondTierElement = true;
                    anim.SetBool("SecondTierAttack", true);
                    Transform(combinedElement);
                    Debug.Log(combinedElement);
                    instantiated = true;
                }
            }
        }
    }

    /*public void SecondTierAttack() {
        switch (combinedElement) {
            case "Steam":
                transformFx = Instantiate(steamTransform, center.transform.position, steamTransform.transform.rotation);
                aura = steamAura;
                break;
            case "Fog":
                transformFx = Instantiate(fogTransform, center.transform.position, fogTransform.transform.rotation);
                aura = fogAura;
                break;
            case "Sand":
                transformFx = Instantiate(sandTransform, center.transform.position, sandTransform.transform.rotation);
                aura = sandAura;
                break;
            case "Thunder":
                tempAttack = Instantiate(thunderTransform, center.transform.position, thunderTransform.transform.rotation);
                aura = thunderAura;
                break;
            case "Life":
                transformFx = Instantiate(lifeTransform, center.transform.position, lifeTransform.transform.rotation);
                aura = lifeAura;
                break;
            case "Lava":
                tempAttack = Instantiate(lavaAttack, lavaPoint.transform.position, gameObject.transform.rotation);
                Destroy(tempAttack, 20f);
                break;
        }
    }*/

    private void Transform(string element) {
        switch (element)
        {
            case "Steam":
                elementName = "Steam";
                transformFx = Instantiate(steamTransform, center.transform.position, steamTransform.transform.rotation);
                aura = steamAura;
                auraAudioString = "SteamAura";
                attackAudioString = "SteamAttack";
                break;
            case "Fog":
                elementName = "Fog";
                transformFx = Instantiate(fogTransform, center.transform.position, fogTransform.transform.rotation);
                aura = fogAura;
                auraAudioString = "FogAura";
                attackAudioString = "FogAttack";
                break;
            case "Sand":
                elementName = "Sand";
                transformFx = Instantiate(sandTransform, center.transform.position, sandTransform.transform.rotation);
                aura = sandAura;
                auraAudioString = "SandStormAura";
                attackAudioString = "SandStormAttack";
                break;
            case "Thunder":
                elementName = "Thunder";
                transformFx = Instantiate(thunderTransform, center.transform.position, thunderTransform.transform.rotation);
                aura = thunderAura;
                auraAudioString = "LightningAura";
                attackAudioString = "ThunderAttack";
                break;
            case "Life":
                elementName = "Life";
                transformFx = Instantiate(lifeTransform, center.transform.position, lifeTransform.transform.rotation);
                aura = lifeAura;
                auraAudioString = "LifeAura";
                attackAudioString = "LifeAttack";
                break;
            case "Lava":
                elementName = "Lava";
                transformFx = Instantiate(lavaTransform, center.transform.position, lavaTransform.transform.rotation);
                aura = lavaAura;
                auraAudioString = "LavaAura";
                attackAudioString = "LavaAttack";
                break;
        }
        audioManager.Play("ElementCombine");
        anim.SetInteger("CombinedElement", Array.IndexOf(secondTierElements, combinedElement));
        transformFx.transform.parent = center.transform;
        Invoke("Aura", 2f);
    }

    private void Aura() {
        audioManager.FadeOut("ElementCombine");
        tempAura = Instantiate(aura, center.transform.position, aura.transform.rotation);
        tempAura.transform.parent = center.transform;
        audioManager.Play(auraAudioString);
    }

    private bool CheckCollision(GameObject other) {
        if (currentElement == "Earth" && other.gameObject.CompareTag("MudAttack"))
            return false;
        if (currentElement == "Fire" && other.gameObject.CompareTag("FireAttack"))
            return false;
        if (currentElement == "Water" && other.gameObject.CompareTag("WaterAttack"))
            return false;
        if (currentElement == "Air" && other.gameObject.CompareTag("WindAttack"))
            return false;
        return true;
    }

    private void CreateElementCombineLookUpList() {

        //the list of element combinations
        string[] steamCom = new string[] { "Fire", "Water", "Steam" };
        string[] fogCom = new string[] { "Water", "Air", "Fog" };
        string[] sandCom = new string[] { "Earth", "Air", "Sand" };
        string[] thunderCom = new string[] { "Fire", "Air", "Thunder" };
        string[] lifeCom = new string[] { "Water", "Earth", "Life" };
        string[] lavaCom = new string[] { "Fire", "Earth", "Lava" };
        chart = new List<string[]>();

        //should be commented
        chart.Add(steamCom);
        chart.Add(fogCom);
        chart.Add(sandCom);
        chart.Add(thunderCom);
        chart.Add(lifeCom);
        chart.Add(lavaCom);
    }

    public void AddLifeCombination() {
        string[] lifeCom = new string[] { "Water", "Earth", "Life" };
        chart.Add(lifeCom);
    }
    public void AddLavaCombination()
    {
        string[] lavaCom = new string[] { "Fire", "Earth", "Lava" };
        chart.Add(lavaCom);
    }
    public void AddSteamCombination()
    {
        string[] steamCom = new string[] { "Fire", "Water", "Steam" };
        chart.Add(steamCom);
    }
    public void AddSandCombination()
    {
        string[] sandCom = new string[] { "Earth", "Air", "Sand" };
        chart.Add(sandCom);
    }
    public void AddFogCombination()
    {
        string[] fogCom = new string[] { "Water", "Air", "Fog" };
        chart.Add(fogCom);
    }
    public void AddThunderCombination()
    {
        string[] thunderCom = new string[] { "Fire", "Air", "Thunder" };
        chart.Add(thunderCom);
    }

    public string CheckElement() {
        string name = gameObject.transform.name;
        if (name == waterEve || name == waterClone)
        {
            return "Water";
        }
        else if (name == fireEve || name == fireClone)
        {
            return "Fire";
        }
        else if (name == airEve || name == airClone)
        {
            return "Air";
        }
        else if (name == earthEve || name == earthClone)
        {
            return "Earth";
        }
        return "InvalidElement";
    }

    private string CombinedElement(string A, string B) {
        if (A == B)
            return "InvalidElement";
        foreach (string[] list in chart) {
            if ((list[0] == A && list[1] == B) || (list[0] == B && list[1] == A)) {
                Debug.Log("Combined as Element: " + list[2]);
                return list[2];
            }
        }
        return "CombinedElementFailed";
    }
    //the following are attack properties

    /*void Fire()
    {
        SecondTierAttack();
    }*/

    public void FireLava()
    {
        if (!lavaLock) {
            audioManager.Play(attackAudioString);
            tempAttack = Instantiate(lavaAttack, lavaPoint.transform.position, gameObject.transform.rotation);
            ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
            steamScript.master = gameObject;
            //Destroy(tempAttack, 20f);
            lavaLock = true;
        }
    }
    public void FireThunder() {
        audioManager.Play(attackAudioString);
        tempAttack = Instantiate(thunderAttack, thunderPoint.transform.position, gameObject.transform.rotation);
        ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
        steamScript.master = gameObject;
        //Destroy(tempAttack, 5f);
    }

    public void FireLife() {
        audioManager.Play(attackAudioString);
        ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
        foreach (ElementCombineBehaviour script in scripts) {
            script.gameObject.GetComponent<Health>().TryRegen();
            tempAttack = Instantiate(lifeAttack, script.origin.transform.position, lifeAttack.transform.rotation);
            tempAttack.transform.parent = script.origin.transform;
        }
        ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
        steamScript.master = gameObject;
    }

    public void FireSand() {
        audioManager.Play(attackAudioString);
        tempAttack = Instantiate(sandAttack, center.transform.position, sandAttack.transform.rotation);
        ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
        steamScript.master = gameObject;
        //Destroy(tempAttack, 15f);
    }

    public void FireFog()
    {
        audioManager.Play(attackAudioString);
        tempAttack = Instantiate(fogAttack, center.transform.position, fogAttack.transform.rotation);
        ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
        steamScript.master = gameObject;
        //Destroy(tempAttack, 15f);
    }

    public void FireSteam()
    {
        audioManager.Play(attackAudioString);
        tempAttack = Instantiate(steamAttack, steamPoint.transform.position, gameObject.transform.rotation);
        ParticleStopBehaviour steamScript = tempAttack.GetComponent<ParticleStopBehaviour>();
        steamScript.master = gameObject;
        tempAttack.transform.parent = steamPoint.transform;
        //Destroy(tempAttack, 20f);
    }

    public void StopAura() {
        tempAura.Stop();
        audioManager.FadeOut(attackAudioString);
        audioManager.FadeOut(auraAudioString);
        if (lavaLock)
            lavaLock = false;
        secondTierElement = false;
        instantiated = false;
        elementName = "NULL";
    }
}

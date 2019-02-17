using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCombineBehaviour : MonoBehaviour {

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

    public Transform lavaPoint;

    private ParticleSystem transformFx;
    private ParticleSystem aura;
    private ParticleSystem tempAura;
    private ParticleSystem tempAttack;
    private List<string[]> chart;
    private string combinedElement;
    private bool instantiated;
    private string currentElement;
    public bool secondTierElement;
	// Use this for initialization
	void Start () {
        CreateElementCombineLookUpList();
        currentElement = CheckElement();
    }
	

    private void OnParticleCollision(GameObject other)
    {
        if (CheckCollision(other)) {
            if (!instantiated)
            {
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
                secondTierElement = true;
                Transform(combinedElement);
                Debug.Log(combinedElement);
                instantiated = true;
            }
        }
    }

    public void SecondTierAttack() {
        switch (combinedElement) {
            /*case "Steam":
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
                break;*/
            /*case "Thunder":
                tempAttack = Instantiate(thunderTransform, center.transform.position, thunderTransform.transform.rotation);
                aura = thunderAura;
                break;*/
            /*case "Life":
                transformFx = Instantiate(lifeTransform, center.transform.position, lifeTransform.transform.rotation);
                aura = lifeAura;
                break;*/
            case "Lava":
                tempAttack = Instantiate(lavaAttack, lavaPoint.transform.position, gameObject.transform.rotation);
                Destroy(tempAttack, 20f);
                break;
        }
    }

    private void Transform(string element) {
        switch (element)
        {
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
                transformFx = Instantiate(thunderTransform, center.transform.position, thunderTransform.transform.rotation);
                aura = thunderAura;
                break;
            case "Life":
                transformFx = Instantiate(lifeTransform, center.transform.position, lifeTransform.transform.rotation);
                aura = lifeAura;
                break;
            case "Lava":
                transformFx = Instantiate(lavaTransform, center.transform.position, lavaTransform.transform.rotation);
                aura = lavaAura;
                break;
        }
        transformFx.transform.parent = center.transform;
        Invoke("Aura", 2f);
    }

    private void Aura() {
        tempAura = Instantiate(aura, center.transform.position, aura.transform.rotation);
        tempAura.transform.parent = center.transform;
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
        chart.Add(steamCom);
        chart.Add(fogCom);
        chart.Add(sandCom);
        chart.Add(thunderCom);
        chart.Add(lifeCom);
        chart.Add(lavaCom);
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
}

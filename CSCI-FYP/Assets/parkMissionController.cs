using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkMissionController : MonoBehaviour {
    public GameObject Switch1;
    public GameObject Switch2;
    public GameObject Switch3;
    public GameObject fireCage1;
    public GameObject fireCage2;
    public GameObject fireCage3;
    public GameObject fireCage4;
    public GameObject fireCage5;
    
    public GameObject DB;
    private burningController burningScriptS1;
    private burningController burningScriptS2;
    private burningController burningScriptS3;
    private burningController burningScript1;
    private burningController burningScript2;
    private burningController burningScript3;
    private burningController burningScript4;
    private burningController burningScript5;
    
    private CommunWithDatabase DBscript;
    
    private bool forOnce;

    // Use this for initialization
    void Start () {
        forOnce = false;
        burningScriptS1 = Switch1.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScriptS2 = Switch2.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScriptS3 = Switch3.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript1 = fireCage1.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript2 = fireCage2.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript3 = fireCage3.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript4 = fireCage4.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript5 = fireCage5.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript3.turn();
        
        DBscript = DB.GetComponent<CommunWithDatabase>();
    }
	
    private bool AllOff()
    {
        if (burningScriptS1.burning == false && burningScriptS2.burning == false && burningScriptS3.burning == false && burningScript1.burning == false && burningScript2.burning == false && burningScript3.burning == false && burningScript4.burning == false && burningScript5.burning == false)
        {
            return true;
        }
        else return false;
                
    }
	// Update is called once per frame
	void Update () {
		if (AllOff() == true && forOnce == false)
        {
            forOnce = true;
            int i = 2;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            while (i < 25)
            {
                
                StartCoroutine(DisableObject(i,transform.GetChild(26-i).gameObject));
                i = i + 1;
            }
            burningScriptS1.enabled = false;
            burningScriptS2.enabled = false;
            burningScriptS3.enabled = false;
            burningScript1.enabled = false;
            DBscript.finishPuzzle(0);
        }
	}


    public void levelClear()
    {
        forOnce = true;
        print("run level clear");
        for (int i = 0; i < 25; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        burningScriptS1.forceOff();
        burningScriptS2.forceOff(); 
        burningScriptS3.forceOff();
        burningScript1.forceOff();
        burningScript2.forceOff();
        burningScript3.forceOff();
        burningScript4.forceOff();
        burningScript5.forceOff();

        burningScriptS1.enabled = false;
        burningScriptS2.enabled = false;
        burningScriptS3.enabled = false;
        burningScript1.enabled = false;
        burningScript2.enabled = false;
        burningScript3.enabled = false;
        burningScript4.enabled = false;
        burningScript5.enabled = false;

    }

    IEnumerator DisableObject(int i,GameObject road)
    {
        print(i);
        yield return new WaitForSeconds(i-1);
        print("wait end");
        print(i);
        road.SetActive(false);
    }
}

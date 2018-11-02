using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Switch : MonoBehaviour
{
    public GameObject fireCage1;
    public GameObject fireCage2;
    public GameObject fireCage3;
    private burningController burningScript1;
    private burningController burningScript2;
    private burningController burningScript3;
    public bool burning;
    // Use this for initialization
    void Start()
    {
        //this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        burning = true;
        burningScript1 = fireCage1.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript2 = fireCage2.transform.GetChild(0).gameObject.GetComponent<burningController>();
        burningScript3 = fireCage3.transform.GetChild(0).gameObject.GetComponent<burningController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (burning == false)
        {
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false); // unable flame
            this.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(false); //unable sparkle
        }

        else
        {
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); // unable flame
            this.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true); //unable sparkle
        }
    }
    private void turn()
    {
        burningScript1.turn();
        burningScript2.turn();
        burningScript3.turn();
    }



    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("WaterAttack") && burning == true)
        {
            turn();
            burning = false;
        }
        if (other.gameObject.CompareTag("fireAttack") && burning == false)
        {
            turn();
            burning = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (burning == true)
        {
            turn();
            burning = false;
        }
        else if (burning == false)
        {
            turn();
            burning = true;
        }
    }
}

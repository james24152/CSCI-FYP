using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class signTriggerController : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject hints;
    private bool toggled;
    public float hintsToggleDistance;
	// Use this for initialization
	void Start () {
        toggled = false;
	}
	
	// Update is called once per frame
	void Update () {
        print(Vector3.Distance(Player1.transform.position, transform.position));
        print(Player1.transform.position);
        print(transform.position);

		if (Vector3.Distance(Player1.transform.position, transform.position) < hintsToggleDistance || Vector3.Distance(Player2.transform.position, transform.position) < hintsToggleDistance || Vector3.Distance(Player3.transform.position, transform.position) < hintsToggleDistance || Vector3.Distance(Player4.transform.position, transform.position) < hintsToggleDistance)
        {
            print("within...");
            toggled = true;
            hints.SetActive(true);
        }

        if (Vector3.Distance(Player1.transform.position, transform.position) >= hintsToggleDistance && Vector3.Distance(Player2.transform.position, transform.position) >= hintsToggleDistance && Vector3.Distance(Player3.transform.position, transform.position) >= hintsToggleDistance && Vector3.Distance(Player4.transform.position, transform.position) >= hintsToggleDistance)
        {
            print("without");
            toggled = false;
            hints.SetActive(false);
        }

    }
}

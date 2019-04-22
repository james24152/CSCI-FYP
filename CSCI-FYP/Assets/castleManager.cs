using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleManager : MonoBehaviour {

    public int playerCount;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public int sublevel = 1;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        switch (playerCount)
        {
            case 1:
                player1.SetActive(true);
                break;
            case 2:
                player1.SetActive(true);
                player2.SetActive(true);
                break;
            case 3:
                player1.SetActive(true);
                player2.SetActive(true);
                player3.SetActive(true);
                break;
            case 4:
                player1.SetActive(true);
                player2.SetActive(true);
                player3.SetActive(true);
                player4.SetActive(true);
                break;
            default:
                Debug.Log("Player Activation failure");
                break;
        }
        TunePlayView(playerCount);
        audioManager = FindObjectOfType<AudioManager>();
       // audioManager.Play("BGMMedieval");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TunePlayView(int playerNum)
    {
        if (playerNum == 1)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (playerNum == 2)
        {

            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
        }
        else if (playerNum == 3)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            player3.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
        }
        else if (playerNum == 4)
        {
            player1.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            player2.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            player3.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
            player4.transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
        }
    }
}

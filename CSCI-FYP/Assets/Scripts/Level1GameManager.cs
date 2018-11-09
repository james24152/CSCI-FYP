using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1GameManager : MonoBehaviour {

    public int sublevel = 1;
    // Use this for initialization
    public int playerCount;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Text logObjective;
    public Text signObjective;
    public bool logObjectiveStart;
    public bool signObjectiveStart;
    public bool missionFailed;
    public DialogueTrigger dialogueTrigger;
    private void Start()
    {
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
    }
    private void Update()
    {
        if (logObjectiveStart)
            logObjective.gameObject.SetActive(true);
        else
            logObjective.gameObject.SetActive(false);
        if (signObjectiveStart)
            signObjective.gameObject.SetActive(true);
        else
            signObjective.gameObject.SetActive(false);
        if (missionFailed)
            dialogueTrigger.TriggerDialogue(); //also cinematic
    }
}

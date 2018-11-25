using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueTrigger : MonoBehaviour {

    public PlayableDirector cutscene;
    public Dialogue dialogue;
    public Camera cutsceneCam;
    private bool inited;
    private bool inited2;
    private AudioManager audioManager;
    private bool BGMLock;
    public string BGMWhileTrigger;
    public string BGM;
    private void Start()
    {
        BGMLock = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BGM != ""&&BGMLock == false) {
            audioManager.Stop(BGM);
            audioManager.Play(BGMWhileTrigger);
            BGMLock = true;
        }
        
        TriggerDialogue();
    }

    public void TriggerDialogue() {
        if (gameObject.transform.name == "Dialogue Trigger4") {
            Debug.Log("trigger dialogue");
            if (!inited2) {
                audioManager.Play("GameOver");
                cutsceneCam.gameObject.SetActive(true);
                cutscene.Play();
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                inited2 = true;
                Invoke("ResetInit", 20f);
            }
        }
        if (!inited) {
            cutsceneCam.gameObject.SetActive(true);
            Debug.Log("triggered dialoague");
            cutscene.Play();
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            inited = true;
        }
    }

    private void ResetInit() {
        inited2 = false;
    }
}

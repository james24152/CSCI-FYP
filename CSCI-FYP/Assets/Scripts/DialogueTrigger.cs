using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    private bool inited;

    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
    }

    public void TriggerDialogue() {
        if (!inited) {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            inited = true;
        }
    }
}

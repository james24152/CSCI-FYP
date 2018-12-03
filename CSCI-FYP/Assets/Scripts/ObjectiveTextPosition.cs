using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTextPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rect = GetComponent<RectTransform>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Level1GameManager managerScript = gameManager.GetComponent<Level1GameManager>();
        if (managerScript.playerCount == 1) {
            rect.anchorMax = new Vector2(1, 1);
            rect.anchorMin = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
        }
	}
	
}

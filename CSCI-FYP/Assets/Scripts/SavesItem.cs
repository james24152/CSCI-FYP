using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavesItem : MonoBehaviour {

    public Button button;
    public TextMeshProUGUI levelState;
    public TextMeshProUGUI playerNum;
    public TextMeshProUGUI time;

    private Item item;
    private SaveScrollList scrollList;

    // Use this for initialization
    void Start () {
        button.onClick.AddListener(HandleClick);
	}

    public void Setup(Item currentItem, SaveScrollList currentScrollList) {
        item = currentItem;
        levelState.text = currentItem.levelState.ToString();
        playerNum.text = currentItem.playerNum.ToString();
        time.text = currentItem.time;

        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        Debug.Log("loading save with: " + item.levelState + " " + item.playerNum + " "+ item.time);
        PlayerPrefs.SetInt("playerNum", item.playerNum);
        PlayerPrefs.SetInt("saveStatus", 0);
        PlayerPrefs.SetInt("levelState", item.levelState);
        //load
    }
}

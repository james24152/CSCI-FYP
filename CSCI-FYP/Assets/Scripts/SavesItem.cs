using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SavesItem : MonoBehaviour {

    public Button button;
    public LevelStateToggle levelState;
    public TextMeshProUGUI playerNum;
    public TextMeshProUGUI time;

    private Item item;
    private SaveScrollList scrollList;
    private GameObject loadMenu;
    private GameObject loadingBar;

    // Use this for initialization
    void Start () {
        button.onClick.AddListener(HandleClick);
        loadMenu = GameObject.FindGameObjectWithTag("LoadMenu");
        loadingBar = GameObject.FindGameObjectWithTag("LoadingBar");
    }

    public void Setup(Item currentItem, SaveScrollList currentScrollList) {
        item = currentItem;
        switch (currentItem.levelState) {
            case 0:
                levelState.mazeToggle.isOn = false;
                levelState.arenaToggle.isOn = false;
                break;
            case 1:
                levelState.mazeToggle.isOn = true;
                levelState.arenaToggle.isOn = false;
                break;
            case 2:
                levelState.mazeToggle.isOn = false;
                levelState.arenaToggle.isOn = true;
                break;
            case 3:
                levelState.mazeToggle.isOn = true;
                levelState.arenaToggle.isOn = true;
                break;
            default:
                Debug.Log("something wrong with levelState parser");
                break;
        }
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
        loadingBar.GetComponent<LoadingBar>().LoadLevel(2);
        loadMenu.SetActive(false);
    }
}

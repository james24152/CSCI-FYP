using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Item {
    public int levelState;
    public int playerNum;
    public string time;

    public Item(int levelState, int playerNum, string time)
    {
        this.levelState = levelState;
        this.playerNum = playerNum;
        this.time = time;
    }
}

public class SaveScrollList : MonoBehaviour {

    public List<Item> itemList;
    public ObjectPool buttonObjectPool;
    public Transform contentPanel;
    public string[] items;

    // Use this for initialization
    void Start () {
        //AddButtons();	
        StartCoroutine(LoadSaveData());
    }

    private void AddButtons() {
        for (int i = 0; i < itemList.Count; i++) {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);
            SavesItem sampleButton = newButton.GetComponent<SavesItem>();
            sampleButton.Setup(item, this);
        }
    }

    private IEnumerator LoadSaveData()
    {
        WWW saveData = new WWW("http://localhost/gameData.php");
        yield return saveData;
        string saveDataString = saveData.text;
        items = saveDataString.Split(';');
        /*foreach (string item in items) {
            int maplevelNum = getInt(item, "maplevelNum:"); //each levelNum
            int playerNum = getInt(item, "playerNum:"); //each playerNum
            string saveTime = getString(item, "Time:"); //each saveTime

            itemList.Add(new Item(maplevelNum, playerNum, saveTime));
        }*/
        int i;
        for (i = 0; i < items.Length - 1; i++) {
            int maplevelNum = getInt(items[i], "maplevelNum:"); //each levelNum
            int playerNum = getInt(items[i], "playerNum:"); //each playerNum
            string saveTime = getString(items[i], "Time:"); //each saveTime

            itemList.Add(new Item(maplevelNum, playerNum, saveTime));
        }
        AddButtons();
        //printData();

        //save as playerPref: saveStatus, globalplayerNum, inputmaplevelNum, inputplayerNum
    }

    int getInt(string data, string index)
    {
        string temp = data.Substring(data.IndexOf(index) + index.Length);
        if (temp.Contains("|"))
        {
            temp = temp.Remove(temp.IndexOf("|"));
        }
        int value = int.Parse(temp);
        return value;
    }

    string getString(string data, string index)
    {
        string temp = data.Substring(data.IndexOf(index) + index.Length);
        if (temp.Contains("|"))
        {
            temp = temp.Remove(temp.IndexOf("|"));
        }
        return temp;
    }
}

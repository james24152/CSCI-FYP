using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunWithDatabase : MonoBehaviour {

    // Use this for initialization
    public string[] items;
    string saveGameDataURL = "http://localhost/saveGameData.php";
    public int inputmaplevelNum;
    public int inputplayerNum;
    public string inputsaveTime;
    public GameObject[] players = new GameObject[4];
    public int saveStatus;
    public int globalplayerNum;
    private int[] levels;
    void Start() {
        levels = new int[4];
        saveStatus = 1;
        StartCoroutine(performStartLoad());
    }

    private IEnumerator performStartLoad()
    {

        if (saveStatus == 1)
        {
            print("startnewgame");
            levels[0] = 0;
            levels[1] = 0;
            levels[2] = 0;
            levels[3] = 0;
            startNewGame(globalplayerNum);
        }
        else
        {
            print("loadData");
            WWW saveData = new WWW("http://localhost/gameData.php");
            yield return saveData;
            string saveDataString = saveData.text;
            items = saveDataString.Split(';');
            int maplevelNum = getInt(items[0], "maplevelNum:");
            int playerNum = getInt(items[0], "playerNum:");
            string saveTime = getString(items[0], "Time:");
            load(maplevelNum, playerNum);

        }
    }

    public void finishPuzzle(int pNum)
    {
        levels[pNum] = 1;
    }

    private int checkGameProgress (){
        return levels[0] * 1 + levels[1] * 2 + levels[2] * 4 + levels[3] * 8;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputmaplevelNum=checkGameProgress();
            inputplayerNum = globalplayerNum;
            inputsaveTime = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
            save(inputmaplevelNum, inputplayerNum, inputsaveTime);
        }
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
    public void load(int maplevelNum, int playerNum)
    {
        for (int i = 0 ; i < playerNum; i++)
        {
            Instantiate(players[i]);
            
            if (maplevelNum >= 0 && maplevelNum <= 15)
            {
                players[i].transform.position = new Vector3(100.0f, 10.0f, 270.0f + (float)i * 5.0f);
            }
            else { }
        }
        tunePlayView(playerNum);
        
    }

    public void startNewGame(int playerNum)
    {
        
        for (int i = 0; i < playerNum; i++)
        {
            Instantiate(players[i],new Vector3(100.0f,10.0f,270.0f+(float)i*5.0f),Quaternion.identity);
        }
        tunePlayView(playerNum);
        
    }

    public void tunePlayView(int playerNum)
    {
        if (playerNum == 1)
        {
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,1.0f,1.0f);
        }
        else if(playerNum == 2)
        {
            
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f,0.5f,1.0f,1.0f);
            players[1].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,1.0f,0.5f);
        }
        else if (playerNum == 3)
        {
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            players[1].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            players[2].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
        }
        else if (playerNum == 4)
        {
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            players[1].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            players[2].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
            players[3].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
        }
    }



    public void save(int maplevelNum, int playerNum, string saveTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("maplevelNumPost", maplevelNum);
        form.AddField("playerNumPost", playerNum);
        form.AddField("saveTimePost", saveTime);
        WWW www = new WWW(saveGameDataURL,form);
    }

    // Update is called once per frame

}

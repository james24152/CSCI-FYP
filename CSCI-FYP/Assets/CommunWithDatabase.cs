using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunWithDatabase : MonoBehaviour {

    // Use this for initialization
    public string[] items;
    public int[] maplevelSaves;
    public int[] playerNumSaves;
    public string[] timesaves;
    public int[] CmaplevelSaves;
    public int[] CplayerNumSaves;
    public string[] Ctimesaves;
    string saveGameDataURL = "http://localhost/saveGameData.php";
    public int inputmaplevelNum;
    public int inputplayerNum;
    public string inputsaveTime;
    public GameObject[] players = new GameObject[4];
    public int saveStatus;
    public int globalplayerNum;
    private int[] levels;
    
    public GameObject parkMission;
    public GameObject arenaMission;
    private parkMissionController parkMissionScript;
    private arenaMissionController arenaMissionScript;
    void Start() {
        maplevelSaves = new int[100];
        playerNumSaves = new int[100];
        timesaves = new string[100];
        CmaplevelSaves = new int[100];
        CplayerNumSaves = new int[100];
        Ctimesaves = new string[100];
        levels = new int[2]; // Park = levels[0], Arena = levels[1]
        levels[0] = 0;
        levels[1] = 0;
        parkMissionScript = parkMission.GetComponent<parkMissionController>();
        arenaMissionScript = arenaMission.GetComponent<arenaMissionController>();
        ////////////////
        //saveStatus = 0;
        ///////////////

        //REMEMBER TO CHANGE THIS BACK
        ////////////////////////////////////////////////////////////
        //StartCoroutine(performStartLoad());
        //////////////////////////////////////////////////////////
    }

    private void StartGame() {
        if (saveStatus == 1)
        {
            print("startnewgame");
            levels[0] = 0;
            levels[1] = 0;
            startNewGame(globalplayerNum);
        }
    }

    private IEnumerator performStartLoad()
    {
        WWW saveData = new WWW("http://localhost/gameData.php");
        yield return saveData;
        string saveDataString = saveData.text;
        items = saveDataString.Split(';');
        //int maplevelNum = getInt(items[0], "maplevelNum:");
        //int playerNum = getInt(items[0], "playerNum:");
        //string saveTime = getString(items[0], "Time:");
        //printData();
        if (saveStatus == 1)
        {
            print("startnewgame");
            levels[0] = 0;
            levels[1] = 0;
            startNewGame(globalplayerNum);
        }
        else
        {
            print("loadData");

            //load(maplevelNum, playerNum);
            load(inputmaplevelNum, inputplayerNum);
        }
    }

    public void finishPuzzle(int pNum)
    {
        levels[pNum] = 1;
    }

    private int checkGameProgress (){
        return levels[0] * 1 + levels[1] * 2;
    }

    private void Update()
    {
        if (saveStatus == 1)
        {
            saveStatus = 0;
            print("startnewgame");
            levels[0] = 0;
            levels[1] = 0;
            startNewGame(globalplayerNum);
        }

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
        
        //player Part
        for (int i = 0 ; i < playerNum; i++)
        {
            players[i].SetActive(true);
            
        }
        tunePlayView(playerNum);

        switch (maplevelNum)
        {
            case 1:
                levels[0] = 1;
                levels[1] = 0;
                break;
            case 2:
                levels[0] = 0;
                levels[1] = 1;
                break;
            case 3:
                levels[0] = 1;
                levels[1] = 1;
                break;
            default:
                levels[0] = 0;
                levels[1] = 0;
                break;
        }
        //mission part
        if(levels[0] == 1)
        {
            parkMissionScript.levelClear();
        }

        if(levels[1] == 1)
        {
            
            arenaMissionScript.levelClear();
        }
    }

    public void startNewGame(int playerNum)
    {
        
        for (int i = 0; i < playerNum; i++)
        {
            //Instantiate(players[i],new Vector3(100.0f,10.0f,270.0f+(float)i*5.0f),Quaternion.identity);
            Debug.Log("invoke active");
            players[i].SetActive(true);
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
            
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f,0.5f,1.0f,0.5f);
            players[1].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,1.0f,0.5f);
        }
        else if (playerNum == 3)
        {
            players[0].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
            players[1].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            players[2].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
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

    public void printData()
    {
        CmaplevelSaves = getMaplevelList();
        CplayerNumSaves = getPlayerNumList();
        Ctimesaves = getTimeList();
    }
    
    
    public int[] getMaplevelList()
    {
        int j = 0;
        for(int i = items.Length - 2; i >= 0; i--)
        {
            //print(items[i]);
            maplevelSaves[j] = getInt(items[i], "maplevelNum:");
            //print(maplevelSaves[j]);
            j++;
            
        }
        
        //print("EOMLS");
        return maplevelSaves;
    }
    public int[] getPlayerNumList()
    {
        int j = 0;
        for (int i = items.Length - 2; i >= 0; i--)
        {
            playerNumSaves[j] = getInt(items[i], "playerNum:");
            //print(playerNumSaves[j]);
            j++;
        }
        
        //print("EOPNS");
        return playerNumSaves;
    }
    public string[] getTimeList()
    {
        int j = 0;
        for (int i = items.Length - 2; i >= 0; i--)
        {
            timesaves[j] = getString(items[i], "Time:");
            //print(timesaves[j]);
            j++;
        }
        
        //print("EOTS");
        return timesaves;
    }

}

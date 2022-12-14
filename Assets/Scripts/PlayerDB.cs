using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class StageData
{
    public int[] StageStar = new int[5];
    public bool[] IsAcitve = new bool[5] { false, false, false, false, false };
}

[System.Serializable]
public class PlayerData
{

    public readonly int _MaxTicket = 30;

    public StageData[] MyStageData= new StageData[5];
    public List<Character> MyCharacters = new List<Character>();
    public bool CutScene = true;
    public int UserSkill_Index = 0;

    public string PlayerName = "김준우";
    public int _Gold = 0;
    public int _Ticket = 30;
    public bool PlayFirst = true;



}



public class PlayerDB : MonoBehaviour
{
    #region 싱글톤

    private static PlayerDB instance = null;

    public static PlayerDB Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerDB>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "PlayerDB";
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<PlayerDB>();
                }
            }
            return instance;
        }
    }

    #endregion

    public PlayerData playerdata;
    public UserSkill myUserSkill;
    public bool IsFirst = true;

    public int Gold
    {
        get => playerdata._Gold;
        set
        {
            playerdata._Gold = value;
            UIManager.Instance.SetGold_Text();
        }

    }

    public int MaxTicket
    {
        get => playerdata._MaxTicket; 
    }

    public int Ticket
    {
        get => playerdata._Ticket;
        set
        {
            playerdata._Ticket = value;
            UIManager.Instance.SetTicket_Text(MaxTicket);
        }

    }


    //유저 스킬도 있음

    private void Awake()
    {
        playerdata = new PlayerData();
        myUserSkill = GameDB.Instance.UserSkills[playerdata.UserSkill_Index];
        SetStageArray();
       // LoadData();
    }

    public void SetStageArray()
    {
        for(int i=0; i<5; i++)
        {
            playerdata.MyStageData[i] = new StageData();
            for (int j = 0; j < 5; j++)
            {
                playerdata.MyStageData[i].IsAcitve = new bool[5];
                playerdata.MyStageData[i].StageStar = new int[5];
                
            }

            playerdata.MyStageData[0].IsAcitve[0] = true;
            //나중에 테스트 끝나면 지우면됨
            /*
            for(int k = 0;k<5;k++)
            {
                playerdata.MyStageData[i].IsAcitve[k] = true;
            }
            */
        }
        
    }

    public void addCharacter(Character newCharacter)
    {
        playerdata.MyCharacters.Add(newCharacter);
    }

    public void SaveData()
    {   
        GameSystem.Save<PlayerData>(ref playerdata, "/PlayerDB.json");
        /*
        string data = JsonConvert.SerializeObject(playerdata);
        File.WriteAllText(Application.dataPath+"/PlayerDB.json",data);
        */
    }

    public void LoadData()
    {
        if (GameSystem.Load<PlayerData>(ref playerdata, "/PlayerDB.json"))
        {
            try
            {
                UIManager.Instance.SetGold_Text();
                UIManager.Instance.SetTicket_Text(MaxTicket);
            }
            catch(System.Exception e)
            {
                Debug.Log(e.Message);
            }

            
        }
    }   

    
}

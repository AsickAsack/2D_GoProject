using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class StageData
{
    public StageData()
    {
        StageStar = new int[5];
        IsAcitve = new bool[5];
    }

    public int[] StageStar;
    public bool[] IsAcitve;
}

[System.Serializable]
public class PlayerData
{

    public readonly int _MaxTicket = 30;

    public StageData[] MyStageData= new StageData[5];
    public List<Character> MyCharacters = new List<Character>();
    public bool CutScene = true;
    public int UserSkill_Index = 0;

    public string PlayerName = "±Ë¡ÿøÏ";
    public int _Gold = 0;
    public int _Ticket = 30;
    public bool PlayFirst = true;



}



public class PlayerDB : MonoBehaviour
{
    #region ΩÃ±€≈Ê

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


    //¿Ø¿˙ Ω∫≈≥µµ ¿÷¿Ω

    private void Awake()
    {
        playerdata = new PlayerData();
        myUserSkill = GameDB.Instance.UserSkills[playerdata.UserSkill_Index];
        //playerdata.MyStageData = new StageData[5];
        //playerdata.MyStageData[0].IsAcitve[0] = true;
    }


    public void addCharacter(Character newCharacter)
    {
        playerdata.MyCharacters.Add(newCharacter);
    }

    public void SaveData()
    {   playerdata.MyStageData[0].IsAcitve[0] = true;
        GameSystem.Save<PlayerData>(ref playerdata, "/PlayerDB.json");
        /*
        string data = JsonConvert.SerializeObject(playerdata);
        File.WriteAllText(Application.dataPath+"/PlayerDB.json",data);
        */
    }

    public void LoadData()
    {
        GameSystem.Load<PlayerData>(ref playerdata, "/PlayerDB.json");
        UIManager.Instance.SetGold_Text();
        UIManager.Instance.SetTicket_Text(MaxTicket);
    }   

    
}

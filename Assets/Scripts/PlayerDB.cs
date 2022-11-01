using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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


    public List<Character> MyCharacters = new List<Character>();
    public int Gold { get; set; }
    //유저 스킬도 있음




    public void addCharacter(Character newCharacter)
    {
        MyCharacters.Add(newCharacter);
        //Debug.Log(newCharacter.name + "이 추가되었습니다.");
    }

    public void SaveData()
    {
        string data = JsonConvert.SerializeObject(MyCharacters);
        File.WriteAllText(Application.dataPath+"/PlayerDB.json",data);

    }

    public void LoadData()
    {
        string data = File.ReadAllText(Application.dataPath + "/PlayerDB.json");
        MyCharacters = JsonConvert.DeserializeObject(data) as List<Character>;
    }

    
}

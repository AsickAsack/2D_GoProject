using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum CharacterName
{
    Strong,lazy,mad
}

public enum CharacterStat
{
    Name=1,Character_Des,Min_Power,Max_Power,Mass,Drag,Active_Des,Active_Figure,Passive_Des,Passive_Figure,Passive_Range
}


public class GameDB : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1G6EXf961cN9OO_SmY-67wblpfaSLLb8N_odImJvg9Ls/export?format=csv";
    public static Dictionary<int,string[]> CharacterDB = new Dictionary<int,string[]>();

    //아이콘은 게임 DB에서 관리하거나 직렬화 하지 않기
    /*
    public Sprite[] CutScene;
    public Sprite[] BigImage;
    public Sprite[] CharacterIcon;
    */
    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;

        string[] tempDB = data.Split('\n');

        for(int i= 1;i<12;i++)
        {
            string[] tempDB2 = tempDB[i].Split(',');
            CharacterDB.Add(int.Parse(tempDB2[0]), tempDB2);
        }

        
       
    }
}

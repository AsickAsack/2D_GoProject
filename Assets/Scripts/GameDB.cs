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
    Name=1,Character_Des,MinPower,MaxPower,Mass,Drag,Active_Des,Active_Figure,Passive_Des,Passive_Figure,Passive_Range
}


public class GameDB : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1G6EXf961cN9OO_SmY-67wblpfaSLLb8N_odImJvg9Ls/export?format=csv";
    public static Dictionary<int,string[]> CharacterDB = new Dictionary<int,string[]>();

    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;

        string[] tempDB = data.Split('\n');

        for(int i= 1;i<tempDB.Length;i++)
        {
            string[] tempDB2 = tempDB[i].Split(',');
            CharacterDB.Add(i-1, tempDB2);
        }

        
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum CharacterName
{
    Strong=1001,lazy,mad,twin,prophet,Vampire,PackMan,
    Covid,Leesin,Nerd
}

public enum CharacterStat
{
    Name=1,Character_Des,Min_Power,Max_Power,Mass,Drag,
    Active_Des,Active_Target,Active_Figure,
    Passive_Des, Passive_probability, Passive_Range, Character_Story
}

public enum ActiveTarget
{
    Team=1,Enemy,All,Obstacle
}


public class GameDB : MonoBehaviour
{
    public static GameDB Instance;

    const string URL = "https://docs.google.com/spreadsheets/d/1G6EXf961cN9OO_SmY-67wblpfaSLLb8N_odImJvg9Ls/export?format=csv";
    public static Dictionary<int, string[]> CharacterDB = new Dictionary<int, string[]>();

    public Sprite[] CharacterImage;
    public Sprite[] CharacterIcon;

    private void Awake()
    {
        Instance = this;

    }

    public Sprite GetCharacterImage(Character Char)
    {
        return CharacterImage[(int)Char.MyCharacter-(int)CharacterName.Strong];
    }

    public Sprite GetCharacterIcon(Character Char)
    {
        return CharacterIcon[(int)Char.MyCharacter-(int)CharacterName.Strong];
    }


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

        for(int i= 1;i<tempDB.Length;i++)
        {
            string[] tempDB2 = tempDB[i].Split(',');
            Debug.Log(tempDB2[0]);
            CharacterDB.Add(int.Parse(tempDB2[0]), tempDB2);
            
        }

        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.Strong));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.lazy));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.mad));


    }
}

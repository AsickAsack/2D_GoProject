using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum ObstacleName
{
    Wall//현재는 벽만 구현함
}

public enum MonsterName
{ 
    Basic=2001,Big,Silence,Boss,Bomb,Armor, cushion // 반복이동,순서
}

public enum MonsterStat
{
    Name,Des,Skill_Figure,Mass,Size
}

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

    const string Char_URL = "https://docs.google.com/spreadsheets/d/1G6EXf961cN9OO_SmY-67wblpfaSLLb8N_odImJvg9Ls/export?format=csv";
    const string Mon_URL = "https://docs.google.com/spreadsheets/d/1MHohZTbe87kDvFujM9cYgBkQgGATA6cjONlPEv3jPuo/export?format=csv";

    public static Dictionary<int, string[]> CharacterDB = new Dictionary<int, string[]>();

    public static Dictionary<int, string[]> MonsterDB = new Dictionary<int, string[]>();
    public GameObject[] Monsters;

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

    public GameObject GetMonster(MonsterName name)
    {
        return Monsters[(int)name - (int)MonsterName.Basic];
    }


    //아이콘은 게임 DB에서 관리하거나 직렬화 하지 않기
    /*
    public Sprite[] CutScene;
    public Sprite[] BigImage;
    public Sprite[] CharacterIcon;
    */

    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(Char_URL);
        yield return www.SendWebRequest();

        InputDB(DownLoad_DB(www), CharacterDB);

        UnityWebRequest www2 = UnityWebRequest.Get(Mon_URL);
        yield return www2.SendWebRequest();

        InputDB(DownLoad_DB(www2), MonsterDB);

        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.Strong));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.lazy));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.mad));


    }

    //DB 다운로드
    string DownLoad_DB(UnityWebRequest www)
    {
        return www.downloadHandler.text; 
    }

    //딕셔너리에 DB넣기
    public void InputDB(string data,Dictionary<int,string[]> dic)
    { 
        string[] tempDB = data.Split('\n');

        for (int i = 1; i < tempDB.Length; i++)
        {
            string[] tempDB2 = tempDB[i].Split(',');
            dic.Add(int.Parse(tempDB2[0]), tempDB2);
        }
    }

}

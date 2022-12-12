using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum ObstacleName
{
    Wall,Stop,Booster,Toggle,BlackHole
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
    Strong=1001,Olaf,mad,Killer,Witch,BaseBall,Leesin,Rebirth, brainwash,Avoid, bulldozer
}

public enum CharacterStat
{
    Name=1,Character_Des,Min_Power,Max_Power,Mass,Drag,
    Skill_Des,Skill_Target,Skill_Type,
    Skill_probability,Skill_Range,Story
}

public enum ActiveTarget
{
    None,Team,Enemy,All,Obstacle
}

public enum NotifyIcon : int
{
    Killstreak=0,Killstreak_Fail,MostKill,PowerUp,ReBirth,HomeRun
}

public enum UserSkillicon
{
    Bus,Ice,Wall,Heart
}

public class GameDB : MonoBehaviour
{
    private static GameDB _Instance = null;
    public static GameDB Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType<GameDB>();
                if(_Instance == null)
                {
                    GameObject obj = Resources.Load("GameDB") as GameObject;
                    _Instance = obj.GetComponent<GameDB>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _Instance;
        }
    }


    public const string Char_URL = "https://docs.google.com/spreadsheets/d/1G6EXf961cN9OO_SmY-67wblpfaSLLb8N_odImJvg9Ls/export?format=csv";
    public const string Mon_URL = "https://docs.google.com/spreadsheets/d/1MHohZTbe87kDvFujM9cYgBkQgGATA6cjONlPEv3jPuo/export?format=csv";

    public static Dictionary<int, string[]> CharacterDB = new Dictionary<int, string[]>();
    public static Dictionary<int, string[]> MonsterDB = new Dictionary<int, string[]>();


    public GameObject[] Characters;
    public GameObject[] Monsters;
    public GameObject[] Obstacles;
    public UserSkill[] UserSkills;

    public Sprite[] CharacterImage;
    public Sprite[] CharacterIcon;
    public Sprite[] NotifySprite;
    public Sprite[] UserSkillicon;

    public Sprite GetNotifySpirte(NotifyIcon notifyicon)
    {
        return NotifySprite[(int)notifyicon];
    }

    public Sprite GetUserSkillicon(UserSkillicon userSkillicon)
    {
        return UserSkillicon[(int)userSkillicon];
    }

    public Sprite GetCharacterImage(Character Char)
    {
        return CharacterImage[(int)Char.MyCharacter-(int)CharacterName.Strong];
    }
    public Sprite GetCharacterIcon(Character Char)
    {
        return CharacterIcon[(int)Char.MyCharacter-(int)CharacterName.Strong];
    }

    //캐릭터 얻기
    public GameObject GetCharacter(CharacterName name)
    {
        return Characters[(int)name - (int)CharacterName.Strong];
    }

    //몬스터 얻기
    public GameObject GetMonster(MonsterName name)
    {
        return Monsters[(int)name - (int)MonsterName.Basic];
    }

    //장애물 얻기
    public GameObject GetObstacle(ObstacleName name)
    {
        return Obstacles[(int)name];
    }
    //DB 다운로드
    public string DownLoad_DB(UnityWebRequest www)
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

    //캐릭터 수치 바꾸는 함수
    public string ChangeFigure(Character character,string s)
    {
        if (s.Contains('n'))
            s = s.Replace("n", character.Skill_UIFigure.ToString());

        return s;
    }

}

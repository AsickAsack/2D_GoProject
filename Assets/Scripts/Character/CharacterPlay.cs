using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterPlay : MonoBehaviour
{

    public Character character;

    bool IsPassive;

    public GameObject Effect;
    public int Index;
    public string Name;
    public string Character_Des;
    float Min_Power;
    float Max_Power;
    float Mass;
    float Drag;
    string Active_Des;
    float Active_Figure;
    string Passive_Des;
    float Passive_Figure;
    float Passive_Range;

    int Level;
    //말의 크기
    int Size;
    //스킬 강화효과
    int SkillEffect;
    //발사 위력
    //컷씬
    //사운드
    //이미지

    //public 

    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5.0f);

        GetNewDB();
    }


    public abstract void AcitveSkill();
    public abstract void PassiveSkill();
    
    public void GetNewDB()
    {
        Name = GameDB.CharacterDB[Index][(int)CharacterStat.Name];
        Character_Des = GameDB.CharacterDB[Index][(int)CharacterStat.Character_Des];
        /*
        Min_Power = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Min_Power]);
        Max_Power = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Max_Power]);
        Drag = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Drag]);
        Active_Des = GameDB.CharacterDB[Index][(int)CharacterStat.Active_Des];
        Active_Figure = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Active_Figure]);
        Passive_Des = GameDB.CharacterDB[Index][(int)CharacterStat.Passive_Des];
        Passive_Figure = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Passive_Figure]);
        Passive_Range = float.Parse(GameDB.CharacterDB[Index][(int)CharacterStat.Passive_Range]);
        */

    }

    
}

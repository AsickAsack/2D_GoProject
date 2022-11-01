using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{

    public Character() { }
    public Character(CharacterName name)
    {
        int temp = 0;
        Name = GameDB.CharacterDB[(int)name][temp++];
        Character_Des = GameDB.CharacterDB[(int)name][temp++];
        Min_Power = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Max_Power = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Mass = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Drag = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Active_Des = GameDB.CharacterDB[(int)name][temp++];
        active_target = (ActiveTarget)int.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Active_Figure = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Passive_Des = GameDB.CharacterDB[(int)name][temp++];
        Passive_probability = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Passive_Range = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
    }

    int Level;
    int Max_Level;
    int Size;
    
    float EXP;
    float Max_EXP;

    //맞을때, 때릴때, 스킬, 패시브 생각
    int SkillEffectindex;

    public string Name;
    public string Character_Des;
    float Min_Power;
    float Max_Power;
    float Mass;
    float Drag;
    string Active_Des;
    ActiveTarget active_target;
    float Active_Figure;
    string Passive_Des;
    float Passive_probability;
    float Passive_Range;

    
    //스킬 강화효과
    
    //발사 위력
    //컷씬
    //사운드
    //이미지



}


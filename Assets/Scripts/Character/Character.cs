using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Character
{

    public Character() { }
    public Character(int name)
    {
        MyCharacter = (CharacterName)name;
        int temp = 1;
        Name = GameDB.CharacterDB[(int)name][temp++];
        Character_Des = GameDB.CharacterDB[(int)name][temp++];
        Min_Power = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Max_Power = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Mass = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Drag = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Des = RePlaceString(GameDB.CharacterDB[(int)name][temp++]);
        active_target = (ActiveTarget)int.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Figure = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Type = int.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_probability = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Range = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Story = RePlaceString(GameDB.CharacterDB[(int)name][temp++]);
        GetTime = DateTime.Now;
    }


    public CharacterName MyCharacter;

    int grade;
    int Level=1;
    int Max_Level;
    int Size;
    
    float EXP;
    float Max_EXP;
    DateTime GetTime;
    //맞을때, 때릴때, 스킬, 패시브 생각
    int SkillEffectindex;

    public string Name;
    public string Character_Des;
    public float Min_Power;
    public float Max_Power;
    public float Mass;
    public float Drag;
    public string Skill_Des;
    public ActiveTarget active_target;
    public float Skill_Figure;
    public int Skill_Type;
    public float Skill_probability;
    public float Skill_Range;
    public string Story;

    //스킬 강화효과  
    //사운드
    //이미지 


    string RePlaceString(string story)
    {
        story = story.Replace("`","\n");
        story = story.Replace("~",",");

        return story;
    }

}


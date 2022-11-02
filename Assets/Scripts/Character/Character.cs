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
        Active_Des = GameDB.CharacterDB[(int)name][temp++];
        active_target = (ActiveTarget)int.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Active_Figure = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Passive_Des = GameDB.CharacterDB[(int)name][temp++];
        Passive_probability = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Passive_Range = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Story = GetStory(GameDB.CharacterDB[(int)name][temp++]);
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
    public string Active_Des;
    public ActiveTarget active_target;
    public float Active_Figure;
    public string Passive_Des;
    public float Passive_probability;
    public float Passive_Range;
    public string Story;

    //스킬 강화효과  
    //사운드
    //이미지 


    string GetStory(string story)
    {
        story = story.Replace("`","\n");
        story = story.Replace("~",",");

        return story;
    }

}


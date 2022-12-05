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
        Skill_UIFigure = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Figure = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Type = int.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_probability = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Skill_Range = float.Parse(GameDB.CharacterDB[(int)name][temp++]);
        Story = RePlaceString(GameDB.CharacterDB[(int)name][temp++]);
        GetTime = DateTime.Now;
    }

    public Character(Character character)
    {

        this.MyCharacter = character.MyCharacter;
        this.Name = character.Name;
        this.Character_Des = character.Character_Des;
        this.Min_Power = character.Min_Power;
        this.Max_Power = character.Max_Power;
        this.Mass = character.Mass;
        this.Drag = character.Drag;
        this.Skill_Des = character.Skill_Des;
        this.active_target = character.active_target;
        this.Skill_UIFigure = character.Skill_UIFigure;
        this.Skill_Figure = character.Skill_Figure;
        this.Skill_Type = character.Skill_Type;
        this.Skill_probability = character.Skill_probability;
        this.Skill_Range = character.Skill_Range;
        this.Story = character.Story;
        this.GetTime = DateTime.Now;

    }


    public CharacterName MyCharacter;

    int grade;
    int Level=1;
    int Max_Level;
    int Size;
    
    float EXP;
    float Max_EXP;
    public DateTime GetTime;
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
    public float Skill_UIFigure;
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


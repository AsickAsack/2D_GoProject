using System.Collections;
using System.Collections.Generic;

public class Monster
{

    public string Name;
    public string Skill_Des;
    public int Skill_Figure;
    public float Mass;
    public float Size;
  

    public Monster() { }
    public Monster(MonsterName name) 
    {
        int temp = 1;
        this.Name = GameDB.MonsterDB[(int)name][temp++];
        this.Skill_Des = GameDB.MonsterDB[(int)name][temp++];
        this.Skill_Figure = int.Parse(GameDB.MonsterDB[(int)name][temp++]);
        this.Mass = int.Parse(GameDB.MonsterDB[(int)name][temp++]);
        this.Size = float.Parse(GameDB.MonsterDB[(int)name][temp++]);
    }





}

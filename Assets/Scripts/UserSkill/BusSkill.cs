using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSkill : UserSkill
{
    public GameObject Bus;


    public override void Skill(Vector2 pos)
    {
        Bus obj = Instantiate(Bus, pos, Bus.transform.rotation).GetComponent<Bus>();
        obj.StartMove();
        SetSkillPoint();
        
    }

      
   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSkill : UserSkill
{
    public GameObject Bus;


    public override void Skill(Vector2 pos)
    {
        GameObject obj = Instantiate(Bus, pos, Bus.transform.rotation);
        SetSkillPoint();
        //obj.GetComponent<Bus>().GetComponent/
    }

      
   

}

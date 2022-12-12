using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSkill : UserSkill
{
    public GameObject Bus;

    private void Awake()
    {
        myIcon = UserSkillicon.Bus;
    }

    public override void Skill(Vector2 pos)
    {
        Bus obj = Instantiate(Bus, new Vector3(pos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).y), Bus.transform.rotation).GetComponent<Bus>();
        obj.StartMove();
        SetSkillPoint();
    }

      
   

}

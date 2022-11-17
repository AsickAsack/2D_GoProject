using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeesinSkill : ConfiltAndSKill
{

    public GameObject CloudBackGround;
    GameObject CloudObj;
    float OrgMass;
    float SkillPower = 1.0f;

    private void Awake()
    {
        OrgMass = MyRigid.mass;
        CloudObj = Instantiate(CloudBackGround);
        CloudObj.SetActive(false);
    }

    public void Set_MassAndSkillPower(float Mass, float SkillPower)
    {
        MyRigid.mass = Mass;
        this.SkillPower = SkillPower;
    }

    //이동할 때 구름 위치  생각?
    
    public override void CheckSKill(GameState SkillState)
    {
        switch(SkillState)
        {
            case GameState.Ready:
                Set_MassAndSkillPower(OrgMass * 2.0f, 4.0f);

                CloudObj.SetActive(true);
                break;

            case GameState.Move:
                CloudObj.SetActive(false);

                break;

            case GameState.End:
                Set_MassAndSkillPower(OrgMass, 1.0f);
                break;

        }
    }


    public override void GoForward(Vector2 Dir, float Power)
    {
        Debug.Log(SkillPower);
        MyRigid.AddForce(Dir * (Power * SkillPower), ForceMode2D.Impulse);
    }


    private void OnDisable()
    {
        Set_MassAndSkillPower(OrgMass, 1.0f);
        CloudObj.SetActive(false);
    }

    

}

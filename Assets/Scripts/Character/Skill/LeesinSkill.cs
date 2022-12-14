using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeesinSkill : CharacterSkill
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

    public override void ChangeRoutine()
    {
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
                Set_MassAndSkillPower(OrgMass * characterplay.character.Skill_Figure, characterplay.character.Skill_Figure);
                SoundManager.Instance.PlayEffect(19);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
    }


    public override void GoForward(Vector2 Dir, float Power, Transform tr)
    {
        MyRigid.AddForce(Dir * (Power * SkillPower), ForceMode2D.Impulse);
    }


  

}

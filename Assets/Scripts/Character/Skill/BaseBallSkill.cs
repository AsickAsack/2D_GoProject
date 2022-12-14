using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallSkill : CharacterSkill
{
    public GameObject BaseBall_Bat;
    Coroutine mySkillCo;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            if(mySkillCo != null)
            {
                StopCoroutine(mySkillCo);
                BaseBall_Bat.SetActive(false);
            }

            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
    }


    public override void CheckSKill(GameState SkillState)
    {
        switch (SkillState)
        {


            case GameState.Move:
                mySkillCo = StartCoroutine(Bat_Skill(MyRigid.velocity.magnitude * this.characterplay.character.Skill_Figure));
                
                break;


        }
    }

    


    IEnumerator Bat_Skill(float f)
    {
        while(MyRigid.velocity.magnitude > f)
        {
            yield return null;
        }

        MyRigid.velocity = Vector2.zero;
        BaseBall_Bat.SetActive(true);

        float d = 360.0f;

        while(d > 0.0f)
        {
            d -= Time.deltaTime * 720.0f;
            BaseBall_Bat.transform.Rotate(-Vector3.forward * Time.deltaTime *720.0f);
            yield return null;
        }

        BaseBall_Bat.SetActive(false);
    }

}

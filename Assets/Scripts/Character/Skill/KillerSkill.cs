using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSkill : ConflictAndSKill
{

    int MurDerCount = 0;

    private void OnEnable()
    {
        MurDerCount = 0;
    }

    public bool GetPassivePriority(CompareSkill other)
    {
        if (SkillPriority > other.SkillPriority)
            return true;
        else
            return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            if (OnBoard)
            {
                IsSKill = true;
                CompareSkill temp = collision.transform.GetComponent<CompareSkill>();

                if (temp == null)
                {
                        //실행
                        MurderSkill(collision);
                }
                //상대가 패시브가 있다면
                else
                {
                    if (temp.GetSkillPriority(this))
                    {
                        //실행
                        MurderSkill(collision);
                    }
                    else
                    {
                        ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                    }
                    
                }

                
            }
            else
            {
                ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
            }
        }
    }


    public void MurderSkill(Collision2D Other)
    {
        MurDerCount++;
        MyRigid.velocity = Vector2.zero;
        PlayManager.Instance.objectPool.GetActiveEffects(Skill_index, Other.transform.position);
        Other.transform.GetComponent<DeathProcess>()?.Death();
        IsSKill = false;
    }


}

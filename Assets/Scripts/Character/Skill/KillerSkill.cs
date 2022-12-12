using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSkill : CharacterSkill
{

    bool IsMurder = false;

    private void OnEnable()
    {
        IsMurder = false;
    }


    public bool GetPassivePriority(ICompareSkill other)
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
            if (characterplay.OnBoard)
            {
                IsSKill = true;
                ICompareSkill temp = collision.transform.GetComponent<ICompareSkill>();

                if (temp == null)
                {
                    //실행
                    if (!MurderSkill(collision))
                        ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                }
                else
                {
                    if (temp.GetSkillPriority(this))
                    {
                        //실행
                        if(!MurderSkill(collision))
                            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
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

    public bool MurderSkill(Collision2D Other)
    {
        if (!IsMurder)
        {
            IsMurder = true;
            MyRigid.velocity = Vector2.zero;
            PlayManager.Instance.objectPool.GetActiveEffects(Skill_index, Other.transform.position);
            Other.transform.GetComponent<IDeathProcess>()?.Death();
            IsSKill = false;
            return true;
        }
        else
            return false;
    }


}

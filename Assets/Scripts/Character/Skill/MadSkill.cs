using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadSkill : CharacterSkill
{
    int ConflictCount = 0;

    private void OnEnable()
    {
        ConflictCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            //서있는 말이 아니라면
            if (!characterplay.OnBoard)
            {
                ConflictCount++;

                if (ConflictCount == 3)
                {
                    IsSKill = true;
                    ICompareSkill temp = collision.transform.GetComponent<ICompareSkill>();

                    if (temp == null)
                    {
                        characterplay.ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                    }
                    else
                    {
                        if (temp.GetSkillPriority(this))
                        {
                            //실행
                            Skill(collision);
                        }
                        else
                        {
                            characterplay.ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                        }
                    }
                }
                else
                {
                    characterplay.ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                }
                
            }
            else
            {
                characterplay.ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
            }
        }
    }

    public void Skill(Collision2D collision)
    {
        PlayManager.Instance.objectPool.GetActiveEffects(Skill_index, collision.GetContact(0).point);

        Collider2D[] temp = Physics2D.OverlapCircleAll(this.transform.position, 2.0f);

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].CompareTag("EnemyBall") || temp[i].CompareTag("PlayerBall") && temp[i].transform != this.transform)
            {
                temp[i].GetComponent<IDeathProcess>()?.Death();
            }
        }

        IsSKill = false;
    }
}

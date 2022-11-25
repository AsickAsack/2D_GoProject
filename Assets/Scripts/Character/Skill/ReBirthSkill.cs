using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirthSkill : ConflictAndSKill
{

    bool Rebirth = false;
    public float ReBirthRange;

    public override void ListenToEvent(Skill_Condition Skill_Condition,Transform tr)
    {
        if (mySkill_Condition == Skill_Condition)
        {
            if (!Rebirth)
            {
                Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, ReBirthRange, 1 << LayerMask.NameToLayer("PlayerBall"));

                for (int i = 0; i < coll.Length; i++)
                {
                    if (coll[i].transform == tr)
                    {
                        Debug.Log("ȯ��!");
                        Rebirth = true;
                        PlayManager.Instance.ReBirthRoutine(tr);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("�̹� �ѹ� �츲");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

    }

    private void OnEnable()
    {
        Rebirth = false;
    }
}

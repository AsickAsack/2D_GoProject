using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirthSkill : CharacterSkill
{

    bool Rebirth = false;
    public float ReBirthRange;

    public override void ListenToEvent(Skill_Condition Skill_Condition,Transform tr)
    {
        if (mySkill_Condition == Skill_Condition && this.gameObject.activeSelf)
        {
            if (!Rebirth&&!IsSilence)
            {
                Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, ReBirthRange, 1 << LayerMask.NameToLayer("PlayerBall"));

                for (int i = 0; i < coll.Length; i++)
                {
                    if (coll[i].transform == tr)
                    {
                        SoundManager.Instance.PlayEffect(24);
                        Rebirth = true;
                        PlayManager.Instance.ReBirthRoutine(tr);
                        characterplay.PassiveRangeObj.SetActive(false);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("이미 한번 살림");
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

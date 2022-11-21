using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidSkill : ConflictAndSKill
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CompareCollisionTag(collision.transform))
        {
            if(OnBoard)
            {
                IsSKill = true;

                if (GetSkillPriority(this))
                {
                    Rigidbody2D TempRigid = collision.transform.GetComponent<Rigidbody2D>();
                    TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
                }
                else
                {
                    ConflictProcess(collision, 0.0f);
                }
            }
            else
            {
                ConflictProcess(collision, 0.0f);
            }

            IsSKill = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareCollisionTag(collision.transform))
        {
            if (OnBoard)
            {
                IsSKill = true;

                if (GetSkillPriority(this))
                {
                    Rigidbody2D TempRigid = collision.transform.GetComponent<Rigidbody2D>();
                    TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
                }
                else
                {
                    //ConflictProcess(collision, 0.0f);
                }
            }
            else
            {
                //ConflictProcess(collision, 0.0f);
            }

            IsSKill = false;
        }
    }


    public override void GoForward(Vector2 Dir, float Power)
    {
        this.transform.GetComponentInParent<Rigidbody2D>().AddForce(Dir * Power, ForceMode2D.Impulse);
    }

}

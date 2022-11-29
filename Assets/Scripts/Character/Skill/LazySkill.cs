using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazySkill : ConflictAndSKill
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall") || collision.gameObject.CompareTag("PlayerBall"))
        {
            //보드 위에 말이라면..
            if(OnBoard)
            {
                IsSKill = true;
                ICompareSkill temp = collision.transform.GetComponent<ICompareSkill>();

                if (temp == null)
                {
                    ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
                }
                else
                {
                    if (temp.GetSkillPriority(this))
                    {
                        this.MyRigid.velocity = Vector2.zero;
                        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.2f, 0.2f);
                        IsSKill = false;
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
}

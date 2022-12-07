using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlafSkill : CharacterSkill
{
    public GameObject IceObj;
    bool FirstHit = false;

    private void OnEnable()
    {
        FirstHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall") || collision.gameObject.CompareTag("PlayerBall"))
        {
            //���� ���� ���̶��..
            if(!characterplay.OnBoard && !FirstHit)
            {
                IsSKill = true;
                FirstHit = true;

                if (GetSkillPriority(collision.gameObject.GetComponent<ICompareSkill>()))
                {
                    Ice ice = Instantiate(IceObj).GetComponent<Ice>();
                    ice.SetIce(collision.gameObject);
                }

                IsSKill = false;
            }

            characterplay.ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StrongSKill : CharacterSkill
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            IsSKill = true;

            if (collision.gameObject.GetComponent<ICompareSkill>().GetSkillPriority(this))
            {
                if(!PlayerDB.Instance.playerdata.PlayFirst)
                    Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));
                else
                    Instantiate(TutorialPlaymanager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));

                collision.gameObject.GetComponent<Rigidbody2D>().velocity *= characterplay.character.Skill_Figure;
                ConflictProcess(collision, this.GetComponent<Rigidbody2D>().velocity.magnitude * 2.0f);
            }
            else
                ConflictProcess(collision, this.GetComponent<Rigidbody2D>().velocity.magnitude * 2.0f);

            IsSKill = false;
        }

    }

}

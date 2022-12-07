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

            if (GetSkillPriority(collision.gameObject.GetComponent<ICompareSkill>()))
            {
                Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));
                collision.gameObject.GetComponent<Rigidbody2D>().velocity *= characterplay.character.Skill_Figure;
                characterplay.ConflictProcess(collision, this.GetComponent<Rigidbody2D>().velocity.magnitude * 2.0f);
            }

            IsSKill = false;
        }

    }

}

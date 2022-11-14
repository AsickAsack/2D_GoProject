using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadActive : ActiveClass
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, 2.0f);
            GameObject obj = PlayManager.Instance.objectPool.GetEffect(1, this.transform.position, Quaternion.identity);

            for (int i = 0; i < coll.Length; i++)
            {
                if ((coll[i].CompareTag("EnemyBall") || coll[i].CompareTag("PlayerBall")) && coll[i].transform != this.transform)
                {
                    GameObject obj1 = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, coll[i].transform.position, Quaternion.identity);
                    coll[i].GetComponent<DeathProcess>()?.Death();

                }
            }
        }

        ConflictProcess(collision,myRigid2d.velocity.magnitude);

    }
}

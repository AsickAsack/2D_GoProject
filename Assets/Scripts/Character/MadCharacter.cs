using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadCharacter : CharacterPlay
{


    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        //PlayManager.Instance.ingameUI.OnActive();

        Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, 2.0f);
        GameObject obj = PlayManager.Instance.objectPool.GetEffect(ActivePrefab_Index, this.transform.position, Quaternion.identity);

        for (int i = 0; i < coll.Length; i++)
        {
            if ((coll[i].CompareTag("EnemyBall") || coll[i].CompareTag("PlayerBall")) && coll[i].transform != this.transform)
            {
                GameObject obj1 = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, coll[i].transform.position, Quaternion.identity);
                coll[i].GetComponent<DeathProcess>()?.Death();

            }
        }

        return true;
    }

    public override void PassiveSkill()
    {
        //ActiveSkill(Collision2D collision);
        Debug.Log("패시브야");
    }




}

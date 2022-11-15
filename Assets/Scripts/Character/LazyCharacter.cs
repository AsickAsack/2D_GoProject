using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LazyCharacter : CharacterPlay
{

    int Count = 0;

    public override UnityAction PassiveCheck(PassiveType passvieType, GameObject gameObject)
    {
        if (this.passiveType != passvieType) return null;

        Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, this.character.Passive_Range, 1 << LayerMask.NameToLayer("PlayerBall"));

        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].gameObject == gameObject)
            {
                Debug.Log("패시브!");
                Instantiate(PlayManager.Instance.objectPool.GetPoolEffect(EffectName.PlayerFall, this.transform.position, Quaternion.identity));
                return () => coll[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        return null;
    }



    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        Count++;

        if (Count == 2)
        {
            Debug.Log("액티브!");
            Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
            MyRigid.velocity *= 0.2f;
            return true;
        }
        return true;
    }

    public override void PassiveSkill()
    {
       
    }
}

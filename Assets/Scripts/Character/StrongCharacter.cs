using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class StrongCharacter : CharacterPlay
{

    public override UnityAction PassiveCheck(PassiveType passvieType, GameObject gameObject)
    {
        if (this.passiveType != passvieType) return null;

        Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, this.character.Passive_Range,1<<LayerMask.NameToLayer("PlayerBall"));

        for(int i=0;i<coll.Length;i++)
        {
            if(coll[i].gameObject == gameObject)
            {
                Debug.Log("패시브!");
                Instantiate(PlayManager.Instance.objectPool.GetPoolEffect(EffectName.PlayerFall, this.transform.position, Quaternion.identity));
                return () => coll[i].GetComponent<Rigidbody2D>().velocity *= 2.0f;
            }
        }

        return null;
    }

    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
        //충돌 시 밀어내는 힘 2배
        this.MyRigid.velocity *= this.character.Active_Figure;
        return true;
    }

    public override void PassiveSkill()
    {
        //주변 범위 안에서  충돌이 일어나면 2배의 힘으로 밀려나게함
    }
}

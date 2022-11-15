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
                Debug.Log("�нú�!");
                Instantiate(PlayManager.Instance.objectPool.GetPoolEffect(EffectName.PlayerFall, this.transform.position, Quaternion.identity));
                return () => coll[i].GetComponent<Rigidbody2D>().velocity *= 2.0f;
            }
        }

        return null;
    }

    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
        //�浹 �� �о�� �� 2��
        this.MyRigid.velocity *= this.character.Active_Figure;
        return true;
    }

    public override void PassiveSkill()
    {
        //�ֺ� ���� �ȿ���  �浹�� �Ͼ�� 2���� ������ �з�������
    }
}

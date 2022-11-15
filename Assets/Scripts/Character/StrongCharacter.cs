using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCharacter : CharacterPlay
{


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCharacter : CharacterPlay
{

    public override void AcitveSkill()
    {   //�浹 �� �о�� �� 2��, ��Ƽ�� �״��� Ȯ���ؾ���
        if (PlayManager.Instance.IsActive)
        {
            Debug.Log("��" + this.MyRigid.velocity);
            this.MyRigid.velocity *= 2;
            Debug.Log("��" + this.MyRigid.velocity);
        }
    }

    public override void PassiveSkill()
    {
        //�ֺ� ���� �ȿ���  �浹�� �Ͼ�� 2���� ������ �з�������
    }
}

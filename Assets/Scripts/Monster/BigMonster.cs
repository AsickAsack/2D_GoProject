using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonster : MonsterPlay
{
    public override void Death()
    {
        CountProcess();
    }

    public override void Initialize()
    {
        //���� ���� 
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        ConflictPlayer(collision);
    }

    public override void Skill()
    {
        //��� ��ų ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMonster : MonsterPlay
{
    public override void Death()
    {
        CountProcess();
    }

    public override void Initialize()
    {
       
        //�⺻ �������� �ʱ�ȭ
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        ConflictPlayer(collision);
    }

    public override void Skill()
    {
       //��ų ����
    }
}

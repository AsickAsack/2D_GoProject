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
        //질량 높게 
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        ConflictPlayer(collision);
    }

    public override void Skill()
    {
        //얘는 스킬 없음
    }
}

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
       
        //기본 스텟으로 초기화
    }

    public override void Skill()
    {
       //스킬 없음
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonsterPlay
{

    //생성할 몬스터 프리펩
    public GameObject MonsterPrefab;
    
    //생성할 가로세로 랜덤위치 제한
    public Vector2 RangeOfcreate;

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        //몬스터 프리펩, 기본 스텟, 레인지까지
    }

    public override void Skill()
    {
        GameObject Obj = Instantiate(MonsterPrefab,
            new Vector2(transform.position.x + Random.Range(RangeOfcreate.x, RangeOfcreate.y), transform.position.y + Random.Range(RangeOfcreate.x, RangeOfcreate.y)), Quaternion.identity);
    }

    




}

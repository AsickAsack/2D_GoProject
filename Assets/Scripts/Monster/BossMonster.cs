using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonsterPlay
{

    //������ ���� ������
    public GameObject MonsterPrefab;
    
    //������ ���μ��� ������ġ ����
    public Vector2 RangeOfcreate;

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        //���� ������, �⺻ ����, ����������
    }

    public override void Skill()
    {
        GameObject Obj = Instantiate(MonsterPrefab,
            new Vector2(transform.position.x + Random.Range(RangeOfcreate.x, RangeOfcreate.y), transform.position.y + Random.Range(RangeOfcreate.x, RangeOfcreate.y)), Quaternion.identity);
    }

    




}

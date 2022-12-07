using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonsterPlay
{

    //생성할 몬스터 프리펩
    public GameObject MonsterPrefab;
    public int CreateMonsterCount;
    float BossRadius;
    float offset = 0.2f;
    bool IsCreate = false;

    private void Awake()
    {
        BossRadius = Vector2.Distance(this.transform.position, this.GetComponent<CircleCollider2D>().bounds.max) * 2.0f; 
    }

    public override void InitForTurn()
    {
        IsCreate = false;
    }

    public override void Initialize()
    {
        //몬스터 프리펩, 기본 스텟, 레인지까지
    }


    public void Skill()
    {
        IsCreate = true;

        for (int i = 0; i < CreateMonsterCount; i++)
        {
            GameObject Obj = Instantiate(MonsterPrefab, GetCreatePos(), Quaternion.identity);
            Obj.GetComponent<MonsterPlay>().monster = new Monster(MonsterName.Basic);
            StageManager.instance.CurMonsters.Add(Obj.GetComponent<MonsterPlay>());
        }
        
        PlayManager.Instance.EnemyCount += CreateMonsterCount;
        
    }

    public Vector2 GetCreatePos()
    {
        /*
        RaycastHit2D[] MyHit = Physics2D.CircleCastAll(this.transform.position, MonsterPrefabRadius,
           direction, BossRadius + offset);*/

        return this.transform.position + (Vector3)new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * (BossRadius + offset);
    }
    public override void GoForward(Vector2 Dir, float Power, Transform tr)
    {
        if (!IsCreate)
        {
            Skill();
        }

        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }
}

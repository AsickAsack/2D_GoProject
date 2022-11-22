using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonsterPlay
{

    //생성할 몬스터 프리펩
    public GameObject MonsterPrefab;
    public int CreateMonsterCount;
    
    //생성할 가로세로 랜덤위치 제한
    public Vector2 RangeOfcreate;

    public override void Death()
    {
        CountProcess();
    }

    public override void Initialize()
    {
        //몬스터 프리펩, 기본 스텟, 레인지까지
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        
    }

    public override void Skill()
    {

        for (int i = 0; i < CreateMonsterCount; i++)
        {
            GameObject Obj = Instantiate(MonsterPrefab,this.transform.position, Quaternion.identity);
            Obj.GetComponent<MonsterPlay>().monster = new Monster(MonsterName.Basic);
            StageManager.instance.CurMonsters.Add(Obj.GetComponent<MonsterPlay>());
        }

        PlayManager.Instance.EnemyCount += CreateMonsterCount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            Skill();
        }
    }
}

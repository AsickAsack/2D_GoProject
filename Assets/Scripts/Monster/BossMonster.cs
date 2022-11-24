using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonsterPlay
{

    //������ ���� ������
    public GameObject MonsterPrefab;
    public int CreateMonsterCount;
    
    //������ ���μ��� ������ġ ����
    public Vector2 RangeOfcreate;

    public override void Death()
    {
        CountProcess();
    }

    public override void Initialize()
    {
        //���� ������, �⺻ ����, ����������
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

    public override void GoForward(Vector2 Dir, float Power)
    {
        //Skill();
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }
}

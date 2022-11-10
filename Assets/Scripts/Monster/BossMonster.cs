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
        CountProcess();
    }

    public override void Initialize()
    {
        //���� ������, �⺻ ����, ����������
    }

    public override void Skill()
    {
        Debug.Log(this.transform.position);

        GameObject Obj = Instantiate(MonsterPrefab,
            this.transform.position, Quaternion.identity);

        Obj.GetComponent<MonsterPlay>().monster = new Monster(MonsterName.Basic);

        GameObject Obj1 = Instantiate(MonsterPrefab,
            this.transform.position, Quaternion.identity);

        Obj1.GetComponent<MonsterPlay>().monster = new Monster(MonsterName.Basic);

        PlayManager.Instance.EnemyCount += 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            Skill();
        }
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMonster : Monster
{
    public GameObject BombEffect;
    int LimitCount;
    public float BombRange;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Skill()
    {
        GameObject obj = Instantiate(BombEffect, transform.position,Quaternion.identity);

        Collider2D[] MyCollider = Physics2D.OverlapCircleAll((Vector2)this.transform.position, BombRange);

        if(MyCollider.Length > 0)
        {
            for(int i=0;i<MyCollider.Length;i++)
            {
                //다른 죽이는 함수 있으면 그거 쓰기
                Destroy(MyCollider[i].gameObject);
            }
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMonster : MonsterPlay
{
    //public GameObject BombEffect;
    int LimitCount;
    public float BombRange;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Skill()
    {
        GameObject obj = Instantiate(Resources.Load("Bomb") as GameObject, transform.position,Quaternion.identity);

        Collider2D[] MyCollider = Physics2D.OverlapCircleAll((Vector2)this.transform.position, BombRange);

        if(MyCollider.Length > 0)
        {
            for(int i=0;i<MyCollider.Length;i++)
            {
                if(MyCollider[i].CompareTag("PlayerBall")|| MyCollider[i].CompareTag("EnemyBall"))
                Destroy(MyCollider[i].gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            Skill();
        }
    }


}

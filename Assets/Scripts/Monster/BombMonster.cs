using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMonster : MonsterPlay
{
    //public GameObject BombEffect;
    int LimitCount;
    public float BombRange;
    //public GameObject Circle;


    public override void Initialize()
    {
        
    }

  
    private void Update()
    {
        //Circle.transform.position = this.transform.position;
        //Circle.transform.localScale = new Vector2(BombRange*2, BombRange*2);
        
    }


    public override void Skill()
    {
        GameObject obj = Instantiate(Resources.Load("Bomb") as GameObject, transform.position,Quaternion.identity);
        obj.transform.localScale = new Vector2(BombRange, BombRange);

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

    public override void Death()
    {
        CountProcess();
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        
    }
}

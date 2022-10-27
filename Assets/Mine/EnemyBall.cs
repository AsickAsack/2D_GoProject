using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : Character
{

    public Vector2 myVelocity;

    (Vector2, Vector2) ChangeVelocity(Vector2 v1,Vector2 v2)
    {
        return (v2,v1);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //적 돌이 적을 쳤을때
        if(collision.transform.CompareTag("EnemyBall"))
        {
            Debug.Log("검은 돌끼리 맞았음");

            /*
            EnemyBall enemyBall = collision.transform.GetComponent<EnemyBall>();
            enemyBall.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, Power);
            */
            Vector2 v1 = this.MyRigid.velocity;
            Vector2 v2 = collision.transform.GetComponent<EnemyBall>().MyRigid.velocity;

            (collision.transform.GetComponent<EnemyBall>().MyRigid.velocity, this.MyRigid.velocity) = ChangeVelocity(v1,v2);
            
        }


    }

    private void Update()
    {
        myVelocity = MyRigid.velocity;
    }


    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        //MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
        MyRigid.velocity = Dir * this.Power;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : Character
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //적 돌이 적을 쳤을때
        if(collision.transform.CompareTag("EnemyBall"))
        {
            EnemyBall enemyBall = collision.transform.GetComponent<EnemyBall>();
            enemyBall.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, Power/2);
        }

        

        
    }


    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }
}

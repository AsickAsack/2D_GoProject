using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : TempCharacter
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //적 돌이 적을 쳤을때,,,
        if(collision.transform.CompareTag("EnemyBall"))
        {

            EnemyBall enemyBall = collision.transform.GetComponent<EnemyBall>();
            enemyBall.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude/10);
            
        }
  
        if (collision.transform.CompareTag("PlayerBall"))
        {
            
            PlayerBall PlayerBall = collision.transform.GetComponent<PlayerBall>();
            PlayerBall.GoForward((Vector2.Reflect(GetComponent<Rigidbody2D>().velocity,this.transform.up)), collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude/10);            
            

        }
       

        //서있는 흰돌 맞았을때도 생각
    }

    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
        
    }
}

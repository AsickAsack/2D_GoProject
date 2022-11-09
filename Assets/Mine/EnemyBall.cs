using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : TempCharacter
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //�� ���� ���� ������,,,
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
       

        //���ִ� �� �¾������� ����
    }

    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBall : TempCharacter
{
    public float dirspeed;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall"))
        {
            EnemyBall Enemy = collision.transform.GetComponent<EnemyBall>();

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized,this.GetComponent<Rigidbody2D>().velocity.magnitude);

            
            //줄어드는 파워
            //Power -= 5.0f;
        }

        //서있는 흰돌 맞았을때도 생각
    }

    public void GoForward(Vector2 Dir,float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }


}

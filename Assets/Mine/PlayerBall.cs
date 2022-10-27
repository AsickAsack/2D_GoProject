using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Character
{
    public float dirspeed;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall"))
        {
            //collision.transform.GetComponent<EnemyBall>().GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized,Power<0.0f ? 0.0f:Power);
            collision.transform.GetComponent<EnemyBall>().GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, 30f);
            //this.GoForward()

            //줄어드는 파워
            Power -= 5.0f;
        }
    }

    public void GoForward(Vector2 Dir,float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
        //MyRigid.velocity = Dir * this.Power;
    }

    private void Update()
    {
        this.Power -= Time.deltaTime* dirspeed;
    }

}

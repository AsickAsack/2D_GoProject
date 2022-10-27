using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Character
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall"))
        {
            collision.transform.GetComponent<EnemyBall>().GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized,Power);

            Power /= 2;
        }
    }

    public void GoForward(Vector2 Dir,float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }

    private void Update()
    {
        this.Power -= Time.deltaTime;
    }

}

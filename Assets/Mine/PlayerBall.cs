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

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized,Power<0.0f ? 1.0f:Power);


            //�پ��� �Ŀ�
            Power -= 5.0f;
        }

        //���ִ� �� �¾������� ����
    }

    public void GoForward(Vector2 Dir,float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }

    private void Update()
    {
        this.Power -= Time.deltaTime * dirspeed;
    }


}

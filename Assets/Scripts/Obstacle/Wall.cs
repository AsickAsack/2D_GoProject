using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{

    Vector2 mypos;
    private void Awake()
    {
        mypos = this.transform.position;
    }
    public override void Skill(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            SoundManager.Instance.PlayEffect(11);
            ICompareSkill Obj = collision.transform.GetComponent<ICompareSkill>();
            Obj.GetRigidBody().velocity = Vector2.Reflect(Obj.MyVelocity, -collision.GetContact(0).normal);

        }
    }

    public override void Skill(Collider2D collision)
    {
        //Ʈ����
    }
}

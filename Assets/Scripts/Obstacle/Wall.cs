using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    public override void Skill(Transform tr)
    {
        if (tr.CompareTag("PlayerBall") || tr.CompareTag("EnemyBall"))
        {
            Rigidbody2D TempRigid = tr.GetComponent<Rigidbody2D>();
            TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Skill(collision.transform);
    }




}

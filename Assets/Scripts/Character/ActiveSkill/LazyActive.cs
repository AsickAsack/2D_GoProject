using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyActive : ActiveClass
{
    int Count = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            ConflictProcess(collision,myRigid2d.velocity.magnitude);

            if(++Count == 2 )
            {
                myRigid2d.velocity *= 0.2f;
            }
        }
    }
}

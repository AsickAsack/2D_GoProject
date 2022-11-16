using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazySkill : ConfiltAndSKill
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall") || collision.gameObject.CompareTag("PlayerBall"))
        {
            if(OnBoard)
            {


            }
        }

    }
}

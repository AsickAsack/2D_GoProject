using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlafSkill : ConflictAndSKill
{
    public GameObject IceObj;
    bool FirstHit = false;

    private void OnEnable()
    {
        FirstHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBall") || collision.gameObject.CompareTag("PlayerBall"))
        {
            //보드 위에 말이라면..
            if(!OnBoard && !FirstHit)
            {
                IsSKill = true;
                FirstHit = true;

                Ice ice = Instantiate(IceObj).GetComponent<Ice>();
                ice.SetIce(collision.gameObject);

                IsSKill = false;
            }
               
            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
       

    }
}

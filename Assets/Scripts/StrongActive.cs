using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongActive : ActiveClass
{
    //��� �浹 ĳ������
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            //�º����϶�

            Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));
            //Debug.Log(myRigid2d.velocity.magnitude);
            //myRigid2d.velocity *= 2.0f;
            //Debug.Log(myRigid2d.velocity.magnitude);
           ConflictProcess(collision,myRigid2d.velocity.magnitude*2.0f);

        }

       
        
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StrongActive : ActiveClass
{
    //얘는 충돌 캐릭터임
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            
            Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));
            collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 2;
           ConflictProcess(collision,myRigid2d.velocity.magnitude*2.0f);

            
        }
        
    }



    public void sadf()
    {

    }
    
    
}

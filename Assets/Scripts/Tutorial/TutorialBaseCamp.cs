using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBaseCamp : MonoBehaviour
{
    public Vector2 ClearSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TutorialPlaymanager.Instance.gameState != GameState.Ready && (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall")))
        {
            Rigidbody2D TempRigid = collision.GetComponent<Rigidbody2D>();
            TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, ClearSize);
    }

    public void ClearBase()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(this.transform.position,this.GetComponent<BoxCollider2D>().size,0);

        for(int i=0;i<collider2Ds.Length;i++)
        {
            if(collider2Ds[i].CompareTag("PlayerBall")|| collider2Ds[i].CompareTag("EnemyBall"))
            {
                collider2Ds[i].GetComponent<IDeathProcess>()?.Death();
            }
        }
    }
}

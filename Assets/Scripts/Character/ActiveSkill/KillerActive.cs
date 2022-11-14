using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerActive : ActiveClass
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            PlayManager.Instance.objectPool.GetEffect(3, this.transform.position,Quaternion.identity);

            collision.gameObject.GetComponent<DeathProcess>()?.Death();

            myRigid2d.velocity *= 2.0f;
        }

        if (collision.transform.CompareTag("EnemyBall"))
        {

            Debug.Log("���� �÷��̾��ε� ���Ϳ� �浹��");
            GameObject Obj = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);

            MonsterPlay Enemy = collision.transform.GetComponent<MonsterPlay>();

            //AcitveSkill();


            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, myRigid2d.velocity.magnitude);
        }
    }
}

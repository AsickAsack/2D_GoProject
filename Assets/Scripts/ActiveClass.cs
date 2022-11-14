using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveClass : MonoBehaviour
{
    //�ñ�
    public GameState MyState;
    //����
    public bool Condition;
    //�ൿ
    public UnityAction myAction;
    //�ݶ��̴�
    public Collider2D myColl;

    public Rigidbody2D myRigid2d;

    private void Awake()
    {
        myRigid2d = this.GetComponent<Rigidbody2D>();
    }
    

    public void ConflictProcess(Collision2D collision,float Power)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {

            Debug.Log("���� �÷��̾��ε� ���Ϳ� �浹��");
            GameObject Obj = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);

            MonsterPlay Enemy = collision.transform.GetComponent<MonsterPlay>();

            //AcitveSkill();


            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, Power);
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {


            CharacterPlay Enemy = collision.transform.GetComponent<CharacterPlay>();

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, Power);

        }
    }

    


}

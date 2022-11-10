using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DeathProcess
{
    public void Death();
}

public abstract class MonsterPlay : MonoBehaviour, DeathProcess
{

    private Rigidbody2D _myRigid;
    public Rigidbody2D myRigid
    {
        get 
        {
            _myRigid = this.GetComponent<Rigidbody2D>();
            return _myRigid;
        }
    }

    public Monster monster;
    public float Power;

    public abstract void Initialize();
    public abstract void Skill();

    public abstract void PlayerConflicRoutine(Collision2D collision);


    private void Start()
    {
        Basic_init();
    }

    //��� ���͵��� �ʿ��� �ʱ�ȭ(����,ũ��)
    public void Basic_init()
    {
        myRigid.mass = monster.Mass;
        this.transform.localScale = new Vector2(monster.Size, monster.Size);
        Initialize();
    }

    public abstract void Death();

    public void CountProcess()
    {
        PlayManager.Instance.EnemyCount--;
        Destroy(this.gameObject);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerConflicRoutine(collision);

        BasicConflictMonster(collision);


    }



    public void BasicConflictPlayer(Collision2D collision)
    {
        //���ִ� �� �¾������� ����
        if (collision.transform.CompareTag("PlayerBall"))
        {
            Debug.Log("���� �����ε� �÷��̾�� �浹��");

            CharacterPlay Enemy = collision.transform.GetComponent<CharacterPlay>();

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
    }

    public void BasicConflictMonster(Collision2D collision)
    {
        //���Ͱ� ���Ϳ� �¾�����
        if (collision.transform.CompareTag("EnemyBall"))
        {
            MonsterPlay Enemy = collision.transform.GetComponent<MonsterPlay>();

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

        }

    }





    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        myRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
        
    }



}

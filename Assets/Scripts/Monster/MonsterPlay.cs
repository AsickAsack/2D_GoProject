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

    //모든 몬스터들이 필요한 초기화(질량,크기)
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
        //서있는 흰돌 맞았을때도 생각
        if (collision.transform.CompareTag("PlayerBall"))
        {
            Debug.Log("나는 몬스터인데 플레이어와 충돌함");

            CharacterPlay Enemy = collision.transform.GetComponent<CharacterPlay>();

            Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
    }

    public void BasicConflictMonster(Collision2D collision)
    {
        //몬스터가 몬스터에 맞았을때
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DeathProcess
{
    public void Death();
}

public abstract class MonsterPlay : MonoBehaviour, DeathProcess, Confilct, CompareSkill
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

    public int SkillPriority { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool IsSKill { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Monster monster;
    public float Power;
    public bool IsConflict = true;

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
        this.gameObject.SetActive(false);
        PlayManager.Instance.CurMultiKill++;
        PlayManager.Instance.CurKillStreaks++;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        ConflictPlayer(collision);



    }
    public void ConflictProcess_Myself(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        Rigidbody2D TempRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        myRigid.AddForce(((Vector2)this.transform.position - collision.GetContact(0).point).normalized * Power, ForceMode2D.Impulse);
    }


    public void ConflictPlayer(Collision2D collision)
    {
        //서있는 흰돌 맞았을때도 생각
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {

            CompareSkill CK = collision.transform.GetComponent<CompareSkill>();

            CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

        }
    }





    public void GoForward(Vector2 Dir, float temp)
    {
        //this.Power = Power;
        myRigid.AddForce(Dir * temp, ForceMode2D.Impulse);
        Debug.Log("들어온 파워" + this.Power);
    }

    public bool CheckConfilct()
    {
        return IsConflict;
    }

    public bool GetSkillPriority(CompareSkill other)
    {
        return true;
    }
}

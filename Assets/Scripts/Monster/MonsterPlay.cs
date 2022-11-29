using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathProcess
{
    public void Death();
}

public abstract class MonsterPlay : MonoBehaviour, IDeathProcess, IConfilct, ICompareSkill,IMoveCheck
{

    private Rigidbody2D _myRigid;
    public Rigidbody2D MyRigid
    {
        get 
        {
            _myRigid = this.GetComponent<Rigidbody2D>();
            return _myRigid;
        }
    }

    public int SkillPriority { get; set; }
    public bool IsSKill { get; set; }

    public Monster monster;
    public float Power;
    public bool IsConflict = true;
    public Vector2 MyVelocity { get; set; }
    public bool IsUserSKill { get; set; }

    public abstract void Initialize();
    public abstract void Skill();



    public virtual void Death()
    {
        CountProcess();
    }

    private void Start()
    {
        Basic_init();
    }

    //��� ���͵��� �ʿ��� �ʱ�ȭ(����,ũ��)
    public void Basic_init()
    {
        MyRigid.mass = monster.Mass;
        this.transform.localScale = new Vector2(monster.Size, monster.Size);
        Initialize();
    }

    private void Update()
    {
        //�����Ӹ��� ���ν�Ƽ �� ����(��ֹ��� �浹 ó�� ������)
        MyVelocity = MyRigid.velocity;
    }

    

    public virtual void CountProcess()
    {
        if (!this.gameObject.activeSelf) return;

        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, this.transform.position, Quaternion.identity);
        PlayManager.Instance.EnemyCount--;
        this.gameObject.SetActive(false);
        PlayManager.Instance.CurMultiKill++;
        
    }


    public Rigidbody2D GetRigidBody()
    {
        return MyRigid;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ConflictPlayer(collision);
    }


    public virtual void ConflictPlayer(Collision2D collision)
    {
        //���ִ� �� �¾������� ����
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {

            ICompareSkill CK = collision.transform.GetComponent<ICompareSkill>();

            CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, MyRigid.velocity.magnitude,this.transform);

        }
    }


    public virtual void GoForward(Vector2 Dir, float Power, Transform tr)
    {
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }




    public bool CheckConfilct()
    {
        return IsConflict;
    }

    public bool GetSkillPriority(ICompareSkill other)
    {
        return true;
    }

    public bool GetIsStop()
    {
        if (MyRigid.velocity == Vector2.zero)
            return true;
        else
            return false;
    }
}

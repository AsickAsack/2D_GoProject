using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathProcess
{
    public void Death();
    public void Death(int EffectIndex);
}

public abstract class MonsterPlay : MonoBehaviour, IDeathProcess, IConfilct, ICompareSkill,IMoveCheck, IHomeRun
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

    [SerializeField]
    int _SkillPriority;

    public int SkillPriority
    {
        get => _SkillPriority;
        set => _SkillPriority = value;  
    }
    public bool IsSKill { get; set; }

    public Monster monster;
    public float Power;
    public bool IsConflict = true;
    public Vector2 MyVelocity { get; set; }
    public bool IsUserSKill { get; set; }
    public bool IsHomeRun { get; set; } = false;

    public abstract void Initialize();

    public virtual void Death()
    {
        CountProcess();
    }
    public void Death(int EffectIndex)
    {
        if (!this.gameObject.activeSelf) return;

        PlayManager.Instance.objectPool.GetActiveEffects(EffectIndex, this.transform.position);
        PlayManager.Instance.EnemyCount--;
        this.gameObject.SetActive(false);
        PlayManager.Instance.CurMultiKill++;
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
        // ��� �켱 ������ �� ������ true�� ��������
        if (other.SkillPriority < this.SkillPriority)
        {
            if (other.IsSKill)
                return true;

            else
                return false;
        }
        else
        {
            if (this.IsSKill)
                return false;
            else
                return true;
        }
    }
    public bool CompareRoutine(ICompareSkill other)
    {
        if (other == null) return false;

        if (GetSkillPriority(other))
            return true;
        else
            return false;
    }

    //�ϸ��� �ʱ�ȭ�ؾߵǴ�
    public virtual void InitForTurn()
    {
        //
    }

    public bool GetIsStop()
    {
        if (MyRigid.velocity == Vector2.zero)
        {
            InitForTurn();
            return true;
        }
        else
            return false;
    }

    public void HomeRun()
    {
        IsHomeRun = true;
    }

    public void HomeRunRoutine(Transform tr)
    {
        if (IsHomeRun)
        {
            PlayManager.Instance.ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.HomeRun), "Ȩ��!");
            PlayManager.Instance.objectPool.GetActiveEffects(12,tr.position);
        }
    }
}

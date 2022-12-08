using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveCheck
{
    //���� �ߴ��� Ȯ���ϴ� bool��
    public bool IsUserSKill { get; set; }

    //���� �ߴ��� �������ִ� �Լ�
    public bool GetIsStop();
}


//��ų �켱���� �� & �浹 �������̽�
public interface ICompareSkill
{
    //���ν�Ƽ���� ������ ������Ƽ
    public Vector2 MyVelocity { get; set; }

    //��ų �켱����
    public int SkillPriority { get; set; }

    //��ų �ߵ� ��������?
    public bool IsSKill { get; set; }

    //���� ���� �켱������ ���ϴ� �Լ�
    public bool GetSkillPriority(ICompareSkill other);

    //��� �浹 ó���� �� �Լ��� ������
    public void GoForward(Vector2 Dir, float Power,Transform tr);

    //������ٵ� ���� �Լ�
    public Rigidbody2D GetRigidBody();

}

//��ų�� � ���ǿ� �����ϴ����� ���� ������
public enum Skill_Condition
{
   None,Confilct,Death
}

public class CharacterSkill : MonoBehaviour, IObserver, ICompareSkill
{
    
    [SerializeField]
    int _SkillPriority;
    public int Skill_index;
    public bool IgnoreObstacle;
    public bool IsSilence;
    public Skill_Condition mySkill_Condition;

    public Vector2 MyVelocity { get; set; }


    private Rigidbody2D _MyRigid;
    public Rigidbody2D MyRigid
    {
        get
        {
            if (_MyRigid == null)
                _MyRigid = this.GetComponent<Rigidbody2D>();

            return _MyRigid;
        }

    }
    public int SkillPriority
    {
        get
        {
            return _SkillPriority;
        }

        set
        {
            _SkillPriority = value;
        }
    }

    public bool IsSKill { get; set; } = false;

    public CharacterPlay characterplay;

    private void Awake()
    {
        characterplay = this.transform.GetComponent<CharacterPlay>();
    }

    private void OnEnable()
    {
        IsSKill = false;
    }

    private void Update()
    {
        //MyVelocity = MyRigid.velocity;
    }

    private void FixedUpdate()
    {
        MyVelocity = MyRigid.velocity;
    }

    //������ ���� �Լ�
    public virtual void GoForward(Vector2 Dir, float Power,Transform tr)
    {
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }


    //��ų üũ(��Ȳ�� �´� ��ų�� �ִ���)
    public virtual void CheckSKill(GameState SkillState)
    {
        return;
    }



    //������ �޼ҵ�
    public virtual void ListenToEvent(Skill_Condition Skill_Condition, Transform tr)
    {
        return;
    }


    public Rigidbody2D GetRigidBody()
    {
        return MyRigid;
    }


    public bool GetSkillPriority(ICompareSkill other)
    {
        if (IsSilence) return false;

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
   

    public bool CompareCollisionTag(Transform tr)
    {

        if (tr.CompareTag("EnemyBall") || tr.CompareTag("PlayerBall"))
            return true;
        else
           return false;
    }

    public virtual void ListenToGameState(GameState state)
    {
        //
    }
}
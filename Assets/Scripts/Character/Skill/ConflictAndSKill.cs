using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface CompareSkill
{
    public int SkillPriority { get; set; }
    public bool IsSKill { get; set; }
    public bool GetSkillPriority(CompareSkill other);
    public void GoForward(Vector2 Dir, float Power);

}
public enum Skill_Type
{
    Active,Passive
}

public enum Skill_Condition
{
    Ready,Shot,Confilct
}

public class ConflictAndSKill : MonoBehaviour, IObserver, CompareSkill
{
    public int Skill_index;
    [SerializeField]
    int _SkillPriority;
    public bool OnBoard;
    public GameState SkillState;
    public Skill_Type mySkill_Type;
    public Skill_Condition mySkill_Condition;

    public delegate void SkillDeligate();
    public SkillDeligate skillDeligate;

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

    private void OnEnable()
    {
        IsSKill = false;
    }

    //������ ���� �Լ�
    public virtual void GoForward(Vector2 Dir, float Power)
    {
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }


    
    //��ų üũ(��Ȳ�� �´� ��ų�� �ִ���)
    public virtual void CheckSKill(GameState SkillState)
    {
        if (SkillState != this.SkillState || skillDeligate == null) return;

        skillDeligate();
    }



    //������ �޼ҵ�
    public void ListenToSubeject(Skill_Type Skill_Type, Skill_Condition Skill_Condition)
    {
        if (this.mySkill_Type == Skill_Type && mySkill_Condition == Skill_Condition)
        {
            //��ų�� �޾ƾ� �Ҷ�

            if (this.mySkill_Type == Skill_Type)//���� �˻�
            {

            }
            else
                return; //������ ������
        }
        else
            return;
    }





    //�浹�� ��ƾ
    public void ConflictProcess(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        CompareSkill CK = collision.gameObject.GetComponent<CompareSkill>();
        CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, MyRigid.velocity.magnitude);
    }

    public void ConflictProcess_Myself(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        Rigidbody2D TempRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        MyRigid.AddForce(((Vector2)this.transform.position - collision.GetContact(0).point).normalized * Power, ForceMode2D.Impulse);
    }
    public bool GetSkillPriority(CompareSkill other)
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


    public bool CompareRoutine(CompareSkill other)
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

}

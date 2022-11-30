using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveCheck
{
    //정지 했는지 확인하는 bool값
    public bool IsUserSKill { get; set; }

    //정지 했는지 리턴해주는 함수
    public bool GetIsStop();
}


//스킬 우선순위 비교 & 충돌 인터페이스
public interface ICompareSkill
{
    //벨로시티값을 저장할 프로퍼티
    public Vector2 MyVelocity { get; set; }

    //스킬 우선순위
    public int SkillPriority { get; set; }

    //스킬 발동 조건인지?
    public bool IsSKill { get; set; }

    //상대와 나의 우선순위를 비교하는 함수
    public bool GetSkillPriority(ICompareSkill other);

    //모든 충돌 처리는 이 함수로 시작함
    public void GoForward(Vector2 Dir, float Power,Transform tr);

    //리지드바디 리턴 함수
    public Rigidbody2D GetRigidBody();

}

//스킬이 어떤 조건에 반응하는지에 대한 열거자
public enum Skill_Condition
{
   None,Confilct,Death
}

public class ConflictAndSKill : MonoBehaviour, IObserver, ICompareSkill
{
    
    [SerializeField]
    int _SkillPriority;
    public int Skill_index;
    public bool OnBoard;
    public bool IgnoreObstacle;
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

    public Character character;

    private void Awake()
    {
        character = this.GetComponent<CharacterPlay>().character;
    }

    private void OnEnable()
    {
        IsSKill = false;
    }

    private void Update()
    {
        MyVelocity = MyRigid.velocity;
    }

    //앞으로 가는 함수
    public virtual void GoForward(Vector2 Dir, float Power,Transform tr)
    {
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }


    //스킬 체크(상황에 맞는 스킬이 있는지)
    public virtual void CheckSKill(GameState SkillState)
    {
        return;
    }



    //옵저버 메소드
    public virtual void ListenToEvent(Skill_Condition Skill_Condition, Transform tr)
    {
        return;
    }


    public Rigidbody2D GetRigidBody()
    {
        return MyRigid;
    }


    //충돌시 루틴
    public void ConflictProcess(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        ICompareSkill CK = collision.gameObject.GetComponent<ICompareSkill>();
        CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, MyRigid.velocity.magnitude,this.transform);
    }


    public bool GetSkillPriority(ICompareSkill other)
    {
        // 상대 우선 순위가 더 높으면 true를 리턴해줌
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

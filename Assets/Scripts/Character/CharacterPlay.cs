using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Confilct
{
    public bool CheckConfilct();
}



public abstract class CharacterPlay : MonoBehaviour, DeathProcess, Confilct
{

    public Character character;
    public bool OnBoard =false;
    bool IsPassive;
    public bool IsConfilct;
    public bool IsActive;
    public int ActivePrefab_Index;


    public SpriteRenderer InGame_Sprite;
    public GameObject Effect;
    public float Power;
    public int Index;

    protected Rigidbody2D MyRigid
    {
        get => this.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        InGame_Sprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    

    public abstract bool ActiveSkill(Vector2 pos , Collision2D collision = null);
    public abstract void PassiveSkill();

    public virtual void ChangeONBorad()
    {
        OnBoard = true;
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 상대 돌과 부딪혔을때
        if (collision.transform.CompareTag("EnemyBall"))
        {
            CheckProcess(collision, ActiveTarget.Enemy);
        }
        else if (collision.transform.CompareTag("PlayerBall"))
        {
            CheckProcess(collision, ActiveTarget.Team);
        }

    }

    public void CheckProcess(Collision2D collision,ActiveTarget activeTarget)
    {
        if (character.active_target == activeTarget || character.active_target == ActiveTarget.All && !OnBoard)
        {
            if (PlayManager.Instance.IsActive)
            {
                if (ActiveSkill(collision.GetContact(0).point, collision))
                    ConflictProcess(collision, MyRigid.velocity.magnitude);
            }
            else
                ConflictProcess(collision, MyRigid.velocity.magnitude);
        }
        else
        {

            if(collision.gameObject.GetComponent<Confilct>().CheckConfilct())
            ConflictProcess(collision, MyRigid.velocity.magnitude);
            
        }
    }

    public void ConflictProcess(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        Rigidbody2D TempRigid = collision.gameObject.GetComponent<Rigidbody2D>();

        TempRigid.AddForce((collision.GetContact(0).point - (Vector2)this.transform.position).normalized * Power , ForceMode2D.Impulse);
    }


    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void ExitGame()
    {
        //구멍에 빠졌을때
        //그냥 떨어졌을때 -> 1. 살려줄 패시브가 있는지 탐색 or  // 2. 
        
    }


    public bool CheckConfilct()
    {
        return IsConfilct;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public interface IConfilct
{
    public bool CheckConfilct();
}
public enum PassiveType
{
    Conflict,Death
}


public class CharacterPlay : MonoBehaviour, IDeathProcess, IMoveCheck
{

    public Character character;
    public CharacterSkill MySkill;
    public bool OnBoard;
    public GameObject PassiveRangeObj;

    public SpriteRenderer InGame_Sprite;
    public float Power;
    public int Index;

    public Rigidbody2D MyRigid
    {
        get => this.GetComponent<Rigidbody2D>();
    }
    public bool IsUserSKill { get; set; }

    private void OnEnable()
    {
        MySkill.enabled = true;
    }

    private void Awake()
    {
        InGame_Sprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    /*
    public virtual void Rebirth()
    {
        OnBoard = false;
    }
    */
    public virtual void ChangeONBorad()
    {
        if(this != null)
        {
            PlayManager.Instance.OnBoardPlayer.Add(this.gameObject);
            if(PassiveRangeObj != null)
            PassiveRangeObj.SetActive(true);
            OnBoard = true;
        }
    }

    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        if (!this.gameObject.activeSelf) return;

        if(OnBoard)
        {
            PlayManager.Instance.RemoveObserver(this.gameObject);
        }
        OnBoard = false;
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, this.transform.position, Quaternion.identity);        
        PlayManager.Instance.NotifyEventToObservers(Skill_Condition.Death, this.transform);
        this.gameObject.SetActive(false);

    }



    public void Death(int EffectIndex)
    {

    }
    public void ExitGame()
    {
        //���ۿ� ��������
        //�׳� ���������� -> 1. ����� �нú갡 �ִ��� Ž�� or  // 2. 
        
    }

    public bool GetIsStop()
    {
        if (MyRigid.velocity == Vector2.zero)
            return true;
        else
            return false;
    }
}


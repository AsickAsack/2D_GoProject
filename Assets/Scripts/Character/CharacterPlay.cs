using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public interface Confilct
{
    public bool CheckConfilct();
}
public enum PassiveType
{
    Conflict,Death
}


public class CharacterPlay : MonoBehaviour, DeathProcess
{

    public Character character;
    public ConflictAndSKill MySkill;
    public bool OnBoard =false;
    
    public GameObject PassiveRangeObj;

    public SpriteRenderer InGame_Sprite;
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

    public virtual void Rebirth()
    {
        OnBoard = false;
    }
    
    public virtual void ChangeONBorad()
    {
        if(this != null)
        {
            //PlayManager.Instance.OnBoardPlayer.Add(this);
            if(PassiveRangeObj != null)
            PassiveRangeObj.SetActive(true);
            MySkill.OnBoard = true;
        }
    }

    public void GoForward(Vector2 Dir, float Power)
    {
        this.Power = Power;
        MyRigid.AddForce(Dir * this.Power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        //구멍에 빠졌을때
        //그냥 떨어졌을때 -> 1. 살려줄 패시브가 있는지 탐색 or  // 2. 
        
    }


  
}


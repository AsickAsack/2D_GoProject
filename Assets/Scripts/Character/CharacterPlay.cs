using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CharacterPlay : MonoBehaviour, DeathProcess
{
    public ActiveClass MyActive;
    public Character character;
    public bool OnBoard =false;
    bool IsPassive;
    public bool IsActive;

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
    

    public abstract void AcitveSkill();
    public abstract void PassiveSkill();


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        //보드에 있을때
        if (OnBoard)
        {
            Debug.Log("온보드");
            if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
            {
                int rand = Random.Range(1, 11);

                if (rand < 4)
                    PassiveSkill();
            }

        }
        else //아닐때. 즉, 쏠 때
        {
            Debug.Log("온보드 아님");
            if (collision.transform.CompareTag("EnemyBall"))
            {

                Debug.Log("나는 플레이어인데 몬스터와 충돌함");
                GameObject Obj= PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit,collision.GetContact(0).point,Quaternion.identity);

                MonsterPlay Enemy = collision.transform.GetComponent<MonsterPlay>();

                //AcitveSkill();

                
                    Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

                

            }

            if (collision.transform.CompareTag("PlayerBall"))
            {
                

                CharacterPlay Enemy = collision.transform.GetComponent<CharacterPlay>();

                Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

            }
        }
        //서있는 흰돌 맞았을때도 생각
        */
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
  
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CharacterPlay : MonoBehaviour, DeathProcess
{
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

    /*
    private void Update()
    {
        if(OnBoard)
        {
            PassiveSkill();
        }
    }
    */
    
    public abstract void AcitveSkill();
    public abstract void PassiveSkill();
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        else //아닐때
        {
            Debug.Log("온보드 아님");
            if (collision.transform.CompareTag("EnemyBall"))
            {
                EnemyBall Enemy = collision.transform.GetComponent<EnemyBall>();

                AcitveSkill();

                Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

            }

            if (collision.transform.CompareTag("PlayerBall"))
            {

                CharacterPlay Enemy = collision.transform.GetComponent<CharacterPlay>();

                Enemy.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, this.GetComponent<Rigidbody2D>().velocity.magnitude);

            }
        }
        //서있는 흰돌 맞았을때도 생각
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
}


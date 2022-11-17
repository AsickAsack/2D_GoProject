using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkill : ConfiltAndSKill, ComparePassive
{
    public GameObject Candy;
    public CircleCollider2D mycoll;

    public int PassivePriority { get; set; }
    public bool IsPassive { get; set ; }

    private void Awake()
    {

        mycoll = this.GetComponent<CircleCollider2D>();
    }

    public override void CheckSKill(GameState SkillState)
    {
        switch (SkillState)
        {

            case GameState.Ready:
                //MyRigid.simulated = false;
                break;

            case GameState.Move:
                StartCoroutine(GoFly());
                break;


        }
    }

    public override void GoForward(Vector2 Dir, float Power)
    {
        MyRigid.AddForce(Dir * (Power/1.5f), ForceMode2D.Impulse);
    }



    IEnumerator GoFly()
    {
        IsPassive = true;
        mycoll.isTrigger = true;
        //MyRigid.simulated = false;
        while (true)
        {
            float x = this.transform.localScale.x + (Time.deltaTime * 2.0f);
            x = Mathf.Clamp(x,0.2f, 0.4f);
            this.transform.localScale = new Vector2(x, x);
            
            



            if (MyRigid.velocity.magnitude < 1.0f)
            {
                
                break;
            }

            yield return null;
        }

        Instantiate(Candy, this.transform.position, Quaternion.identity);

        while (true)
        {
            float x = this.transform.localScale.x - (Time.deltaTime * 1.0f);
            x = Mathf.Clamp(x, 0.2f, 0.4f);
            this.transform.localScale = new Vector2(x, x);

            if(Mathf.Approximately(x,0.2f))
            {
                //mycoll.isTrigger = false;
                break;
            }

            yield return null;
        }
        

        Collider2D[] tempcoll = Physics2D.OverlapCircleAll(this.transform.position, 1.0f);

        for(int i=0;i<tempcoll.Length;i++)
        {
            if(tempcoll[i].transform.CompareTag("EnemyBall") || tempcoll[i].transform.CompareTag("PlayerBall") && tempcoll[i].transform != this.transform)
            {
                Rigidbody2D temprigid = tempcoll[i].GetComponent<Rigidbody2D>();
                temprigid.AddForce((tempcoll[i].transform.position - this.transform.position).normalized * 5.0f, ForceMode2D.Impulse);
            }
        }

        mycoll.isTrigger = false;
    }

    public bool GetPassivePriority(ComparePassive other)
    {
        throw new System.NotImplementedException();
    }
}

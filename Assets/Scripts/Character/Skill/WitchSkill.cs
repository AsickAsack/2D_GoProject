using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkill : ConflictAndSKill
{
    public GameObject Candy;
    public int CandyCount;
    public float CandyRadius;
    public CircleCollider2D mycoll;

    private void Awake()
    {
        mycoll = this.GetComponent<CircleCollider2D>();
        CandyRadius = Candy.GetComponent<CircleCollider2D>().radius;
    }

    private void OnEnable()
    {
        this.transform.localScale = new Vector2(0.2f, 0.2f);
    }

    public override void CheckSKill(GameState SkillState)
    {
        switch (SkillState)
        {

            case GameState.Ready:
                //MyRigid.simulated = false;
                break;

            case GameState.Move:
                StartCoroutine(GoFly(MyRigid.velocity.magnitude * 0.8f));
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            ConflictProcess(collision, collision.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
        
    }


    public override void GoForward(Vector2 Dir, float Power)
    {
        MyRigid.AddForce(Dir * (Power/1.5f), ForceMode2D.Impulse); //늘어난 크기만큼 날아가는 파워를 줄임..-> 변f수화 고민
    }



    IEnumerator GoFly(float StartMagunitude)
    {

        IsSKill = true;
        mycoll.isTrigger = true;
        IgnoreObstacle = true;
        float ThrowCandyDistance = StartMagunitude * 0.4f;
        float LastThrowY = -2.5f;

        //MyRigid.simulated = false;
        while (true)
        {
            if (MyRigid.velocity.magnitude > 1.0f)
            {
                float x = this.transform.localScale.x + (Time.deltaTime * 2.0f);
                x = Mathf.Clamp(x, 0.2f, 0.4f);
                this.transform.localScale = new Vector2(x, x);
            }
            else
            {  
                    float x = this.transform.localScale.x - (Time.deltaTime * 1.0f);
                    x = Mathf.Clamp(x, 0.2f, 0.4f);
                    this.transform.localScale = new Vector2(x, x);

                    if (Mathf.Approximately(x, 0.2f))
                    {
                        //mycoll.isTrigger = false;
                        break;
                    }
                 
            }

            if (MyRigid.velocity.magnitude < StartMagunitude && MyRigid.velocity.magnitude > StartMagunitude - ThrowCandyDistance /*&& mycoll.bounds.min.y > LastThrowY*/)
            {
                Instantiate(Candy, GetCandyPos(this.transform, LastThrowY), Quaternion.identity);
                StartMagunitude -= ThrowCandyDistance;
                LastThrowY = mycoll.bounds.max.y;
            }

            yield return null;
        }

        PlayManager.Instance.objectPool.GetActiveEffects(8, this.transform.position);

        Collider2D[] tempcoll = Physics2D.OverlapCircleAll(this.transform.position, 1.0f);

        for (int i = 0; i < tempcoll.Length; i++)
        {
            if (tempcoll[i].transform.CompareTag("EnemyBall") || tempcoll[i].transform.CompareTag("PlayerBall") && tempcoll[i].transform != this.transform)
            {
                ICompareSkill temprigid = tempcoll[i].GetComponent<ICompareSkill>();
                temprigid.GoForward((tempcoll[i].transform.position - this.transform.position).normalized,5.0f);
            }
        }

        IsSKill = false;
        mycoll.isTrigger = false;
        IgnoreObstacle = false;
    }


    public void CandyPosCheck()
    {
        Collider2D[] tempcoll = Physics2D.OverlapCircleAll(this.transform.position, 1.0f);

        for (int i = 0; i < tempcoll.Length; i++)
        {
            if (tempcoll[i].transform.CompareTag("EnemyBall") || tempcoll[i].transform.CompareTag("PlayerBall") && tempcoll[i].transform != this.transform)
            {
                Rigidbody2D temprigid = tempcoll[i].GetComponent<Rigidbody2D>();
                temprigid.AddForce((tempcoll[i].transform.position - this.transform.position).normalized * 5.0f, ForceMode2D.Impulse);
            }
        }
    }



    public Vector2 GetCandyPos(Transform tr,float Y)
    {
        Vector2 testpos = new Vector2(Random.Range(mycoll.bounds.min.x,mycoll.bounds.max.x), Random.Range(Y, mycoll.bounds.max.y));

        Collider2D[] temp = Physics2D.OverlapCircleAll(testpos,CandyRadius/(1/Candy.transform.localScale.x),1<<LayerMask.NameToLayer("EnemyBall"));
        
        if(temp.Length != 0)
        {
            return new Vector2(100.0f,100.0f);
        }

        //여기 코드 수정하자..

        return testpos;
    }

   
}

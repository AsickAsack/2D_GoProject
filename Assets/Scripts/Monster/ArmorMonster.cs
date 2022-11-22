using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorMonster : MonsterPlay
{
    public GameObject Helmet;
    public bool IsHelmet = true;

    private void Awake()
    {
        
    }

    public override void Death()
    {
       CountProcess();
    }

    IEnumerator TakeOff_Helmet(Vector2 dir)
    {
        float time = 5.0f;
        while (0.0 < time)
        {
            time -= Time.deltaTime;
           // Helmet.transform.SetParent(null);
            Helmet.transform.GetComponentInChildren<Transform>().Rotate(Vector3.forward * Time.deltaTime * 360.0f);
            Helmet.transform.position += (Vector3)dir * Time.deltaTime * 5.0f;

            yield return null;
        }

        //Instantiate(PlayManager.Instance.objectPool.PoolEffectPrefab[1], Helmet.transform.position, Quaternion.identity);
        Destroy(Helmet.gameObject);

        
        //myRigid.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void ConflictPlayer(Collision2D collision)
    {
        //서있는 흰돌 맞았을때도 생각
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            CompareSkill CK = collision.transform.GetComponent<CompareSkill>();
            CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, myRigid.velocity.magnitude);

            if (IsHelmet)
            {
                StartCoroutine(TakeOff_Helmet(((Vector2)this.transform.position-collision.GetContact(0).point).normalized));
                
            }

        }
    }
    public override void GoForward(Vector2 Dir, float temp)
    {
        if (IsHelmet)
        {
            myRigid.velocity = Vector2.zero;
            IsHelmet = false;
            return;
        }


        myRigid.AddForce(Dir * temp, ForceMode2D.Impulse);
    }

    public override void Initialize()
    {
        //
    }

    
    public override void Skill()
    {
        if (IsHelmet)
        {
            //헬멧 날아가기
    //        StartCoroutine(TakeOff_Helmet());
        }
    }
    
    public override void PlayerConflicRoutine(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {

        }
    }
}

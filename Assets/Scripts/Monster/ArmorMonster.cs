using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorMonster : MonsterPlay
{
    public GameObject Helmet;
    public bool IsHelmet = true;

    public override void Death()
    {
       CountProcess();
    }

    IEnumerator TakeOff_Helmet()
    {
        float time = 5.0f;
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            Helmet.transform.Rotate(Vector3.forward * Time.deltaTime * 5.0f);
            Helmet.transform.Translate((Vector2.right + Vector2.up) * Time.deltaTime * 5.0f);

            yield return null;
        }

        //Instantiate(PlayManager.Instance.objectPool.PoolEffectPrefab[1], Helmet.transform.position, Quaternion.identity);
        Destroy(Helmet.gameObject);

        IsHelmet = false;
        //myRigid.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void Initialize()
    {
        //
    }

    public override void Skill()
    {
        if (IsHelmet)
        {
            //Çï¸ä ³¯¾Æ°¡±â
            StartCoroutine(TakeOff_Helmet());
        }
    }

    public override void PlayerConflicRoutine(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {

        }
    }
}

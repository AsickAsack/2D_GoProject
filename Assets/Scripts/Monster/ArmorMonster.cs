using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorMonster : MonsterPlay
{
    public GameObject Helmet;

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

        Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], Helmet.transform.position, Quaternion.identity);
        Destroy(Helmet.gameObject);

        Helmet = null;
    }

    public override void Initialize()
    {
        //
    }

    public override void Skill()
    {
        if (Helmet != null)
        {
            //Çï¸ä ³¯¾Æ°¡±â
            StartCoroutine(TakeOff_Helmet());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            Skill();
        }
    }
}

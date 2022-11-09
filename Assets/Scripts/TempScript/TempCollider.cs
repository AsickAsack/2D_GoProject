using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("EnemyBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            obj.GetComponent<Death>().Death();
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            obj.GetComponent<Death>()?.Death();
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}

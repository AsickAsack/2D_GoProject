using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCollider : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            collision.GetComponent<DeathProcess>()?.Death();
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], collision.transform.position, Quaternion.identity);
            collision.GetComponent<DeathProcess>()?.Death();
        }
    }
    
}

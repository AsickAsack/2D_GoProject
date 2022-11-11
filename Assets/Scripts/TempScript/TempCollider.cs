using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCollider : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            GameObject obj = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, collision.transform.position, Quaternion.identity);
            collision.GetComponent<DeathProcess>()?.Death();
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            GameObject obj = PlayManager.Instance.objectPool.GetPoolEffect(EffectName.MonsterFall, collision.transform.position, Quaternion.identity);
            collision.GetComponent<DeathProcess>()?.Death();
        }
    }
    
}

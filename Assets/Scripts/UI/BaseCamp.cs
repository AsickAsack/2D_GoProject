using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayManager.Instance.gameState != GameState.Ready && (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall")))
        {
            Rigidbody2D TempRigid = collision.GetComponent<Rigidbody2D>();
            TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
        }
    }
}

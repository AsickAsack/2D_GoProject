using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D tempRigid = collision.GetComponent<Rigidbody2D>();
            tempRigid.velocity *= 2.0f;

            this.gameObject.SetActive(false);
        }
    }
}

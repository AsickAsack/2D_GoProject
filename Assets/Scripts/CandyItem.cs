using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyItem : MonoBehaviour
{

    public float Power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Candy(collision.transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Candy(collision.transform);
    }

    public void Candy(Transform tr)
    {
        if (tr.CompareTag("PlayerBall") || tr.CompareTag("EnemyBall"))
        {
            if (tr.GetComponent<Collider2D>().isTrigger) return;

            Rigidbody2D tempRigid = tr.GetComponent<Rigidbody2D>();

            tempRigid.velocity *= Power;

            this.gameObject.SetActive(false);
        }
    }
}

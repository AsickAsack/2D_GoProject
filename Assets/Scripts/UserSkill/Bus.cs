using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D temp = collision.gameObject.GetComponent<Rigidbody2D>();

            temp.AddForce(((Vector2)collision.transform.position-collision.GetContact(0).point).normalized * 10.0f,ForceMode2D.Impulse);
            
        }
    }

    private void Update()
    {
        this.transform.position = this.transform.position + Vector3.up * Time.deltaTime * 20.0f;
    }
}

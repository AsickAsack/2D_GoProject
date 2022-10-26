using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ball"))
        {
            Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 velocity = collision.gameObject.GetComponent<Ball>().velocity;
            ballRB.velocity = Vector2.Reflect(velocity, -collision.GetContact(0).normal);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 StartVelocity = new Vector2(10.0f, 10.0f);
    public Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 6.0f,ForceMode2D.Impulse);

    }
}

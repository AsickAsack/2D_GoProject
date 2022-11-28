using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,Down,Left,Right
}


public class Booster : Obstacle
{
    public Transform Arrow;
    public Direction myDirection;
    public float Power;
    Vector2 ArrowDirection;

    private void Awake()
    {
        myDirection = (Direction)Random.Range(0, 5);
        SetArrow();
    }

    public void SetArrow()
    {
        switch(myDirection)
        {
            case Direction.Up:

                Arrow.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                ArrowDirection = Vector2.up;
                break;
            case Direction.Down:

                Arrow.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                ArrowDirection = Vector2.down;
                break;
            case Direction.Left:

                Arrow.rotation = Quaternion.Euler(0.0f, 0.0f, -180.0f);
                ArrowDirection = Vector2.left;
                break;
            case Direction.Right:

                //기본이 오른쪽
                ArrowDirection = Vector2.right;
                break;
        }
    }
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<ConflictAndSKill>().IgnoreObstacle) return;

        Skill(collision);
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<ConflictAndSKill>().IgnoreObstacle) return;

        Skill(collision);
    }

    
    public override void Skill(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
            Debug.Log(collision.transform);
    }
    
    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D myRigid = collision.transform.GetComponent<Rigidbody2D>();
            myRigid.velocity = Vector2.zero;
            myRigid.AddForce(ArrowDirection * Power, ForceMode2D.Impulse);
        }
    }
}

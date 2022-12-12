using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictCollider : MonoBehaviour
{

    public Transform Board;
    public Vector2 YLimit;
    public Vector2 XLimit;

    private void Awake()
    {
        XLimit.x = Board.position.x - Board.localScale.x * 10;
        XLimit.y = Board.position.x + Board.localScale.x * 10;

        Debug.Log(Board.position.x);
        Debug.Log(Board.localScale.x);
        Debug.Log(XLimit.x);

        YLimit.x = Board.position.y - Board.localScale.y * 10;
        YLimit.y = Board.position.y + Board.localScale.y * 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            if (CheckPos(collision.transform))
            {
                collision.GetComponent<IDeathProcess>()?.Death();
            }
        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            if (CheckPos(collision.transform))
            {
                collision.GetComponent<IDeathProcess>()?.Death();
            }
        }
    }

    public bool CheckPos(Transform tr)
    {
        if (tr.position.x < XLimit.x || tr.position.x > XLimit.y || tr.position.y < YLimit.x || tr.position.y > YLimit.y)
            return true;
        else
            return false;
    }
    
}


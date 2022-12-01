using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallBat : MonoBehaviour
{
    [SerializeField]
    Collider2D myColl;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            Debug.Log("트리거엔터" + collision.transform);
            ICompareSkill Enemy = collision.transform.GetComponent<ICompareSkill>();

            Enemy.GoForward((collision.transform.position-myColl.bounds.center).normalized, 20.0f,this.transform);
        }
    }
}

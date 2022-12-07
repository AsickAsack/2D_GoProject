using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHomeRun
{
    public bool IsHomeRun { get; set;}
    public void HomeRun();
    public void HomeRunRoutine(Transform tr);
}

public class BaseBallBat : MonoBehaviour
{
    [SerializeField]
    Collider2D myColl;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall") || collision.transform.CompareTag("PlayerBall"))
        {
            ICompareSkill Enemy = collision.transform.GetComponent<ICompareSkill>();
            Enemy.GoForward((collision.transform.position - myColl.bounds.center).normalized, 20.0f,this.transform);

            //IHomerun�������̽��� null�� �ƴ϶�� Ȩ���Լ� ����
            collision.GetComponent<IHomeRun>()?.HomeRun();
        }
    }
}

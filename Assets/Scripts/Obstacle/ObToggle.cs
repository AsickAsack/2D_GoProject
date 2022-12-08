using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObToggle : Obstacle
{
    List<Obstacle> ConnectObstacle;
    public GameObject[] ToggleObj;
    bool IsToggle = false;

    private void OnEnable()
    {
        IsToggle = false;   
    }

    private void Awake()
    {
        ConnectObstacle = new List<Obstacle>();
    }

    public void ResetToggle()
    {
        IsToggle = false;
    }

    //����� ��ֹ����� ��������
    public void SetConnect(Obstacle obstacles)
    {
       ConnectObstacle.Add(obstacles);
    }
    
    public override void Skill(Collision2D collision)
    {
        //
    }

    //��ֹ����� Ȱ��ȭ ���¸� �ݴ�� �ٲ���
    public override void Skill(Collider2D collision)
    {

        if ((collision.gameObject.CompareTag("PlayerBall") || collision.gameObject.CompareTag("EnemyBall")) && !IsToggle)
        {

            for (int i = 0; i < ToggleObj.Length; i++)
            {
                ToggleObj[i].SetActive(!ToggleObj[i].activeSelf);
            }

            for (int i = 0; i < ConnectObstacle.Count; i++)
            {
                ConnectObstacle[i].gameObject.SetActive(!ConnectObstacle[i].gameObject.activeSelf);
            }

            IsToggle = true;
        }
    }

    
}

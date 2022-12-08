using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObToggle : Obstacle
{
    List<Obstacle> ConnectObstacle;
    public GameObject[] ToggleObj;

    private void Awake()
    {
        ConnectObstacle = new List<Obstacle>();
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
        for(int i=0;i<ToggleObj.Length;i++)
        {
            ToggleObj[i].SetActive(!ToggleObj[i].activeSelf);
        }

        for(int i=0;i<ConnectObstacle.Count;i++)
        {
            ConnectObstacle[i].gameObject.SetActive(!ConnectObstacle[i].gameObject.activeSelf);
        }
    }

    
}

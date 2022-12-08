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

    //연결된 장애물들을 세팅해줌
    public void SetConnect(Obstacle obstacles)
    {
       ConnectObstacle.Add(obstacles);
    }
    
    public override void Skill(Collision2D collision)
    {
        //
    }

    //장애물들의 활성화 상태를 반대로 바꿔줌
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

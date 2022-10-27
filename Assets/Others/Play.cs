using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject Player;
    public GameObject CurBall;
    Vector2 CurPos;
    Vector2 EndPos;

    void Update()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.DrawLine(CurBall.transform.position, MousePos);

    }
}

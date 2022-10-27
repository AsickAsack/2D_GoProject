using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    
    bool IsHit;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 targetPos;
    RaycastHit2D MyRayCast;

    //파워 관련
    public float Power;
    public Vector2 LimitPower;

    //오브젝트
    public PlayerBall CurPlayer;
    public GameObject Arrow;
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("PlayerBall"));

            if (MyRayCast)
            {
                //Arrow.gameObject.SetActive(true);
                StartPos = Input.mousePosition;
                IsHit = true;
            }
        }

        if(Input.GetMouseButton(0))
        {
            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));

            if (IsHit)
            {

                targetPos = MyRayCast.point;

               // if (Arrow.transform.rotation.eulerAngles.z > 180.0f) tempM = 180.0f; else tempM = 0;
                //
                //Arrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f,Arrow.transform.rotation.eulerAngles.z-Vector2.Angle(Vector2.up, targetPos)- tempM));
                Debug.DrawLine(CurPlayer.transform.position, targetPos, Color.red);
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (IsHit)
            {
                EndPos = Input.mousePosition;
                //Arrow.gameObject.SetActive(false);
                //적당한 파워를 위해 20 낮춤
                Power = Vector2.Distance(StartPos, EndPos)/20.0f;
                Power = Mathf.Clamp(Power, LimitPower.x, LimitPower.y);

                //플레이어 스크립트에서 보내기
                CurPlayer.GoForward(-(targetPos - (Vector2)CurPlayer.transform.position).normalized, Power);
                IsHit = false;
            }
        }
    }
}

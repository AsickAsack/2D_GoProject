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

    //화면
    [SerializeField]
    float CameraMoveSpeed = 5.0f;
    [SerializeField]
    float CameraSmooth = 20.0f;


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("PlayerBall"));

            StartPos = Input.mousePosition;

            if (MyRayCast)
            {
                Arrow.transform.position = CurPlayer.transform.position;
                Arrow.gameObject.SetActive(true);    
                IsHit = true;
            }
        }

        
        
        if (Input.GetMouseButton(0))
        {
            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));

            if (IsHit)
            {

                targetPos = -(MyRayCast.point - (Vector2)CurPlayer.transform.position).normalized;

                float temp = 0.0f;

                //음수면 왼쪽, 양수면 오른쪽
                if (Vector2.Dot(Vector2.right, targetPos) < 0.0f)
                    temp = 180 + Vector2.Angle(Vector2.up, targetPos);
                else
                    temp = 180 - Vector2.Angle(Vector2.up, targetPos);

                Arrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, temp));

                //적당한 파워를 위해 20 나눔
                Power = Vector2.Distance(StartPos, Input.mousePosition) / 20.0f;
                Power = Mathf.Clamp(Power, LimitPower.x, LimitPower.y);

                Arrow.transform.localScale = new Vector3(Power / 100.0f, Power / 100.0f, 0.0f);

            }
            else
            {
                float X = (-(Input.mousePosition.x - StartPos.x) / 10) * Time.deltaTime * CameraMoveSpeed;
                float Y = (-(Input.mousePosition.y - StartPos.y) / 10) * Time.deltaTime * CameraMoveSpeed;

                Vector3 CameraPos = Camera.main.transform.position;
                CameraPos += new Vector3(X * 0.5f, Y * 0.5f);

                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraPos, CameraSmooth * Time.deltaTime);
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (IsHit)
            {
                EndPos = Input.mousePosition;
                Arrow.gameObject.SetActive(false);
                
               
                //플레이어 스크립트에서 보내기
                CurPlayer.GoForward(targetPos, Power);
                IsHit = false;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready, Shot, End
}

public class PlayManager : MonoBehaviour
{
    public InGameUI ingameUI;

    public bool IsHit = false;
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

    public Image CameraMovePanel;

    private void Awake()
    {
        //게임 세팅
        StageManager.instance.SetStage((int)StageManager.instance.CurStage.x, (int)StageManager.instance.CurStage.y);
    }

    GameState gameState = GameState.Ready;

    void Update()
    {
        GameLoop();
    }


    public void ChangeState(GameState s)
    {
        if (s == gameState) return;

        gameState = s;

        switch (s)
        {
            case GameState.Ready:
                // 오른쪽 UI클릭해서 말 터치하기 -> 
                break;

            case GameState.Shot:

                break;

            case GameState.End:
                break;
        }
    }

    public void GameLoop()
    {
        switch (gameState)
        {
            case GameState.Ready:
                break;

            case GameState.Shot:
                ShotLoop();
                break;

            case GameState.End:
                break;
        }
    }


   

    #region State.Shot

    public void ShotLoop()
    {
        if (Input.GetMouseButtonDown(0))
        {

            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            StartPos = Input.mousePosition;

            if (MyRayCast)
            {
                CameraMovePanel.raycastTarget = false;
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
                CameraMovePanel.raycastTarget = true;
            }
        }
    }

    #endregion

}

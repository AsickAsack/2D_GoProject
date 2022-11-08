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
    public static PlayManager Instance;

    public InGameUI ingameUI;
    public CharacterManager CharManager;

    public bool IsActive;
    public bool IsHit = false;
    public Transform BaseCamp;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 targetPos;
    RaycastHit2D MyRayCast;

    //�Ŀ� ����
    public float Power;
    public Vector2 LimitPower;

    //������Ʈ
    public CharacterPlay CurPlayer;
    public GameObject Arrow;



    private void Awake()
    {
        Instance = this;

        //���� ����
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
                ingameUI.SetTextPhase("Choice Phase");
                ingameUI.SetCharacterPopUP(true);
                //������ UIŬ���ؼ� �� ��ġ�ϱ� -> 
                break;

            case GameState.Shot:
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.SetCharacterPopUP(false);
                //ingameUI.inGameIcon[


                break;

            case GameState.End:
                //ingameUI.SetTextPhase("End Phase");
                //���� �ϰ� �Ѿ��
                break;
        }
    }

    public void GameLoop()
    {
        switch (gameState)
        {
            case GameState.Ready:
                Ready_Loop();
                break;

            case GameState.Shot:
                ShotLoop();
                break;

            case GameState.End:
                
                break;
        }
    }


   
    //���� ��
    public void Ready_Loop()
    {
        if (Input.GetMouseButtonDown(0))
        {

            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            StartPos = Input.mousePosition;

            if (MyRayCast)
            {
                ingameUI.CameraMovePanel.raycastTarget = false;
                Arrow.transform.position = CurPlayer.transform.position;
                Arrow.gameObject.SetActive(true);
                IsHit = true;
                //�߻� ������ ����
                ChangeState(GameState.Shot);
            }
        }

    }



    #region State.Shot

    public void ShotLoop()
    {

        if (Input.GetMouseButton(0))
        {
            MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));

            if (IsHit)
            {

                targetPos = -(MyRayCast.point - (Vector2)CurPlayer.transform.position).normalized;

                float temp = 0.0f;

                //������ ����, ����� ������
                if (Vector2.Dot(Vector2.right, targetPos) < 0.0f)
                    temp = 180 + Vector2.Angle(Vector2.up, targetPos);
                else
                    temp = 180 - Vector2.Angle(Vector2.up, targetPos);

                Arrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, temp));



                //������ �Ŀ��� ���� 20 ����
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


                //�÷��̾� ��ũ��Ʈ���� ������
                CurPlayer.GetComponent<PlayerBall>().GoForward(targetPos, Power);
                IsHit = false;
                ingameUI.CameraMovePanel.raycastTarget = true;
                //End ���·� ü����
                ChangeState(GameState.End);
            }
        }
    }

    #endregion

    public void ChangeCurPlayer(int index)
    {
        if (StageManager.instance.CurCharacters[index].OnBoard) return;

        if (CurPlayer != null)
            CurPlayer.gameObject.SetActive(false);

        CurPlayer = StageManager.instance.CurCharacters[index];
        CurPlayer.transform.position = BaseCamp.position;
        CurPlayer.gameObject.SetActive(true);

        if(IsActive)
        ingameUI.ChangeAcitveBtn();
    }

}

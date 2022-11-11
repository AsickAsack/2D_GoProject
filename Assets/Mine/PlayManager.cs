using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum GameState
{
    Ready, Shot, End
}

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public InGameUI ingameUI;
    public CharacterManager CharManager;
    public ObjectPool objectPool;

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
    public float DividePower;
    public float DivideArrowSize;


    //������Ʈ
    public CharacterPlay CurPlayer;
    GameObject CurPlayerIcon;
    public GameObject Arrow;
    Quaternion ArrowOriginAngle;

    //ȿ��

    //����
    public int EnemyCount;
    public int PlayerCount;

    private void Awake()
    {
        Instance = this;
        ArrowOriginAngle = Arrow.transform.rotation;
        //���� ����
        StageManager.instance.SetStage((int)StageManager.instance.CurStage.x, (int)StageManager.instance.CurStage.y);
        EnemyCount = StageManager.instance.stage[(int)StageManager.instance.CurStage.x-1].subStage[(int)StageManager.instance.CurStage.y-1].Object_Information.MyMonster.Length;
        PlayerCount = StageManager.instance.CurCharacters.Count;
    }

    public GameState gameState = GameState.Ready;

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
                CurPlayer.OnBoard = true;
                CurPlayer = null;
                ingameUI.SetTextPhase("Choice Phase");
                ingameUI.SetCharacterPopUP(true);
                //������ UIŬ���ؼ� �� ��ġ�ϱ� -> 
                break;

            case GameState.Shot:
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.SetCharacterPopUP(false);
                CurPlayerIcon.SetActive(false);


                break;
            case GameState.End:

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
                
                if(CurPlayer == null)
                {
                    Check_Exit();
                    ChangeState(GameState.Ready);
                    return;
                }

                if (CurPlayer.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                {
                    Check_Exit();
                    ChangeState(GameState.Ready);
                }
                break;
        }
    }

    public void Check_Exit()
    {
        if (EnemyCount == 0)
        {
            Debug.Log("�÷��̾� �¸� ��");
            //SceneLoader.Instance.Loading_LoadScene(0);
        }
        else if (PlayerCount == 0)
        {
            Debug.Log("���İ� �¸� ��");
        }
    }

    //���� ��
    public void Ready_Loop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Perspective �ƴҶ�
            //MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));
            
            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition),float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            StartPos = Input.mousePosition;

            if (MyRayCast && CurPlayer != null)
            {
                ingameUI.CameraMovePanel.raycastTarget = false;
                Arrow.transform.rotation = ArrowOriginAngle;
                Arrow.gameObject.SetActive(true);
                IsHit = true;

                //GameObject obj = Instantiate(effectManager.EffectPrefaps[0],CurPlayer.transform);
                //obj.transform.position = CurPlayer.transform.position;

               

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
            //MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));
            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));

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



                //������ �Ŀ��� ���� ����
                Power = Vector2.Distance(StartPos, Input.mousePosition) / DividePower;
                Power = Mathf.Clamp(Power, LimitPower.x, LimitPower.y);

               
                Arrow.transform.localScale = new Vector3(Power / DivideArrowSize, Power / DivideArrowSize, 0.0f);

            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (IsHit)
            {
                EndPos = Input.mousePosition;
                Arrow.gameObject.SetActive(false);


                //�÷��̾� ��ũ��Ʈ���� ������
                CurPlayer.GoForward(targetPos, Power);
                IsHit = false;
                ingameUI.CameraMovePanel.raycastTarget = true;
                PlayerCount--;
                //End ���·� ü����
                ChangeState(GameState.End);
            }
        }
    }

    #endregion

    //���� �� ������ ��
    public void ChangeCurPlayer(int index,GameObject Obj)
    {
        if (StageManager.instance.CurCharacters[index].OnBoard) return;

        if (CurPlayer != null)
            CurPlayer.gameObject.SetActive(false);

        CurPlayer = StageManager.instance.CurCharacters[index];
        CurPlayer.transform.position = BaseCamp.position;
        CurPlayer.gameObject.SetActive(true);


        CurPlayerIcon = Obj;
        //��Ƽ�갡 ���� �ִٸ�
        if (IsActive)
            ingameUI.ChangeAcitveBtn();
        
    }
}

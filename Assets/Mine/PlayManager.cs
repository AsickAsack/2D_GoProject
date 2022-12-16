using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

    ////////////////////
    //  ������ ����   //
    ////////////////////
public interface ISubject
{
    public void RegisterObserver(GameObject O);
    public void RemoveObserver(GameObject O);
    public void NotifyEventToObservers(Skill_Condition Skill_Condition, Transform tr);
    public void NotifyGameStateToObservers(GameState state);
}

public interface IObserver
{
    public void ListenToEvent(Skill_Condition Skill_Condition,Transform tr);
    public void ListenToGameState(GameState state);
}


// ���� ����Ǵ� ������ ���� ������
public enum GameState
{
    None,
    Ready, //���� ���� �� �� ���� �ܰ�
    Shot, //�� �߻縦 ���� �巡�� �ϴ� �ܰ�
    Move,// ���� �����̰� �ִ� �ܰ�
    End, // ������ ���� ���� ��� �ܰ�
    UserSkillSelect, // ���� ��ų ���� �ܰ�
    UserSKill
}

public class PlayManager : MonoBehaviour, ISubject
{
    public static PlayManager Instance;

    public InGameUI ingameUI;
    public ObjectPool objectPool;


    //public bool IsActive;

    public bool IsHit = false;
    public BaseCamp BaseCamp;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 targetPos;
    RaycastHit2D MyRayCast;

    //�Ŀ� ����
    public float Power;
    public Vector2 LimitPower;
    public float DividePower;
    public float MultiplyPower = 1.0f;


    //������Ʈ
    public CharacterPlay CurPlayer;
    GameObject CurPlayerIcon;
    public GameObject[] CharacterIcons;
    public InGameArrow Arrow;
    public MeterScript PowerOBJ;
    Vector2 PowerOBJPos;
    Quaternion ArrowOriginAngle;
    public UserSkill UserSkillObj;



    //����

    public int _EnemyCount;
    public int EnemyCount
    {
        get => _EnemyCount;
        set {

            _EnemyCount = value;

            if(MaxMonsterCount < _EnemyCount)
            {
                MaxMonsterCount = _EnemyCount;
            }

            if (EnemyCount == 0)
            {
                StartCoroutine(SetResult(ingameUI.SetResultCanavs,true));
            }
        }
    }
    public int PlayerCount;
    public int MaxMonsterCount;
    public int UserSkillPoint = 0;

    public int CurTurn = 0;
    //���Ͽ� �󸶳� ��Ҵ��� �� �� �ִ� ��Ƽų
    int _CurMultiKill;
    public int CurMultiKill
    {
        get => _CurMultiKill;
        set
        {
            _CurMultiKill = value;
            if (CurMultiKill != 0)
            {
                ingameUI.SetKillUi(_CurMultiKill);

                if (CurMultiKill > MultiKill)
                {
                    MultiKill = CurMultiKill;
                    //ingameUI.SetMostKillUI(MultiKill);

                    if (MultiKill > 1)
                        ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.MostKill), $"�ִ� ų {MultiKill} ų �޼�! ");
                }

                if (!KillStreakCheck)
                {
                    KillStreaks++;
                    KillStreakCheck = true;
                }
            }

        }
    }
    public int MultiKill;

    public bool KillStreakCheck = false;
    public int _KillStreaks;
    public int KillStreaks
    {
        get => _KillStreaks;
        set
        {
            _KillStreaks = value;
            if (_KillStreaks != 0)
            {
                //ų ��Ʈ�� �˸�
                ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.Killstreak), $"{_KillStreaks} Combo!");
            }
        }
    }



    public List<GameObject> OnBoardPlayer = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        ArrowOriginAngle = Arrow.transform.rotation;
        //���� ����
        StageManager.instance.SetStage((int)StageManager.instance.CurStage.x, (int)StageManager.instance.CurStage.y);
        EnemyCount = MaxMonsterCount =  StageManager.instance.stage[(int)StageManager.instance.CurStage.x - 1].subStage[(int)StageManager.instance.CurStage.y - 1].Object_Information.MyMonster.Length;
        PlayerCount = StageManager.instance.CurCharacters.Count;
        PowerOBJPos = Camera.main.ScreenToWorldPoint(PowerOBJ.transform.position);
    }

  

    public GameState gameState = GameState.Ready;

    void Update()
    {
        GameLoop();
    }

    public void Init()
    {
        ingameUI.SetTextPhase(CurTurn + "��! Choice Phase");
    }

    public void ChangeState(GameState s)
    {
        if (s == gameState) return;

        gameState = s;

        switch (s)
        {
            case GameState.Ready:

                CurTurn++;
                ingameUI.UserSKillBtn.SetActive(true);

                if (CurPlayer != null)
                    CurPlayer.ChangeONBorad();

                CurPlayer = null;

                ingameUI.SetTextPhase(CurTurn + "��! Choice Phase");
                ingameUI.SetCharacterPopUP(true);


                break;

            case GameState.UserSkillSelect:
                //cur�÷��̾� ����, ĳ���� ����â �ݱ�, ���� ui

                ingameUI.UserSkillBox.SetActive(true);
                ingameUI.SetCharacterPopUP(false);
                if (CurPlayer != null)
                {
                    CurPlayer.gameObject.SetActive(false);
                    CurPlayer = null;
                }
                break;

            case GameState.UserSKill:
                ingameUI.UserSkillBox.SetActive(false);
                break;

            case GameState.Shot:

                Arrow.InitArrow((int)LimitPower.x,(int)LimitPower.y);
                ingameUI.UserSKillBtn.SetActive(false);
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.SetCharacterPopUP(false);
                ingameUI.CharacterPopup.SetPanelSize(false);
                CurPlayerIcon.SetActive(false);

                break;

            case GameState.Move:

                break;

            case GameState.End:
                {
                    CheckEnd();
                }
                
                break;
        }

        Check_SkillExist(s);
        NotifyGameStateToObservers(s);
    }

    public void CheckEnd()
    {
        if (EnemyCount == 0)
        {
            StartCoroutine(SetResult(ingameUI.SetResultCanavs, true));
        }
        else if (PlayerCount == 0)
        {
            StartCoroutine(SetResult(ingameUI.SetResultCanavs, false));
        }
        else
        {
            this.BaseCamp.ClearBase();
            ResetToggle();
            CountRoutine();
            Check_SkillExist(GameState.End);
            NotifyGameStateToObservers(GameState.End);
            ChangeState(GameState.Ready);
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

            case GameState.Move:
                MoveLoop();
                break;

            case GameState.End:
                break;

            case GameState.UserSkillSelect:
                UserSkillSelectLoop();
                break;

            case GameState.UserSKill:
                UserSkillLoop();
                break;
        }
    }

    public void CountRoutine()
    {

        if (!KillStreakCheck && KillStreaks != 0)
        {
            ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.Killstreak_Fail), "Combo �ʱ�ȭ");
            KillStreaks = 0;
        }
        else
            KillStreakCheck = false;

        CurMultiKill = 0;
    }
    public void MoveLoop()
    {
        if (CheckMove())
            ChangeState(GameState.End);
    }

  

    //�ߵ� �� ��Ų�� �ִ��� Ȯ��
    public void Check_SkillExist(GameState gameState)
    {
        if (CurPlayer == null) return;

        CurPlayer.MySkill.CheckSKill(gameState);
    }

    //���� ��
    public void Ready_Loop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Perspective �ƴҶ�
            //MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            StartPos = Input.mousePosition;

            if (MyRayCast && CurPlayer != null)
            {
                ingameUI.CameraMovePanel.raycastTarget = false;
                PowerOBJ.gameObject.SetActive(true);
                Arrow.transform.rotation = ArrowOriginAngle;
                Arrow.gameObject.SetActive(true);
                IsHit = true;
                LimitPower.y = CurPlayer.character.Max_Power * MultiplyPower;
                PowerOBJ.slider.maxValue = LimitPower.y;

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
            //�۽���Ƽ�� �ƴҶ�
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

                Arrow.MyArrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, temp));


                PowerOBJ.transform.position = Camera.main.WorldToScreenPoint(PowerOBJPos);


                //������ �Ŀ��� ���� ����
                Power = Vector2.Distance(StartPos, Input.mousePosition) / DividePower;
                Power = Mathf.Clamp(Power, LimitPower.x, LimitPower.y);

                Arrow.SetArrow((int)Power);
                PowerOBJ.SetHealth(Power);

                //Arrow.transform.localScale = new Vector3(Power / DivideArrowSize, Power / DivideArrowSize, 0.0f);

            }
        }


        if (Input.GetMouseButtonUp(0))
        {

            if (IsHit)
            {
                SoundManager.Instance.PlayEffect(8);
                EndPos = Input.mousePosition;
                Arrow.EndArrow();
                PowerOBJ.gameObject.SetActive(false);

                //�÷��̾� ��ũ��Ʈ���� ������
                CurPlayer.MySkill.GoForward(targetPos, Power, null);
                IsHit = false;
                ingameUI.CameraMovePanel.raycastTarget = true;
                //Arrow.transform.localScale = new Vector3(LimitPower.x / DivideArrowSize, LimitPower.x / DivideArrowSize, 0.0f);
                PlayerCount--;
                //End ���·� ü����
                ChangeState(GameState.Move);
            }
        }
    }

    #endregion

    //���� �� ������ ��
    public void ChangeCurPlayer(int index,GameObject Obj)
    {
        if (StageManager.instance.CurCharacters[index].OnBoard) return;

        if (CurPlayer != null)
        {
            CurPlayer.gameObject.SetActive(false);
            CurPlayer.MySkill.ChangeRoutine();
        }

        PlayManager.Instance.ingameUI.InfoIcon.SetActive(true);

        CurPlayer = StageManager.instance.CurCharacters[index];
        CurPlayer.transform.position = BaseCamp.transform.position;
        CurPlayer.gameObject.SetActive(true);


        Check_SkillExist(GameState.Ready);
        CurPlayerIcon = Obj;

        /* ������ ��Ƽ�갡 ���ý����� �ٲ�ٸ� ����� �Լ�
         
        //��Ƽ�갡 ���� �ִٸ�
        if (IsActive)
            ingameUI.ChangeAcitveBtn();
        */
    }

    public void UserSkillSelectLoop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, 1 << LayerMask.NameToLayer("Board"));

            if (MyRayCast)
            {
                PlayerDB.Instance.myUserSkill.Skill(MyRayCast.point);
                ingameUI.SetUserSkillPoint(UserSkillPoint);
                ingameUI.UserSKillBtn.SetActive(false);
                ChangeState(GameState.UserSKill);
            }
        }
    }

    public void UserSkillLoop()
    {
        if(CheckMove())
        {
            gameState = GameState.Ready;
            ingameUI.SetTextPhase(CurTurn + "��! Choice Phase");
            ingameUI.UserSKillBtn.SetActive(true);
            ingameUI.SetCharacterPopUP(true);
            if(UserSkillObj != null)
            {
                UserSkillObj.End_Skill();
            }
        }
    }

    public void RegisterObserver(GameObject O)
    {
        OnBoardPlayer.Add(O);
    }

    public void RemoveObserver(GameObject O)
    {
        OnBoardPlayer.Remove(O);
        
    }

    public void NotifyEventToObservers(Skill_Condition Skill_Condition,Transform tr)
    {
        for (int i = 0; i < OnBoardPlayer.Count; i++)
        {
            OnBoardPlayer[i].GetComponent<IObserver>()?.ListenToEvent(Skill_Condition, tr);
        }
    }

    //ȯ�� ��ƾ
    public void ReBirthRoutine(Transform tr)
    {
        CharacterPlay character = tr.GetComponent<CharacterPlay>();
        if (character == CurPlayer) CurPlayer = null;

        ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.ReBirth), $"{character.character.Name} ȯ��!");
        int index = StageManager.instance.CurCharacters.FindIndex(x => x.character == character.character);
        PlayerCount++;
        character.OnBoard = false;
        objectPool.GetEffect(7, tr.position, Quaternion.identity);
        ingameUI.CharacterPopup.SetPanelSize(true);
        CharacterIcons[index].SetActive(true);
    }

    //�ö���ִ� ���鿡�� ���� ���� �˷��ֱ�
    public void NotifyGameStateToObservers(GameState State)
    {
        for (int i = 0; i < OnBoardPlayer.Count; i++)
        {
            OnBoardPlayer[i].GetComponent<IObserver>()?.ListenToGameState(State);
        }
    }

    IEnumerator SetResult(UnityAction<bool> unityAction,bool IsClear)
    {
        yield return new WaitForSeconds(2.0f);
        unityAction(IsClear);
    }

    public bool CheckMove()
    {
        
        if(CurPlayer == null || CurPlayer.GetIsStop())
        {
            for(int i=0;i < StageManager.instance.CurMonsters.Count;i++)
            {
                if (!StageManager.instance.CurMonsters[i].GetComponent<IMoveCheck>().GetIsStop())
                    return false;
            }

            for (int i = 0; i < OnBoardPlayer.Count; i++)
            {
                if (!OnBoardPlayer[i].GetComponent<IMoveCheck>().GetIsStop())
                    return false;
            }

            return true;
        }

        return false;
        
    }

    public void ResetToggle()
    {
        for(int i=0;i< StageManager.instance.CurToggle.Count;i++)
        {
            StageManager.instance.CurToggle[i].ResetToggle();
        }

    }
}

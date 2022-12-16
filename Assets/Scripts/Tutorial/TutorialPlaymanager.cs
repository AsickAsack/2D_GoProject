using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialPlaymanager : MonoBehaviour, ISubject
{
    public static TutorialPlaymanager Instance;

    public InGameUI ingameUI;
    public ObjectPool objectPool;


    //public bool IsActive;

    public bool IsHit = false;
    public TutorialBaseCamp BaseCamp;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 targetPos;
    RaycastHit2D MyRayCast;

    //파워 관련
    public float Power;
    public Vector2 LimitPower;
    public float DividePower;
    public float MultiplyPower = 1.0f;


    //오브젝트
    public CharacterPlay CurPlayer;
    GameObject CurPlayerIcon;
    public GameObject[] CharacterIcons;
    public InGameArrow Arrow;
    public MeterScript PowerOBJ;
    Vector2 PowerOBJPos;
    Quaternion ArrowOriginAngle;
    public UserSkill UserSkillObj;



    //판정
    public int _EnemyCount;
    public int EnemyCount
    {
        get => _EnemyCount;
        set
        {

            _EnemyCount = value;

            if (MaxMonsterCount < _EnemyCount)
            {
                MaxMonsterCount = _EnemyCount;
            }

            if (EnemyCount == 0)
            {
                
            }
        }
    }

    public int PlayerCount;
    public int MaxMonsterCount;
    public int UserSkillPoint = 0;

    public int CurTurn = 0;
    //한턴에 얼마나 잡았는지 알 수 있는 멀티킬
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
                        ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.MostKill), $"최다 킬 {MultiKill} 킬 달성! ");
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
                //킬 스트릭 알림
                ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.Killstreak), $"{_KillStreaks} Combo!");
            }
        }
    }

    string FailString;

    public List<GameObject> OnBoardPlayer = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        ArrowOriginAngle = Arrow.transform.rotation;
        //게임 세팅
        StageManager.instance.SetTutorialStage();
        PowerOBJPos = Camera.main.ScreenToWorldPoint(PowerOBJ.transform.position);
        TutorialManager.instance.TutorialClickAction[0] += SetTutorialCurPlayer;
        TutorialManager.instance.TutorialAction[1] += SetPlayTutorial;
    }


    public GameState gameState = GameState.Ready;

    void Update()
    {
        GameLoop();
    }

    public void Init()
    {
        ingameUI.SetTextPhase(CurTurn + "턴! Choice Phase");
    }

    public void ChangeState(GameState s)
    {
        if (s == gameState) return;

        gameState = s;

        switch (s)
        {
            case GameState.Ready:

                break;

            case GameState.UserSkillSelect:
                //cur플레이어 숨김, 캐릭터 선택창 닫기, 선택 ui
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

                Arrow.InitArrow((int)LimitPower.x, (int)LimitPower.y);
                ingameUI.UserSKillBtn.SetActive(false);
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.SetCharacterPopUP(false);
                ingameUI.CharacterPopup.SetPanelSize(false);

                break;

            case GameState.Move:

                break;

            case GameState.End:
                {
                    CheckEnd();
                }

                break;
        }
    }

    public void CheckEnd()
    {
        if(EnemyCount == 0)
        {
            //성공 ㅋㅋ
        }
        else
        {
            //실패
        }

        /*
            this.BaseCamp.ClearBase();
            CountRoutine();
            ChangeState(GameState.Ready);
        */
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
            ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.Killstreak_Fail), "Combo 초기화");
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




    //당기기 전
    public void Ready_Loop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Perspective 아닐때
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

                //발사 페이즈 진입
                ChangeState(GameState.Shot);
            }
        }

    }



    #region State.Shot

    public void ShotLoop()
    {

        if (Input.GetMouseButton(0))
        {
            //퍼스펙티브 아닐때
            //MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));
            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, 1 << LayerMask.NameToLayer("Raycaster"));

            if (IsHit)
            {


                targetPos = -(MyRayCast.point - (Vector2)CurPlayer.transform.position).normalized;

                float temp = 0.0f;

                //음수면 왼쪽, 양수면 오른쪽
                if (Vector2.Dot(Vector2.right, targetPos) < 0.0f)
                    temp = 180 + Vector2.Angle(Vector2.up, targetPos);
                else
                    temp = 180 - Vector2.Angle(Vector2.up, targetPos);

                Arrow.MyArrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, temp));


                PowerOBJ.transform.position = Camera.main.WorldToScreenPoint(PowerOBJPos);


                //적당한 파워를 위해 나눔
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

                //플레이어 스크립트에서 보내기
                CurPlayer.MySkill.GoForward(targetPos, Power, null);
                IsHit = false;
                ingameUI.CameraMovePanel.raycastTarget = true;
                //Arrow.transform.localScale = new Vector3(LimitPower.x / DivideArrowSize, LimitPower.x / DivideArrowSize, 0.0f);
                PlayerCount--;
                //End 상태로 체인지
                ChangeState(GameState.Move);
            }
        }
    }

    #endregion

    public void SetTutorialCurPlayer()
    {
        EnemyCount = 1;
        ChangeState(GameState.Ready);
        CurPlayer = StageManager.instance.CurCharacters[0];
        CurPlayer.transform.position = BaseCamp.transform.position;
        CurPlayer.gameObject.SetActive(true);

        ingameUI.SetCharacterPopUP(false);

        StageManager.instance.CurMonsters[0].transform.position = Vector2.zero;
        StageManager.instance.CurMonsters[0].gameObject.SetActive(true);
        TutorialManager.instance.StartDialogue();

        //StartCoroutine(DelayCoroutine(1.0f, TutorialManager.instance.StartDialogue));
    }

    public void SetPlayTutorial()
    {
        
    }

    //딜레이 시키는 코루틴
    IEnumerator DelayCoroutine(float time,UnityAction DelayAction)
    {
        yield return new WaitForSeconds(time);

        DelayAction();
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
        if (CheckMove())
        {
            gameState = GameState.Ready;
            ingameUI.SetTextPhase(CurTurn + "턴! Choice Phase");
            ingameUI.UserSKillBtn.SetActive(true);
            ingameUI.SetCharacterPopUP(true);
            if (UserSkillObj != null)
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

    public void NotifyEventToObservers(Skill_Condition Skill_Condition, Transform tr)
    {
        for (int i = 0; i < OnBoardPlayer.Count; i++)
        {
            OnBoardPlayer[i].GetComponent<IObserver>()?.ListenToEvent(Skill_Condition, tr);
        }
    }

    //환생 루틴
    public void ReBirthRoutine(Transform tr)
    {
        CharacterPlay character = tr.GetComponent<CharacterPlay>();
        if (character == CurPlayer) CurPlayer = null;

        ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.ReBirth), $"{character.character.Name} 환생!");
        int index = StageManager.instance.CurCharacters.FindIndex(x => x.character == character.character);
        PlayerCount++;
        character.OnBoard = false;
        objectPool.GetEffect(7, tr.position, Quaternion.identity);
        ingameUI.CharacterPopup.SetPanelSize(true);
        CharacterIcons[index].SetActive(true);
    }

    //올라와있는 말들에게 게임 상태 알려주기
    public void NotifyGameStateToObservers(GameState State)
    {
        for (int i = 0; i < OnBoardPlayer.Count; i++)
        {
            OnBoardPlayer[i].GetComponent<IObserver>()?.ListenToGameState(State);
        }
    }



    public bool CheckMove()
    {

        if (CurPlayer == null || CurPlayer.GetIsStop())
        {
            for (int i = 0; i < StageManager.instance.CurMonsters.Count; i++)
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

    ////////////////////
    //  옵저버 패턴   //
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


// 현재 진행되는 게임의 상태 열거자
public enum GameState
{
    None,
    Ready, //게임 시작 후 알 선택 단계
    Shot, //알 발사를 위해 드래그 하는 단계
    Move,// 알이 움직이고 있는 단계
    End, // 끝나고 종료 조건 계산 단계
    UserSkillSelect, // 유저 스킬 선택 단계
    UserSKill
}

public class PlayManager : MonoBehaviour,ISubject
{
    public static PlayManager Instance;

    public InGameUI ingameUI;
    public CharacterManager CharManager;
    public ObjectPool objectPool;
   

    //public bool IsActive;
    
    public bool IsHit = false;
    public Transform BaseCamp;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 targetPos;
    RaycastHit2D MyRayCast;

    //파워 관련
    public float Power;
    public Vector2 LimitPower;
    public float DividePower;
    public float DivideArrowSize;
    public float MultiplyPower = 1.0f;


    //오브젝트
    public CharacterPlay CurPlayer;
    GameObject CurPlayerIcon;
    public GameObject[] CharacterIcons;
    public GameObject Arrow;
    Quaternion ArrowOriginAngle;
    public UserSkill UserSkillObj;

    //효과
    public Sprite Kill_Sprite;

    //판정

    public int _EnemyCount;
    public int EnemyCount
    {
        get => _EnemyCount;
        set {
            _EnemyCount = value;
            if(EnemyCount == 0)
            {
                StartCoroutine(SetResult(ingameUI.SetResultCanavs,"승리!"));
                //ingameUI.SetResultCanavs("승리!");
            }
        }
    }
    public int PlayerCount;
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
                UserSkillPoint++;
                ingameUI.SetUserSkillPoint(UserSkillPoint);

                if (CurMultiKill > MultiKill)
                {
                    MultiKill = CurMultiKill;
                    ingameUI.SetMostKillUI(MultiKill);

                    if(MultiKill > 1)
                    ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.MostKill), $"최다 킬 {MultiKill} 킬 달성! ");
                }

                if(!KillStreakCheck)
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
            if(_KillStreaks != 0)
            {
                //킬 스트릭 알림
                ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.Killstreak), $"{_KillStreaks} Combo!");
            }
        }
    }



    public List<GameObject> OnBoardPlayer = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        ArrowOriginAngle = Arrow.transform.rotation;
        //게임 세팅
        StageManager.instance.SetStage((int)StageManager.instance.CurStage.x, (int)StageManager.instance.CurStage.y);
        EnemyCount = StageManager.instance.stage[(int)StageManager.instance.CurStage.x-1].subStage[(int)StageManager.instance.CurStage.y-1].Object_Information.MyMonster.Length;
        PlayerCount = StageManager.instance.CurCharacters.Count;
    }

    private void Start()
    {
        //ChangeState(GameState.Ready);
        
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

                CurTurn++;
                ingameUI.UserSKillBtn.SetActive(true);

                if(CurPlayer != null)
                CurPlayer.ChangeONBorad();

                CurPlayer = null;

                ingameUI.SetTextPhase(CurTurn +"턴! Choice Phase");
                ingameUI.SetCharacterPopUP(true);
                

                break;

            case GameState.UserSkillSelect:
                //cur플레이어 숨김, 캐릭터 선택창 닫기, 선택 ui

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
                ingameUI.UserSKillBtn.SetActive(false);
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.SetCharacterPopUP(false);
                CurPlayerIcon.SetActive(false);

                break;

            case GameState.Move:
                
                break;

            case GameState.End:
                
                break;
        }

        Check_SkillExist(s);
        NotifyGameStateToObservers(s);
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
                Check_MoveStop();
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
        if(CheckMove())
            ChangeState(GameState.End);
    }

    public void Check_MoveStop()
    {
        if (EnemyCount == 0)
        {
            ingameUI.SetResultCanavs("승리!");
        }
        else if (PlayerCount == 0)
        {
            ingameUI.SetResultCanavs("패배ㅋ");
        }
        else
        {
            CountRoutine();
            ChangeState(GameState.Ready);
        }
    }

    //발동 할 스킨이 있는지 확인
    public void Check_SkillExist(GameState gameState)
    {
        if (CurPlayer == null) return;

        CurPlayer.MySkill.CheckSKill(gameState);
    }

    //당기기 전
    public void Ready_Loop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Perspective 아닐때
            //MyRayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));
            
            MyRayCast = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition),float.MaxValue, 1 << LayerMask.NameToLayer("BaseCamp"));

            StartPos = Input.mousePosition;

            if (MyRayCast && CurPlayer != null)
            {
                ingameUI.CameraMovePanel.raycastTarget = false;
                Arrow.transform.rotation = ArrowOriginAngle;
                Arrow.gameObject.SetActive(true);
                IsHit = true;
                LimitPower.y = CurPlayer.character.Max_Power * MultiplyPower;

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

                Arrow.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, temp));



                //적당한 파워를 위해 나눔
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


                //플레이어 스크립트에서 보내기
                CurPlayer.MySkill.GoForward(targetPos, Power,null);
                IsHit = false;
                ingameUI.CameraMovePanel.raycastTarget = true;
                Arrow.transform.localScale = new Vector3(LimitPower.x / DivideArrowSize, LimitPower.x / DivideArrowSize, 0.0f);
                PlayerCount--;
                //End 상태로 체인지
                ChangeState(GameState.Move);
            }
        }
    }

    #endregion

    //옆에 돌 눌렀을 때
    public void ChangeCurPlayer(int index,GameObject Obj)
    {
        if (StageManager.instance.CurCharacters[index].MySkill.OnBoard) return;

        if (CurPlayer != null)
            CurPlayer.gameObject.SetActive(false);

        CurPlayer = StageManager.instance.CurCharacters[index];
        CurPlayer.transform.position = BaseCamp.position;
        CurPlayer.gameObject.SetActive(true);

        Check_SkillExist(GameState.Ready);

        CurPlayerIcon = Obj;

        /* 원래는 액티브가 선택식으로 바뀐다면 사용할 함수
         
        //액티브가 켜져 있다면
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
            ingameUI.SetTextPhase(CurTurn + "턴! Choice Phase");
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

    //환생 루틴
    public void ReBirthRoutine(Transform tr)
    {
        CharacterPlay character = tr.GetComponent<CharacterPlay>();
        if (character == CurPlayer) CurPlayer = null;

        ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.ReBirth), $"{character.name} 환생!");
        int index = StageManager.instance.CurCharacters.FindIndex(x => x.character == character.character);
        PlayerCount++;
        character.MySkill.OnBoard = false;
        objectPool.GetEffect(7, tr.position, Quaternion.identity);
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

    IEnumerator SetResult(UnityAction<string> unityAction,string s)
    {
        yield return new WaitForSeconds(2.0f);
        unityAction(s);
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
}

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

    public bool IsHit = false;
    public TutorialBaseCamp BaseCamp;
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

    public GameObject SlideFingerParent;
    public GameObject SlideFinger;
    public Animator SlideFingerAnim;



    //����
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

    public bool IsFail = false;
    string FailString;

    public List<GameObject> OnBoardPlayer = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        ArrowOriginAngle = Arrow.transform.rotation;
        //���� ����
        StageManager.instance.SetTutorialStage();
        PowerOBJPos = Camera.main.ScreenToWorldPoint(PowerOBJ.transform.position);
        TutorialManager.instance.TutorialClickAction[0] += SetTutorialCurPlayer;
        TutorialManager.instance.TutorialClickAction[1] += ActiveUserSkill;
        TutorialManager.instance.TutorialAction[1] += SetTutorialFirst;
        TutorialManager.instance.TutorialAction[3] += SetTutorialSecond;
        TutorialManager.instance.TutorialAction[6] += LootAtSkillPoint;
        TutorialManager.instance.TutorialAction[7] += FocusUserSkill;
        TutorialManager.instance.TutorialAction[8] += ExitTutorial;
    }
    public void ExitTutorial()
    {
        PlayerDB.Instance.playerdata._Gold = 25000;
        PlayerDB.Instance.playerdata.PlayFirst = false;
        PlayerDB.Instance.SaveData();
        StageManager.instance.InitStage(1);
    }

    public void SetTutorialMonster(int index, float x, float y)
    {
        StageManager.instance.CurMonsters[index].transform.position = new Vector2(x, y);
        StageManager.instance.CurMonsters[index].gameObject.SetActive(true);
    }

    public void FocusUserSkill()
    {
        SetTutorialMonster(0, -0.3f, -0.9f);
        SetTutorialMonster(1, 0.6f, -0.1f);
        SetTutorialMonster(2, -0.8f, 2.5f);
        SetTutorialMonster(3, 0.0f, 4.7f);

        ingameUI.UserSKillBtn.SetActive(true);
        TutorialManager.instance.SetFocusOBJ(ingameUI.UserSKillBtn.GetComponent<RectTransform>(), true);
    }

    public void ActiveUserSkill()
    {
        ChangeState(GameState.UserSkillSelect);
    }



    public GameState gameState = GameState.Ready;

    private void Start()
    {
        ChangeState(GameState.None);

    }

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
            case GameState.None:
                break;

            case GameState.Ready:

                break;

            case GameState.UserSkillSelect:
                //cur�÷��̾� ����, ĳ���� ����â �ݱ�, ���� ui
                if (CurPlayer != null)
                {
                    CurPlayer.gameObject.SetActive(false);
                    CurPlayer = null;
                }
                ingameUI.UserSkillBox.SetActive(true);
                //Vector2 FingerPos = Camera.main.WorldToScreenPoint(BaseCamp.transform.position);
                ActiveSliderFinger(BaseCamp.transform.position+(Vector3)Vector2.up*2.0f, 2);
                break;

            case GameState.UserSKill:
                ingameUI.UserSkillBox.SetActive(false);
                break;

            case GameState.Shot:

                Arrow.InitArrow((int)LimitPower.x, (int)LimitPower.y);
                ingameUI.UserSKillBtn.SetActive(false);
                ingameUI.SetTextPhase("Shot Phase");
                ingameUI.InfoIcon.SetActive(false);
                InActiveSliderFinger();

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

    public void DeathInTutorial(GameObject obj)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            Instantiate(GameDB.Instance.Tutorial_OBJ[1], obj.transform.position, Quaternion.identity);
        }
    }

    public void CheckEnd()
    {
        if (EnemyCount == 0)
        {
            IsFail = false;
            DeathInTutorial(CurPlayer.gameObject);
            TutorialManager.instance.StartDialogue();
        }
        else
        {
            IsFail = true;
            DeathInTutorial(CurPlayer.gameObject);
            for (int i = 0; i < StageManager.instance.CurMonsters.Count; i++)
            {
                if (StageManager.instance.CurMonsters[i].gameObject.activeSelf)
                {
                    DeathInTutorial(StageManager.instance.CurMonsters[i].gameObject);
                }
            }

            TutorialManager.instance.TutorialAction[--TutorialManager.instance.Action_Index]();
            //SetTutorialFirst();

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
        public void InActiveSliderFinger()
    {
        SlideFingerAnim.SetBool("IsMove", false);
        SlideFinger.SetActive(false);
    }

    public void ActiveSliderFinger(Vector2 pos,int AnimationIndex)
    {

        SlideFingerParent.transform.position = Camera.main.WorldToScreenPoint(pos);
        SlideFinger.SetActive(true);
        SlideFingerAnim.SetInteger("FingerNum", AnimationIndex);

    }

    public void SetTutorialCurPlayer()
    {

        CurPlayer = StageManager.instance.CurCharacters[0];
        CurPlayer.transform.position = BaseCamp.transform.position;
        CurPlayer.gameObject.SetActive(true);

        ingameUI.SetCharacterPopUP(false);

        TutorialManager.instance.StartDialogue();
    }

    public void SetTutorialSecond()
    {
        SetTutorialRoutine(1, () =>
        {
            CurPlayer = StageManager.instance.CurCharacters[1];
            CurPlayer.transform.position = BaseCamp.transform.position;
            CurPlayer.gameObject.SetActive(true);

            StageManager.instance.CurMonsters[0].transform.position = new Vector2(1.6f, -0.3f);
            StageManager.instance.CurMonsters[0].gameObject.SetActive(true);

            StageManager.instance.CurObstacle[^1].gameObject.SetActive(true);

            ActiveSliderFinger(BaseCamp.transform.position,1);
        });
    }

    public void SetTutorialFirst()
    {

        SetTutorialRoutine(1, () =>
        {
            CurPlayer = StageManager.instance.CurCharacters[0];
            CurPlayer.transform.position = BaseCamp.transform.position;
            CurPlayer.gameObject.SetActive(true);

            StageManager.instance.CurMonsters[0].transform.position = Vector2.zero;
            StageManager.instance.CurMonsters[0].gameObject.SetActive(true);

            ActiveSliderFinger(BaseCamp.transform.position, 0);
        });

    }

    public void SetTutorialRoutine(int EnemyCount,UnityAction MyAction)
    {
        this.EnemyCount = EnemyCount;
        ChangeState(GameState.Ready);

        MyAction();

        if (IsFail)
        {
            IsFail = false;
            ChangeState(GameState.None);
            TutorialManager.instance.FailRoutine();
        }
    }


    //������ ��Ű�� �ڷ�ƾ
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
                ingameUI.SetUserSkillPoint(0);
                //TutorialPlaymanager.Instance.UserSkillPoint -= PlayerDB.Instance.myUserSkill.SkillPoint;
                ingameUI.UserSKillBtn.SetActive(false);
                ingameUI.UserSkillBox.SetActive(false);
                InActiveSliderFinger();
                ChangeState(GameState.None);
                StartCoroutine(DelayCoroutine(1.5f, () => {
                    TutorialManager.instance.StartDialogue();
                }));
            }
        }
    }

   

    public void UserSkillLoop()
    {
        if (CheckMove())
        {
            
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

    public void LootAtSkillPoint()
    {
        StartCoroutine(LookSkillPointCo());
    }

    IEnumerator LookSkillPointCo()
    {
        ingameUI.UserPointSlider.gameObject.SetActive(true);
        ingameUI.SetUserSkillPoint(UserSkillPoint);

        TutorialManager.instance.SetFocusOBJ(ingameUI.UserPointSlider.GetComponent<RectTransform>(), false);

        yield return new WaitForSeconds(1.5f);
        TutorialManager.instance.ReturnTargetRect();

        TutorialManager.instance.StartDialogue();
    }

}

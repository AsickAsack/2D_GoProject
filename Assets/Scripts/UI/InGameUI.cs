using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public InGameIcon[] inGameIcon;
    public Image CameraMovePanel;


    [Header("[캐릭터 선택 팝업]")]

    public IngameIconPanel CharacterPopup;
    public Animator PopupAnim;


    [Header("[텍스트 기능]")]

    public TMPro.TMP_Text Phase_Text;
    Coroutine PTextCo;
    [SerializeField]
    Vector2 LimitTextSize;
    [SerializeField]
    float TextSpeed;

    [Header("[컷씬]")]

    //액티브 켜졌을때 버튼
    public GameObject ActiveOnBtn;
    //액티브 꺼졌을때 버튼
    public GameObject ActiveOffBtn;
    public GameObject CutSceneObj;
    public Image CutSceneImage;
    public Animator CutSceneAnim;
    public Animator PhaseAnim;

    [Header("[액티브 UI]")]

    public GameObject ActiveObj;

    [Header("[기본 UI]")]

    public TMPro.TMP_Text CurTurn_Text;

    [Header("[연속킬 & 킬스트릭 UI & 알림]")]

    public Animator Kill_UI_Animator;
    public Image CharacterIcon;
    public TMPro.TMP_Text Kill_Text;
    public TMPro.TMP_Text KillStreak_Text;
    public TMPro.TMP_Text MostKill_Text;

    public Queue<string> Notify = new Queue<string>();
    public Queue<Sprite> NotifySprite = new Queue<Sprite>();
    public TMPro.TMP_Text NotifyText;
    public GameObject NotifyOBJ;
    public Image NotifyImage;
    Coroutine CoNotify;

    [Header("[유저 스킬 UI]")]

    public Slider UserPointSlider;
    public GameObject UserSKillBtn;

    [Header("[팝업 UI]")]

    public Canvas PopUpCanvas;
    public TMPro.TMP_Text PopUptext;
    public GameObject UserSkillBox;

    [Header("[결과창 UI]")]

    public ResultCanvas resultCanvas;
    public TMPro.TMP_Text Result_Text;

    [Header("[캐릭터 정보 UI]")]

    public Canvas SkillDescriptionCanavs;
    public TMPro.TMP_Text SkillLabel;
    public TMPro.TMP_Text SkillDescriptionText;
    public GameObject InfoIcon;

    public void SetCharacterSkill_UI()
    {
        if(SkillDescriptionCanavs.enabled)
        {
            //이미 창이 켜져있을때
            SetCharacterSkill_UI(false);
        }
        else
        {
            if(PlayManager.Instance.CurPlayer != null)
            SetCharacterSkill_UI(true, PlayManager.Instance.CurPlayer.character);
        }
    }


    public void SetCharacterSkill_UI(bool check,Character character = null)
    {
        if (!check)
        {
            SkillDescriptionCanavs.enabled = false;
            return;
        }

        SkillDescriptionCanavs.enabled = true;
        SkillLabel.text = character.Skill_Type == 1 ? character.Name + "- 액티브 스킬" : character.Name + "- 패시브 스킬";
        SkillDescriptionText.text = GameDB.Instance.ChangeFigure(character, character.Skill_Des);
    }
    public void SetPopup(string s)
    {
        PopUpCanvas.enabled = true;
        PopUptext.text = s;
    }

    //알림 기능
    public void SetNotify(Sprite sprite,string s)
    {
        Notify.Enqueue(s);
        NotifySprite.Enqueue(sprite);
        
        if (CoNotify == null)
        {
            CoNotify = StartCoroutine(NotifyRoutine(1.0f));
        }
    }
    
    IEnumerator NotifyRoutine(float time)
    {
        while(Notify.Count > 0)
        {
            NotifyText.text = Notify.Dequeue();
            NotifyImage.sprite = NotifySprite.Dequeue();
            NotifyOBJ.SetActive(true);

            yield return new WaitForSeconds(time);
            NotifyOBJ.SetActive(false);
           
        }

        CoNotify = null;
    }

    //결과 창 띄우는 함수
    public void SetResultCanavs(bool IsClear)
    {
        resultCanvas.CheckGameResult(IsClear);
    }

    //유저 스킬 버튼 함수
    public void GoUserSkill()
    {
        if (PlayManager.Instance.gameState != GameState.UserSkillSelect)
        {
            if (PlayManager.Instance.UserSkillPoint >= PlayerDB.Instance.myUserSkill.SkillPoint)
            {
                PlayManager.Instance.ChangeState(GameState.UserSkillSelect);
            } 
            else
            {
                SetPopup("포인트가 부족합니다!");
            }
        }
        else
        {
            PlayManager.Instance.gameState = GameState.Ready;
            
            SetTextPhase(PlayManager.Instance.CurTurn + "턴! Choice Phase");
            SetCharacterPopUP(true);
        }

    }

    
    //텍스트 수정 함수
    public void SetTextUI(TMPro.TMP_Text Text,int index,string AnotherMessage)
    {
        Text.gameObject.SetActive(true);
        Text.text = index.ToString() + AnotherMessage;
    }

    //최대 킬 UI 설정 함수
    public void SetMostKillUI(int index)
    {
        SetTextUI(MostKill_Text, index, "");
    }


    //스킬 포인트 세팅 함수
    public void SetUserSkillPoint(int index)
    {
        UserPointSlider.value = index;
    }

    //킬 UI 설정 함수
    public void SetKillUi(int index)
    {
        Kill_UI_Animator.ResetTrigger("MoveUI");
        SetTextUI(Kill_Text, index, " Kill");

        if(!Kill_UI_Animator.GetBool("IsActive"))
            Kill_UI_Animator.SetTrigger("MoveUI");

    }

    #region 액티브 함수

    //액티브 재생 함수(액티브 기능이 없어져서 사용X)
    public void OnActive()
    {
        StartCoroutine(ActiveCo());
    }

    //액티브 재생 코루틴(액티브 기능이 없어져서 사용X)
    IEnumerator ActiveCo()
    {
        ActiveObj.SetActive(true);
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.25f);

        Time.timeScale = 1;
        ActiveObj.SetActive(false);
    }

    #endregion


    //컷씬 나오는함수(액티브 사용이 없어져서 바꿔야함)
    public void CutScene(int index)
    {
        if (PlayManager.Instance.CurPlayer == null || PlayManager.Instance.gameState != GameState.Ready) return;

        //액티브 킬때
        if(index == 1 && PlayerDB.Instance.CutScene)
        {
            CutSceneImage.sprite = GameDB.Instance.GetCharacterImage(PlayManager.Instance.CurPlayer.character);
            CutSceneAnim.SetTrigger("CutOn");
        }

        ChangeAcitveBtn();
    }


    public void ChangeAcitveBtn()
    {
        ActiveOnBtn.SetActive(!ActiveOnBtn.activeSelf);
        ActiveOffBtn.SetActive(!ActiveOffBtn.activeSelf);
        //PlayManager.Instance.IsActive = ActiveOnBtn.activeSelf;
        Debug.Log(ActiveOnBtn.activeSelf);
    }



    #region 텍스트기능

    //페이즈 알려주는 함수
    public void SetTextPhase(string Text)
    {

        Phase_Text.text = Text;
        PhaseAnim.SetTrigger("PhaseChange");

    }


    #endregion


    #region 캐릭터 선택 팝업

    //인게임 캐릭터 선택창 세팅 함수
    public void SetCharacterPopUP(bool Check)
    {
        InfoIcon.SetActive(Check);

        if (Check)
            PopupAnim.SetTrigger("GoLeft");
        else
            PopupAnim.SetTrigger("GoRight");

            
    }

    #endregion


}

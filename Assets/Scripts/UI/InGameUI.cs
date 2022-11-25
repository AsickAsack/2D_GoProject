using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public InGameIcon[] inGameIcon;
    public Image CameraMovePanel;


    [Header("[캐릭터 선택 팝업]")]

    public GameObject CharacterPopup;
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

    [Header("[연속킬 & 킬스트릭 UI]")]

    public Animator Kill_UI;
    public Image CharacterIcon;
    public TMPro.TMP_Text Kill_Text;
    public TMPro.TMP_Text KillStreak_Text;


    [Header("[유저 스킬 UI]")]

    public Slider UserPointSlider;
    public GameObject UserSKillBtn;

    [Header("[결과창 UI]")]

    public Canvas ResultCanvas;
    public TMPro.TMP_Text Result_Text;

    //결과 창 띄우는 함수
    public void SetResultCanavs(string result)
    {
        ResultCanvas.enabled = true;
        Result_Text.text = result;
    }

    //유저 스킬 버튼 함수
    public void GoUserSkill()
    {
        if (PlayManager.Instance.gameState != GameState.UserSkill)
        {
            if (PlayManager.Instance.UserSkillPoint >= PlayerDB.Instance.myUserSkill.SkillPoint)
            {
                PlayManager.Instance.ChangeState(GameState.UserSkill);
            }else
            {
                Debug.Log("포인트 부족ㅋㅋ");
            }
        }
        else
        {
            PlayManager.Instance.gameState = GameState.Ready;
        }
        
    }

    //스킬 포인트 세팅 함수
    public void SetUserSkillPoint(int index)
    {
        UserPointSlider.value = index;
    }

    //킬 UI 설정 함수
    public void SetKillUi(int index)
    {
        Kill_UI.ResetTrigger("MoveUI");
        Kill_UI.gameObject.SetActive(true);
        Kill_Text.text = index.ToString() + " Kill";
        Kill_UI.SetTrigger("MoveUI");

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
        if(Check)
            PopupAnim.SetTrigger("GoLeft");
        else
            PopupAnim.SetTrigger("GoRight");
    }

    #endregion


}

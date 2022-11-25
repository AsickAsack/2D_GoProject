using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public InGameIcon[] inGameIcon;
    public Image CameraMovePanel;


    [Header("[ĳ���� ���� �˾�]")]

    public GameObject CharacterPopup;
    public Animator PopupAnim;


    [Header("[�ؽ�Ʈ ���]")]

    public TMPro.TMP_Text Phase_Text;
    Coroutine PTextCo;
    [SerializeField]
    Vector2 LimitTextSize;
    [SerializeField]
    float TextSpeed;

    [Header("[�ƾ�]")]

    //��Ƽ�� �������� ��ư
    public GameObject ActiveOnBtn;
    //��Ƽ�� �������� ��ư
    public GameObject ActiveOffBtn;
    public GameObject CutSceneObj;
    public Image CutSceneImage;
    public Animator CutSceneAnim;
    public Animator PhaseAnim;

    [Header("[��Ƽ�� UI]")]

    public GameObject ActiveObj;

    [Header("[�⺻ UI]")]

    public TMPro.TMP_Text CurTurn_Text;

    [Header("[����ų & ų��Ʈ�� UI]")]

    public Animator Kill_UI;
    public Image CharacterIcon;
    public TMPro.TMP_Text Kill_Text;
    public TMPro.TMP_Text KillStreak_Text;


    [Header("[���� ��ų UI]")]

    public Slider UserPointSlider;
    public GameObject UserSKillBtn;

    [Header("[���â UI]")]

    public Canvas ResultCanvas;
    public TMPro.TMP_Text Result_Text;

    //��� â ���� �Լ�
    public void SetResultCanavs(string result)
    {
        ResultCanvas.enabled = true;
        Result_Text.text = result;
    }

    //���� ��ų ��ư �Լ�
    public void GoUserSkill()
    {
        if (PlayManager.Instance.gameState != GameState.UserSkill)
        {
            if (PlayManager.Instance.UserSkillPoint >= PlayerDB.Instance.myUserSkill.SkillPoint)
            {
                PlayManager.Instance.ChangeState(GameState.UserSkill);
            }else
            {
                Debug.Log("����Ʈ ��������");
            }
        }
        else
        {
            PlayManager.Instance.gameState = GameState.Ready;
        }
        
    }

    //��ų ����Ʈ ���� �Լ�
    public void SetUserSkillPoint(int index)
    {
        UserPointSlider.value = index;
    }

    //ų UI ���� �Լ�
    public void SetKillUi(int index)
    {
        Kill_UI.ResetTrigger("MoveUI");
        Kill_UI.gameObject.SetActive(true);
        Kill_Text.text = index.ToString() + " Kill";
        Kill_UI.SetTrigger("MoveUI");

    }

    #region ��Ƽ�� �Լ�

    //��Ƽ�� ��� �Լ�(��Ƽ�� ����� �������� ���X)
    public void OnActive()
    {
        StartCoroutine(ActiveCo());
    }

    //��Ƽ�� ��� �ڷ�ƾ(��Ƽ�� ����� �������� ���X)
    IEnumerator ActiveCo()
    {
        ActiveObj.SetActive(true);
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.25f);

        Time.timeScale = 1;
        ActiveObj.SetActive(false);
    }

    #endregion


    //�ƾ� �������Լ�(��Ƽ�� ����� �������� �ٲ����)
    public void CutScene(int index)
    {
        if (PlayManager.Instance.CurPlayer == null || PlayManager.Instance.gameState != GameState.Ready) return;

        //��Ƽ�� ų��
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



    #region �ؽ�Ʈ���

    //������ �˷��ִ� �Լ�
    public void SetTextPhase(string Text)
    {

        Phase_Text.text = Text;
        PhaseAnim.SetTrigger("PhaseChange");

    }


    #endregion


    #region ĳ���� ���� �˾�

    //�ΰ��� ĳ���� ����â ���� �Լ�
    public void SetCharacterPopUP(bool Check)
    {
        if(Check)
            PopupAnim.SetTrigger("GoLeft");
        else
            PopupAnim.SetTrigger("GoRight");
    }

    #endregion


}

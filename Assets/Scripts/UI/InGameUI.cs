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

    [Header("[����ų & ų��Ʈ�� UI & �˸�]")]

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

    [Header("[���� ��ų UI]")]

    public Slider UserPointSlider;
    public GameObject UserSKillBtn;

    [Header("[�˾� UI]")]

    public Canvas PopUpCanvas;
    public TMPro.TMP_Text PopUptext;
    public GameObject UserSkillBox;

    [Header("[���â UI]")]

    public Canvas ResultCanvas;
    public TMPro.TMP_Text Result_Text;

    public void SetPopup(string s)
    {
        PopUpCanvas.enabled = true;
        PopUptext.text = s;
    }

    //�˸� ���
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

    //��� â ���� �Լ�
    public void SetResultCanavs(string result)
    {
        ResultCanvas.enabled = true;
        Result_Text.text = result;
    }

    //���� ��ų ��ư �Լ�
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
                SetPopup("����Ʈ�� �����մϴ�!");
            }
        }
        else
        {
            PlayManager.Instance.gameState = GameState.Ready;
            
            SetTextPhase(PlayManager.Instance.CurTurn + "��! Choice Phase");
            SetCharacterPopUP(true);
        }

    }

    
    //�ؽ�Ʈ ���� �Լ�
    public void SetTextUI(TMPro.TMP_Text Text,int index,string AnotherMessage)
    {
        Text.gameObject.SetActive(true);
        Text.text = index.ToString() + AnotherMessage;
    }

    //�ִ� ų UI ���� �Լ�
    public void SetMostKillUI(int index)
    {
        SetTextUI(MostKill_Text, index, "");
    }


    //��ų ����Ʈ ���� �Լ�
    public void SetUserSkillPoint(int index)
    {
        UserPointSlider.value = index;
    }

    //ų UI ���� �Լ�
    public void SetKillUi(int index)
    {
        Kill_UI_Animator.ResetTrigger("MoveUI");
        SetTextUI(Kill_Text, index, " Kill");

        if(!Kill_UI_Animator.GetBool("IsActive"))
            Kill_UI_Animator.SetTrigger("MoveUI");

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

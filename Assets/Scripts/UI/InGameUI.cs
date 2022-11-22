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

    [Header("[��Ƽ�� UI]")]

    public Animator Kill_UI;
    public Image CharacterIcon;
    public TMPro.TMP_Text Kill_Text;

   

    public void SetKillUi()
    {

        Kill_UI.gameObject.SetActive(true);
        Kill_Text.text = PlayManager.Instance.CurMultiKill.ToString() + "Kill";
        Kill_UI.SetTrigger("MoveUI");

    }


    public void OnActive()
    {
        StartCoroutine(ActiveCo());
    }

    IEnumerator ActiveCo()
    {
        ActiveObj.SetActive(true);
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.25f);

        Time.timeScale = 1;
        ActiveObj.SetActive(false);
    }




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
        PlayManager.Instance.IsActive = ActiveOnBtn.activeSelf;
        Debug.Log(ActiveOnBtn.activeSelf);
    }



    #region �ؽ�Ʈ���

    public void SetTextPhase(string Text)
    {

        Phase_Text.text = Text;
        PhaseAnim.SetTrigger("PhaseChange");
        /*
        if(PTextCo != null)
        StopCoroutine(PTextCo);

        PTextCo = StartCoroutine(PhaseText(Text));
        */
    }

    /* �̰� ���� �Ⱦ��� �𸣰ڴ٤���
     
    IEnumerator PhaseText(string Text)
    {
        Phase_Text.gameObject.SetActive(true);
        PhaseAnim.SetTrigger("PhaseChange");
        Phase_Text.fontSize = LimitTextSize.x;
        Phase_Text.text = Text;

        while(Phase_Text.fontSize < LimitTextSize.y)
        {
            Phase_Text.fontSize += Time.deltaTime * TextSpeed;

            yield return null;
        }

        Phase_Text.gameObject.SetActive(false);

    }
    */

    #endregion


    #region ĳ���� ���� �˾�

    public void SetCharacterPopUP(bool Check)
    {
        if(Check)
            PopupAnim.SetTrigger("GoLeft");
        else
            PopupAnim.SetTrigger("GoRight");
    }

   

    #endregion


}

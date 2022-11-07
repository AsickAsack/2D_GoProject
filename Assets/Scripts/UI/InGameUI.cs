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
    public Animator CutSceneAnim;

    public void CutScene(int index)
    {
        //액티브 킬때
        if(index == 1 && PlayerDB.Instance.CutScene)
        {
            CutSceneAnim.SetTrigger("CutOn");
        }
        
        ActiveOnBtn.SetActive(!ActiveOnBtn.activeSelf);
        ActiveOffBtn.SetActive(!ActiveOffBtn.activeSelf);
        PlayManager.Instance.IsActive = ActiveOnBtn.activeSelf;
    }

    #region 텍스트기능

    public void SetTextPhase(string Text)
    {
        if(PTextCo != null)
        StopCoroutine(PTextCo);

        PTextCo = StartCoroutine(PhaseText(Text));
    }

    IEnumerator PhaseText(string Text)
    {
        Phase_Text.gameObject.SetActive(true);
        Phase_Text.fontSize = LimitTextSize.x;
        Phase_Text.text = Text;

        while(Phase_Text.fontSize < LimitTextSize.y)
        {
            Phase_Text.fontSize += Time.deltaTime * TextSpeed;

            yield return null;
        }

        Phase_Text.gameObject.SetActive(false);

    }

    #endregion


    #region 캐릭터 선택 팝업

    public void SetCharacterPopUP(bool Check)
    {
        if(Check)
            PopupAnim.SetTrigger("GoLeft");
        else
            PopupAnim.SetTrigger("GoRight");
    }

   

    #endregion


}

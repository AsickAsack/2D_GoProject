using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   
    public PopUpManager popupManager;
    public OptionUI optionUI;
    public ScreenSwtich screen_swtich;
    public CharacterUI characterUI;


    #region 게임 스타트 UI 기능

    public void QuitGame(int index)
    {
        popupManager.OpenPopup(index, "종료하기", "정말 종료 하시겠습니까?",()=> Debug.Log("널"));

    }

    #endregion


    #region 캐릭터창

   public void OpenCharacterUI()
    {
        characterUI.SetCharacterUI();
    }


    #endregion



    #region 씬이동

    public void MoveScene(int index)
    {
        LoadingManager.Instance.Loading_LoadScene(index);
    }


    #endregion
}

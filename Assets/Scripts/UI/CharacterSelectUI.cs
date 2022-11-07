using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public CharacterSelect_Btn[] CS_Btns;
    public CS_List[] MyCs;
    public TMPro.TMP_Text Select_Count;



    //캐릭터 선택 UI열기
    public void OpenCharacterSelectUI()
    {
        for(int i = 0; i < PlayerDB.Instance.MyCharacters.Count;i++)
        {
            //보유 캐릭터 수만큼 켜지고 아이콘 까지 세팅

            CS_Btns[i].SetBtn(GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[i]));

        }
    }
}

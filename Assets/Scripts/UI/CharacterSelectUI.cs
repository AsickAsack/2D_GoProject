using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public CharacterSelect_Btn[] CS_Btns;
    public CS_List[] MyCs;
    public TMPro.TMP_Text Select_Count;



    //ĳ���� ���� UI����
    public void OpenCharacterSelectUI()
    {
        for(int i = 0; i < PlayerDB.Instance.MyCharacters.Count;i++)
        {
            //���� ĳ���� ����ŭ ������ ������ ���� ����

            CS_Btns[i].SetBtn(GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[i]));

        }
    }
}

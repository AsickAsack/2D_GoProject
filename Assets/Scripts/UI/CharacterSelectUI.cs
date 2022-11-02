using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public CharacterSelect_Btn[] CS_Btns;
    public CS_List[] MyCs;
    public TMPro.TMP_Text Select_Count;

    private void Awake()
    {
        CS_Btns = this.GetComponentsInChildren<CharacterSelect_Btn>();
    }

    //캐릭터 선택 UI열기
    public void OpenCharacterSelectUI()
    {
        for(int i=0;i<PlayerDB.Instance.MyCharacters.Count;i++)
        {
            CS_Btns[i].SetBtn(this,PlayerDB.Instance.MyCharacters[i]);
        }
    }

    public void SetSelectList(CharacterSelect_Btn curbtn, Character character)
    {
        for(int i=0;i<MyCs.Length;i++)
        {
            if(!MyCs[i].IsSelect)
            {
                MyCs[i].SetBtn(curbtn, character);
                return;
            }    
        }

        

    }



    //시작할때 선택한 캐릭터들 리스트에 집어넣기

}

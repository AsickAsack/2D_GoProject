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

    //ĳ���� ���� UI����
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



    //�����Ҷ� ������ ĳ���͵� ����Ʈ�� ����ֱ�

}

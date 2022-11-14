using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//보유 캐릭터 선택 List
public class CharacterSelectListUI : MonoBehaviour, IPointerDownHandler
{
    public int MyIndex;
    public Character character;
    public Image Icon;
    public GameObject SelectObj;
    bool IsSelect = false;

    public void SetBtn(Character Mychar)
    {
        this.gameObject.SetActive(true);
        character = Mychar;
        Icon.sprite = GameDB.Instance.GetCharacterIcon(Mychar);
    }

    public void ResetBtn()
    {
        character = null;
        IsSelect = false;
        SelectObj.SetActive(IsSelect);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       

        //이미 선택 된 캐릭터를 눌렀을때
        if (IsSelect)
        { 
            CharacterSelectManager.Instance.Set_CharacterIcon(null,MyIndex);
            //캐릭터 선택 UI 켜줌
            SelectObj.gameObject.SetActive(!IsSelect);
        }
        else //선택 안된 캐릭터를 눌렀을때
        {
            if (CharacterSelectManager.Instance.Count == 5)
            {
                PopUpManager.Instance.OpenPopup(0, "안내", "캐릭터를 전부 선택했습니다.", null);
                return;
            }

            MyIndex = CharacterSelectManager.Instance.GetPointer();
            CharacterSelectManager.Instance.Set_CharacterIcon(character,-1);
            //캐릭터 선택 UI 켜줌
            SelectObj.gameObject.SetActive(!IsSelect);
        }

        //이 버튼이 선택되었는지 check
        IsSelect = !IsSelect;
    }

}

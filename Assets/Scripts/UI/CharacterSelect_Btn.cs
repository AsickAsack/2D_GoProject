using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect_Btn : MonoBehaviour, IPointerDownHandler
{
    private CharacterSelectUI CharacterSelect;
    public Character character;
    public Image Icon;
    bool IsSelect = false;

    private void Awake()
    {
        Icon = this.GetComponent<Image>();
    }

    public void SetBtn(CharacterSelectUI CS, Character character)
    {

        CharacterSelect = CS;
        this.character = character;
        Icon.sprite = GameDB.Instance.GetCharacterIcon(character);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsSelect)
        {

            CharacterSelect.SetSelectList(this, this.character);
            //선택 되었다는 이미지 켜기
            //이거랑 맞는 선택 리스트 지우기
            //밑에 UI와 연동시켜 아이콘 띄워주고

            IsSelect = !IsSelect;
        }
        else
        {
            //선택 되었다는 이미지 끄기
            //마지막 인덱스에 캐릭터 추가
            //밑에 UI와 연동시켜놓은 아이콘 없애주고
        }
        //    추가한 리스트 수/CharacterSelect.MyCs.Length
        
    }
}

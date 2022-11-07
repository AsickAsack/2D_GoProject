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
    public GameObject SelectObj;
    bool IsSelect = false;

    public void SetBtn(Sprite CharSprite)
    {
        this.gameObject.SetActive(true);
        Icon.sprite = CharSprite;
    }


    public void OnPointerDown(PointerEventData eventData)
    {

        //이미 선택한 캐릭터가 3마리라면 리턴시킴, 팝업 띄우자.

        //임시로 저장할 클래스가 필요함
        // 스택을 쓰자

        //캐릭터 선택 UI 켜줌
        SelectObj.gameObject.SetActive(!IsSelect);

        //이미 선택 된 캐릭터를 눌렀을때
        if (IsSelect)
        {
            //캐릭터 선택수 늘려줌(텍스트 세팅)
           //변수에서 빼고 맨 뒤부터 지워줌
        }
        else //선택 안된 캐릭터를 눌렀을때
        {
            //임시 변수에 넣어주고 밑에 캐릭터 UI 앞에서부터 채우자
        }

        //이 버튼이 선택되었는지 check
        IsSelect = !IsSelect;
    }

}

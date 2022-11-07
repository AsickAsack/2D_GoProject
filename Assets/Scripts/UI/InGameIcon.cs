using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameIcon : MonoBehaviour, IPointerClickHandler
{

    public int Myindex;
    private Image Icon;

    //클릭했을때
    public void OnPointerClick(PointerEventData eventData)
    {
        //여기 코드 예쁘게 수정 해야됨 
        //Ready페이즈일때 누르면 소환 , 아무것도 없으면 교환
        if (StageManager.instance.CurCharacters[Myindex].OnBoard) return;

        if(PlayManager.Instance.CurPlayer !=null)
            PlayManager.Instance.CurPlayer.gameObject?.SetActive(false); 


        PlayManager.Instance.CurPlayer = StageManager.instance.CurCharacters[Myindex];
        PlayManager.Instance.CurPlayer.transform.position = PlayManager.Instance.BaseCamp.position;
        PlayManager.Instance.CurPlayer.gameObject.SetActive(true);
    }

    private void Awake()
    {
        Icon = this.transform.GetChild(0).GetComponent<Image>();
    }


    private void Start()
    {
        Icon.sprite = GameDB.Instance.GetCharacterIcon(StageManager.instance.SelectCharacters[Myindex]);
    }
}

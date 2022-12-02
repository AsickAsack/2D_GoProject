using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//캐릭터 선택시 띄워주는 UI를 담당하는 Class
public class CharacterSelectUI : MonoBehaviour
{
    public Image Icon;
    public int CurIndex;
    public bool IsSelected = false;
    public RectTransform myRect;

    public void ResetBtn()
    {
        Icon.gameObject.SetActive(false);
        IsSelected = false;
        this.gameObject.SetActive(false);
    }

    public void SetBtn(int Index,Sprite CharSprite)
    {
        CurIndex = Index;
        Icon.sprite = CharSprite;

        if (CurIndex == -1)
        {
            Icon.gameObject.SetActive(true);
            IsSelected = true;
        }
        else
        {    
            Icon.gameObject.SetActive(false);
            IsSelected = false;
        }

    }
}

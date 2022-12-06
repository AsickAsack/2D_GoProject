using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    int Charindex = 0;
    Vector2 StartPos;
    Vector2 EndPos;
    Vector2 TargetPos;
    
    [SerializeField]
    private Vector2 touchRange;
    public TMPro.TMP_Text Sort_Text;
    public TMPro.TMP_Text CharacterName_Text;
    public Image CharacterIcon;
    public Charcter_DetailUI detail;

    public GameObject HaveCharOBJ;
    public GameObject No_HaveCharOBJ;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndPos = eventData.position;

        TargetPos = EndPos - StartPos;

        if (TargetPos.magnitude > touchRange.x && Mathf.Abs(TargetPos.y) < touchRange.y)
        { 
            SetCharacterUI(TargetPos.normalized.x);
        }
        else 
        {
            if(eventData.pointerCurrentRaycast.gameObject.CompareTag("CharacterUI"))
            {
                detail.SetDetail(PlayerDB.Instance.MyCharacters[Charindex]);
            }
        }
    }

    public void SetCharacterUI()
    {
        CharacterIcon.sprite = GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[Charindex]);
        CharacterName_Text.text = PlayerDB.Instance.MyCharacters[Charindex].Name;
    }

    public void SetCharacterUI(float f)
    {
        

        if (f > 0.0f)
        {
            Charindex = --Charindex % PlayerDB.Instance.MyCharacters.Count;
            if (Charindex < 0) Charindex = PlayerDB.Instance.MyCharacters.Count - 1;
            
        }
        else
            Charindex = ++Charindex % PlayerDB.Instance.MyCharacters.Count;
        


        CharacterName_Text.text = PlayerDB.Instance.MyCharacters[Charindex].Name;
        CharacterIcon.sprite = GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[Charindex]);

    }

    

}

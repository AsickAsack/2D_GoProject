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

    public TMPro.TMP_Text Sort_Text;
    public TMPro.TMP_Text CharacterName_Text;
    public Image CharacterIcon;

     public void OnPointerDown(PointerEventData eventData)
    {
        StartPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndPos = eventData.position;
        SetCharacterUI((EndPos-StartPos).normalized.x);
    }

    
    public void SetCharacterUI()
    {
        CharacterIcon.sprite = GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[Charindex].MyCharacter);
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
        CharacterIcon.sprite = GameDB.Instance.GetCharacterIcon(PlayerDB.Instance.MyCharacters[Charindex].MyCharacter);

    }

    

}

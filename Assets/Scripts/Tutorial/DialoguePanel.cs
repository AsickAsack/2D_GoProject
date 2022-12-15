using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialoguePanel : MonoBehaviour,IPointerClickHandler
{
    public DialogueManager MyManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MyManager.SetDialogue(MyManager.Tutorial_Index))
        {
            MyManager.Tutorial_Index++;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartPanel : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.uimanager.screen_swtich.StartSwitch(1.0f);
        
    }

    
}

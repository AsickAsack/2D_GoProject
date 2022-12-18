using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class StageTutorialPanel : MonoBehaviour, IPointerClickHandler
{

    public StageTutorialManager MyManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(MyManager.ActiveObj != null)
        {
            MyManager.ActiveObj.SetActive(false);
            MyManager.ActiveObj = null;
        }

        if(MyManager.ExplainQueue.Count > 0)
        {
            MyManager.ExplainQueue.Dequeue()();
        }
        else
        {
            MyManager.ExplainCanvas.gameObject.SetActive(false);
            PlayManager.Instance.gameState = GameState.Ready;
            Camera.main.transform.position = MyManager.CameraOrgPos;
            MyManager.MovePanel.SetActive(true);
        }
    }
}

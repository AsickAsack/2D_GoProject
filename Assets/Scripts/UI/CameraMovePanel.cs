using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovePanel : MonoBehaviour, IDragHandler 
{
    [SerializeField]
    private float CameraMoveSpeed;
    [SerializeField]
    private float CameraMoveSmooth;

    public PlayManager playManager;
 

    public void OnDrag(PointerEventData eventData)
    {
        if(!playManager.IsHit)
        { 
                float X = -eventData.delta.x * Time.deltaTime * CameraMoveSpeed;
                float Y = -eventData.delta.y * Time.deltaTime * CameraMoveSpeed;

                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                    new Vector3(Camera.main.transform.position.x + X, Camera.main.transform.position.y + Y, Camera.main.transform.position.z), CameraMoveSmooth * Time.deltaTime);
        }
    }

}

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
       
        if (!playManager.IsHit)
        { 
            float X = -eventData.delta.x * Time.deltaTime * CameraMoveSpeed;
            float Y = -eventData.delta.y * Time.deltaTime * CameraMoveSpeed;

            Vector3 pos = new Vector3(Camera.main.transform.position.x + X, Camera.main.transform.position.y + Y, Camera.main.transform.position.z);

            pos.x = Mathf.Clamp(pos.x, -3, 3);
            pos.y = Mathf.Clamp(pos.y, -1, 2);

            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                pos, CameraMoveSmooth * Time.deltaTime);
        }
    }

}

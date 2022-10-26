using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject Player;
    
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePos,transform.forward,999.0f);
            if(hit)
            {
                Player.transform.position = hit.point;
            }
        }

    }
}

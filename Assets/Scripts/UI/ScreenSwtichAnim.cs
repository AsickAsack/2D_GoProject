using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwtichAnim : MonoBehaviour
{
    public GameObject GameStartPanel;

    public void InActivePanel()
    {
        GameStartPanel.SetActive(false);
    }

}

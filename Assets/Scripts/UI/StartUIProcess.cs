using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIProcess : MonoBehaviour
{
    public GameObject LogoPanel;
    public GameObject GameStartPanel;

    private void Start()
    {
        StartCoroutine(WaitPanel(2.1f));
    }

    IEnumerator WaitPanel(float time)
    {
        yield return new WaitForSeconds(time);
        LogoPanel.SetActive(false);
        GameStartPanel.SetActive(true);
    }







}

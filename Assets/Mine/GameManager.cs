using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool CutScene;

    public UIManager uimanager;

    public List<CharacterPlay> myCharacters = new List<CharacterPlay>();

    private void Awake()
    {
        instance = this;
    }





}

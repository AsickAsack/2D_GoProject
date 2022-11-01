using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uimanager;

    public List<CharacterPlay> myCharacters = new List<CharacterPlay>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {




    }



}

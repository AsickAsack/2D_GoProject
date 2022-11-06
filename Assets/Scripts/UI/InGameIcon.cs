using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameIcon : MonoBehaviour
{
    public CharacterPlay MyCharacter;
    private Image Icon;

    private void Awake()
    {
        Icon = GetComponentInChildren<Image>();
    }





}

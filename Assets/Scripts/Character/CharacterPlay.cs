using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CharacterPlay : MonoBehaviour
{
    public Character character;
    public bool OnBoard =false;
    bool IsPassive;
    public bool IsActive;

    public SpriteRenderer InGame_Sprite;
    public GameObject Effect;
    public int Index;

    private void Awake()
    {
        InGame_Sprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public abstract void AcitveSkill();
    public abstract void PassiveSkill();
      
}


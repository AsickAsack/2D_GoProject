using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterPlay : MonoBehaviour
{

    public Character character;

    bool IsPassive;

    public GameObject Effect;
    public int Index;


    public abstract void AcitveSkill();
    public abstract void PassiveSkill();
    
 
    
}

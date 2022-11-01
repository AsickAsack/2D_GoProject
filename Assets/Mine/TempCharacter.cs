using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TempCharacter : MonoBehaviour
{
    [SerializeField]
    protected float Mass;
    [SerializeField]
    protected float Power;
    [SerializeField]
    protected float Minus_Speed;

    public bool MyTurn;
    protected Rigidbody2D MyRigid
    {
        get => this.GetComponentInChildren<Rigidbody2D>();
    }

    public void IsYourTurn(TempCharacter NewChar)
    {
        MyTurn = false;
        NewChar.MyTurn = true;
    }
   
}

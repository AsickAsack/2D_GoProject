using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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

    public void IsYourTurn(Character NewChar)
    {
        MyTurn = false;
        NewChar.MyTurn = true;
    }
   
}

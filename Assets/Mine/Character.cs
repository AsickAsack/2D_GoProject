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
    protected Rigidbody2D MyRigid
    {
        get => this.GetComponentInChildren<Rigidbody2D>();
    }

   
}

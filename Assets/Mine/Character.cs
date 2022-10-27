using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float Mass;
    protected float Power;

    protected Rigidbody2D MyRigid
    {
        get => this.GetComponentInChildren<Rigidbody2D>();
    }

   
}

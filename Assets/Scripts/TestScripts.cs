using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    Rigidbody2D Myrigid;

    private void Awake()
    {
        Myrigid = this.GetComponent<Rigidbody2D>();

        Vector2 temp = new Vector2(0.0f, 5.0f);
        Myrigid.AddForce(Vector2.up * (temp.magnitude*2), ForceMode2D.Impulse);

        //temp *= 2; 
        //Debug.Log(Myrigid.velocity.magnitude);
    }

}

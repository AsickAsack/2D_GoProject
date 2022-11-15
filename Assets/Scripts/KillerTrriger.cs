using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerTrriger : MonoBehaviour
{
    public CharacterPlay myCharacter;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myCharacter.OnBoard)
        {
            if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
            {
                Rigidbody2D temp = collision.gameObject.GetComponent<Rigidbody2D>();

                if (temp.velocity == Vector2.zero)
                {
                    Debug.Log("¿‚æ∆ ∏‘»˚");
                }
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (myCharacter.OnBoard)
        {
            if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
            {
                Rigidbody2D temp = collision.gameObject.GetComponent<Rigidbody2D>();

                if (temp.velocity == Vector2.zero)
                {
                    Debug.Log("¿‚æ∆ ∏‘»˚");
                }
            }
        }
    }

}

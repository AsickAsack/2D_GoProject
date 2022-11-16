using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StrongSKill : ConfiltAndSKill
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(2, collision.GetContact(0).point));
        collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 2;
        ConflictProcess(collision, this.GetComponent<Rigidbody2D>().velocity.magnitude * 2.0f);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Special_Zone : MonoBehaviour
{
    public abstract void Skill(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            collision.transform.GetComponent<IHomeRun>()?.HomeRunRoutine(this.transform);
        }

        Skill(collision);
    }
}

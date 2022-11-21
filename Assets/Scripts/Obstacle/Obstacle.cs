using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public abstract void Skill(Transform tr);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<bulldozerSkill>() != null) return;

        Skill(collision.transform);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<bulldozerSkill>() != null) return;

        Skill(collision.transform);
    }

}

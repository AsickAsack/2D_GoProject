using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Obstacle
{
    public GameObject Pivot;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBall"))
        {
            //collision.transform.SetParent(Pivot.transform);
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0.01f,0.01f);
            
            StartCoroutine(RotatePlayer(collision.gameObject));
        }
    }
    
    

    IEnumerator RotatePlayer(GameObject Player)
    {
        float time = 0.2f;
        while(time > 0.0f)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.01f, 0.01f);
            time -= Time.deltaTime;
            Player.transform.position = Vector2.Lerp(Player.transform.position, this.transform.position, 20.0f * Time.deltaTime);
            yield return null;
        }


        while(Player.transform.localScale.x > 0.0f)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.01f, 0.01f);
            Player.transform.Rotate(Vector3.forward * Time.deltaTime * 720.0f);

            Player.transform.localScale = Player.transform.localScale - (new Vector3(0.1f, 0.1f) * Time.deltaTime);

            yield return null;
        }

        Player.GetComponent<IDeathProcess>()?.Death();
    }
}

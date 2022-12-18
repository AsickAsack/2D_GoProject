using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MorphingMonster : MonsterPlay
{
    public MorphingCanvas RigCanvas;
    public Vector3 CameraOrgPos;
    public float Speed;
    RectTransform MyRect;
    Animator CameraAnim;

    private void Awake()
    {
        CameraAnim = Camera.main.GetComponent<Animator>();
    }
    public override void Initialize()
    {
        /*
        MorphingAnim = Instantiate(MorphingAnim, this.transform.position, Quaternion.identity);
        MorphingAnim.gameObject.SetActive(false);
        */
        CameraAnim.enabled = false;
        CameraOrgPos = new Vector3(0, 1, -10);

        RigCanvas = Instantiate(RigCanvas, this.transform.position, Quaternion.identity);
        RigCanvas.gameObject.SetActive(false);

        RigCanvas.MorphingGraphic.AnimationState.Complete += EndEvent;
    }


    public void EndEvent(Spine.TrackEntry entry)
    {

        RigCanvas.gameObject.SetActive(false);
        MyRect.localScale = new Vector2(0.8f, 0.8f);
        Time.timeScale = 1.0f;
    }

    public override void ConflictPlayer(Collision2D collision)
    {
        //서있는 흰돌 맞았을때도 생각
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {

            SoundManager.Instance.PlayEffect(9);

            RigCanvas.gameObject.SetActive(true);
            CameraAnim.enabled = true;
            CameraAnim.SetTrigger("Shake");
            StartCoroutine(MinusScale());
            Time.timeScale = 0;


            ICompareSkill CK = collision.transform.GetComponent<ICompareSkill>();

            CK.GoForward((collision.GetContact(0).point - (Vector2)this.transform.position).normalized, MyRigid.velocity.magnitude, this.transform);

        }
    }

    IEnumerator MinusScale()
    {
       MyRect = RigCanvas.MorphingGraphic.GetComponent<RectTransform>();

        while (MyRect.localScale.x > 0.2f)
        {
            MyRect.localScale -= new Vector3(0.1f, 0.1f,0.0f) * Time.unscaledDeltaTime * Speed;
            
            yield return null;
        }
    }
  

}

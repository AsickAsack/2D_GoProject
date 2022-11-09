using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour
{

    private void Awake()
    {
        if(!PlayerDB.Instance.IsFirst) 
            this.gameObject.SetActive(false);


    }
}

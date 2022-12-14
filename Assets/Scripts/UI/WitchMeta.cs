using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMeta : MonoBehaviour
{
   
    public void SetWitchSibling(int index)
    {
        this.transform.SetSiblingIndex(this.transform.GetSiblingIndex() + index);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSet : MonoBehaviour
{
    public EffectName effectName;
    float Duration;
    

    private void Awake()
    {
        Duration = this.GetComponent<ParticleSystem>().main.duration;
    }

    private void Update()
    {
        Duration -= Time.deltaTime;

        if (Duration < 0.0f)
        {
            ReturnToPool();
        }
    }
    public void ReturnToPool()
    {
        Duration = this.GetComponent<ParticleSystem>().main.duration;
        PlayManager.Instance.objectPool.ReturnPrefabs(effectName, this.gameObject);
    }
    
}

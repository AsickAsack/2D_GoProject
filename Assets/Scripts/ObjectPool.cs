using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectName
{
    StoneHit,PlayerFall,MonsterFall,Active,Passive,SpecialZone
}

public class ObjectPool : MonoBehaviour
{
    //��Ƽ�� ����Ʈ(���Ӵ� 1���� ������ ������ ������ƮǮx)
    [SerializeField]
    private GameObject[] EffectPrefabs;

    //���� ���̴� ����Ʈ�� ������Ʈ Ǯ
    [SerializeField]
    private GameObject[] PoolEffectPrefab;
    private Dictionary<EffectName, Queue<GameObject>> objectPool = new Dictionary<EffectName, Queue<GameObject>>();

    [SerializeField]
    private int[] CreateNum;

    public GameObject GetActiveEffects(int index,Vector2 pos)
    {
        return Instantiate(EffectPrefabs[index], pos,Quaternion.identity);
    }



    private void Awake()
    {
        InitializePool();
    }

    //�� �����ϸ� ����
    public void InitializePool()
    {
      
        for (int i = 0; i < PoolEffectPrefab.Length; i++)
        {

            objectPool.Add((EffectName)i, new Queue<GameObject>());

            for (int j = 0; j < CreateNum[i]; j++)
            {
                GameObject Obj = Instantiate(PoolEffectPrefab[i]);
                Obj.SetActive(false);
                objectPool[(EffectName)i].Enqueue(Obj);
            }
        }
        
    }

    /// <summary>
    /// StoneHit,PlayerFall,MonsterFall,Active,Passive,SpecialZone
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetPoolEffect(EffectName name, Vector2 pos, Quaternion rot)
    {
        if (objectPool[name].Count == 0)
        {
            return Instantiate(PoolEffectPrefab[(int)name], pos, rot);
        }
        else
        {
            GameObject obj = objectPool[name].Dequeue();
            obj.transform.SetPositionAndRotation(pos, rot);
            obj.SetActive(true);

            return obj;
        }

    }

    /// <summary>
    /// StoneHit,PlayerFall,MonsterFall,Active,Passive,SpecialZone
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    //������ �ݳ�
    public void ReturnPrefabs(EffectName name, GameObject obj)
    {
        Debug.Log(obj.gameObject.name);
        obj.gameObject.SetActive(false);
        objectPool[name].Enqueue(obj);
    }

    //�Ϲ� ����Ʈ
    public GameObject GetEffect(int index,Vector2 pos,Quaternion rot)
    {
        return Instantiate(EffectPrefabs[index],pos,rot);
    }

}

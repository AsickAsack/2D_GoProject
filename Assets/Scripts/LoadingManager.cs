using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{

    private static LoadingManager instance = null;

    public static LoadingManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LoadingManager>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "LoadingManager";
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<LoadingManager>();
                }
            }

            return instance;
        }
    }


    public void Loading_LoadScene(int i)
    {
        StartCoroutine(SceneLoading(i));
    }

    IEnumerator SceneLoading(int i)
    {
        yield return SceneManager.LoadSceneAsync(1);
        yield return StartCoroutine(Loading(i));

    }



    IEnumerator Loading(int index)
    {
        Debug.Log("코루틴 시작");

        LoadingHelper Loading_Helper = GameObject.FindObjectOfType<LoadingHelper>();
        AsyncOperation ao = SceneManager.LoadSceneAsync(index);

        ao.allowSceneActivation = false;

        
        while(!ao.isDone)
        {
            if(Loading_Helper != null)
                Loading_Helper.SetLoading(ao.progress);
           
            
            
            if(Mathf.Approximately(ao.progress,0.9f))
            {
                ao.allowSceneActivation = true;
            }
            
            yield return null;
        }
        
        

    }
}

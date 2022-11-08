using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownLoadDB : MonoBehaviour
{
    public IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(GameDB.Char_URL);
        yield return www.SendWebRequest();

        GameDB.Instance.InputDB(GameDB.Instance.DownLoad_DB(www), GameDB.CharacterDB);

        UnityWebRequest www2 = UnityWebRequest.Get(GameDB.Mon_URL);
        yield return www2.SendWebRequest();

        GameDB.Instance.InputDB(GameDB.Instance.DownLoad_DB(www2), GameDB.MonsterDB);

        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.Strong));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.lazy));
        PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.mad));

 
    }
}

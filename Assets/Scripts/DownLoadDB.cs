using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownLoadDB : MonoBehaviour
{
    public StoreManager storeManager;

    public IEnumerator Start()
    {
        PlayerDB.Instance.LoadData();

        if (!PlayerDB.Instance.IsFirst)
        {
            storeManager.CheckStore();
            yield break;
        }
            

        UnityWebRequest www = UnityWebRequest.Get(GameDB.Char_URL);
        yield return www.SendWebRequest();

        GameDB.Instance.InputDB(GameDB.Instance.DownLoad_DB(www), GameDB.CharacterDB);

        UnityWebRequest www2 = UnityWebRequest.Get(GameDB.Mon_URL);
        yield return www2.SendWebRequest();

        GameDB.Instance.InputDB(GameDB.Instance.DownLoad_DB(www2), GameDB.MonsterDB);

        /*
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Strong));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Olaf));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.mad));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Killer));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Witch));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.BaseBall));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Leesin));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.Rebirth));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.brainwash));
        //PlayerDB.Instance.MyCharacters.Add(new Character((int)CharacterName.Avoid));
        PlayerDB.Instance.playerdata.MyCharacters.Add(new Character((int)CharacterName.bulldozer));
        */

        storeManager.CheckStore();

        PlayerDB.Instance.IsFirst = false;
    }
}

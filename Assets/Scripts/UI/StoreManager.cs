using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public StoreCharacterUI[] StoreCharacters;

    private void Awake()
    {
        StoreCharacters = this.GetComponentsInChildren<StoreCharacterUI>();
        for(int i=0;i<StoreCharacters.Length;i++)
        {
            StoreCharacters[i].gameObject.SetActive(false);
        }
    }


    public void CheckStore()
    {
        CharacterName charname = CharacterName.Strong;

        for(int i=0; i<GameDB.Instance.Characters.Length; i++)
        {
            if(!StoreCharacters[i].gameObject.activeSelf)
            {
                StoreCharacters[i].SetCharacterUI((int)charname++); 
            }
        }
    }

}

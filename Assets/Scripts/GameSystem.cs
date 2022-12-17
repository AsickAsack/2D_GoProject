using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public static class GameSystem
{
    public static void Save<T>(ref T SaveClass,string FilePath)
    {
        string data = JsonConvert.SerializeObject(SaveClass);
        File.WriteAllText(Application.persistentDataPath + FilePath, data);
    }

    public static bool Load<T>(ref T LoadClass,string FilePath)
    {
        if (File.Exists(Application.persistentDataPath + FilePath))
        {
            string data = File.ReadAllText(Application.persistentDataPath + FilePath);
            LoadClass = JsonConvert.DeserializeObject<T>(data);

            return true;
        }

        return false;
    }

    public static void Save<T>(ref T[] SaveClass, string FilePath)
    {
        string data = JsonConvert.SerializeObject(SaveClass);
        File.WriteAllText(Application.persistentDataPath + FilePath, data);
    }

    public static void Load<T>(ref T[] LoadClass, string FilePath)
    {
        if (File.Exists(Application.persistentDataPath + FilePath))
        {
            string data = File.ReadAllText(Application.persistentDataPath + FilePath);
            LoadClass = JsonConvert.DeserializeObject<T[]>(data);
        }
    }

}

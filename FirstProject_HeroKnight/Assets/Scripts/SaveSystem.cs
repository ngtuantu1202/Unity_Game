using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public float x;
    public float y;
}

public static class SaveSystem
{
    //private static string path = Application.persistentDataPath + "/save.json";
    private static string path = Application.dataPath + "/save.json";


    public static void SaveCheckpoint(Vector2 checkpoint)
    {
        SaveData data = new SaveData { x = checkpoint.x, y = checkpoint.y };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static Vector2 LoadCheckpoint()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return new Vector2(data.x, data.y);
        }

        return Vector2.zero; // fallback nếu chưa có file
    }

    public static void DeleteCheckpoint()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }


    public static bool HasSaveFile()
    {
        return File.Exists(path);
    }
}

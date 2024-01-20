using System.IO;
using UnityEngine;

public static class DataManager
{
    public static string directory = Directory.GetCurrentDirectory();
    public static string fileName = "/config.txt";
    public static void Save(ConfigData sData, bool dataMultipleLines = true)
    {
        string json = JsonUtility.ToJson(sData, dataMultipleLines);
        File.WriteAllText(directory + fileName, json);

        Debug.Log("Saved config file");
    }

    public static ConfigData Load()
    {
        string fullPath = directory + fileName;
        ConfigData loadData = new ConfigData();

        if (File.Exists(fullPath))
        {
            if (new FileInfo(fileName.Replace("/", "")).Length != 0)
            {
                string json = File.ReadAllText(fullPath);
                loadData = JsonUtility.FromJson<ConfigData>(json);
                return loadData;
            }
            else
            {
                UnityEngine.Debug.Log("Empty File");
                return null;
            }
        }
        else
        {
            UnityEngine.Debug.Log("Save File does not exist & create new One");
            var newFile = File.Create(fullPath);
            newFile.Close();
            return null;
        }
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderConfig : MonoBehaviour
{
    public static LoaderConfig Instance = null;
    public ConfigData configData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        this.LoadRecords();
    }


    public void LoadRecords()
    {
        if (DataManager.Load() != null)
        {
            this.configData = DataManager.Load();
        }
        else
        {
            Debug.Log("config file is empty and get data from inspector!");
            this.SaveRecords();
        }

        this.changeScene(1);
    }

    public void SaveRecords()
    {
        DataManager.Save(this.configData);
    }

    public void changeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }


    private void OnDisable()
    {
        this.SaveRecords();
    }

    private void OnApplicationQuit()
    {
        this.SaveRecords();
    }
}

[System.Serializable]
public class ConfigData
{

}

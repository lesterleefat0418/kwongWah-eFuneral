using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoaderConfig : MonoBehaviour
{
    public static LoaderConfig Instance = null;
    public ConfigData configData;
    public int languageId;
    public int religionId;
    public int selectReligionSceneLastPageId = 0;
    public bool skipToHuabaoStage = false;
    public Font tc, sc;
    public CanvasGroup serverErrorBox;
    public Text errorText; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public int SelectedLanguageId
    {
        get { return this.languageId; }
        set { this.languageId = value; }
    }

    public int SelectedReligionId
    {
        get { return this.religionId; }
        set { this.religionId = value; }
    }


    void Start()
    {
        this.LoadRecords();
        this.setServerPopup(false);
    }

    public void closePopup()
    {
        this.setServerPopup(false);
    }
    public void setServerPopup(bool status, string content = "")
    {
        SetUI.Run(this.serverErrorBox, status);
        if(this.errorText != null) this.errorText.text ="Server Host error: " + content; 
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

        ScreenController.Instance.isTopMostEnabled = this.configData.topMostEnable;
        ScreenController.Instance.EnableScreenFoucs();

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
    public string getImageFolderName = "KwongwahServer";
    public string adminPassword="000000";
    public bool isLogined = false;
    public float fullGameTime = 1200f;
    public float onlyHuabaoTime = 600f;
    public float goodByePageDuration = 150f;
    public float latestUploadPhotoWithinTime = 5f;
    public bool topMostEnable = false;
}

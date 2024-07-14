using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : FocusWindow
{
    public Vector2Int resolution;
    public bool mouseStatus = true;
    // Start is called before the first frame update

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
    }

    protected override void Start()
    {
        Screen.SetResolution(this.resolution.x, this.resolution.y, true);
        Cursor.visible = this.mouseStatus;

        //base.Start();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            mouseStatus = !this.mouseStatus;
            Cursor.visible = mouseStatus;
        }
        else if (Input.GetKeyDown(KeyCode.F2) && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            LoaderConfig.Instance.configData.topMostEnable = !LoaderConfig.Instance.configData.topMostEnable;
            this.isOn = LoaderConfig.Instance.configData.topMostEnable;
        }
        else if (Input.GetKeyDown(KeyCode.R) && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            Debug.Log("Refresh Scene");
            SceneManager.LoadScene(1);
        }

        /*foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                Debug.Log("Key pressed: " + keyCode);
            }
        }*/
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public int languageId;
    public CanvasGroup captureBg, adminLogin;
    public CountDownTimer countDownTimer;
    public bool showAdminLogin = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public int SelectedLanguageId
    {
        get{return this.languageId;}
        set{this.languageId = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        this.pageController.init();
        SetUI.Run(this.captureBg, false, 0f);
        SetUI.Run(this.adminLogin, false, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.pageController.currentId == 0)
        {
            if (Input.GetKeyDown("a") && !this.showAdminLogin)
            {
                this.showAdminLogin = !this.showAdminLogin;
                SetUI.Run(this.adminLogin, this.showAdminLogin, 0f);
            }
        }
    }

    public void SetLang(int langId)
    {
        this.SelectedLanguageId = langId;
        string lang = "";
        switch (this.SelectedLanguageId)
        {
            case 0:
                lang = "TC";
                break;
            case 1:
                lang = "CN";
                break;
            case 2:
                lang = "Eng";
                break;

        }
        Debug.Log("current lang: " + lang);
    }

    public void ChangePage(int toPageId)
    {
        this.pageController.setPage(toPageId);
        if(toPageId >= 4) SetUI.Run(this.captureBg, true, 0f);
        if(toPageId >= 5) if (this.countDownTimer != null) this.countDownTimer.showTimer();
    }

    public void closeAdminLogin()
    {
        this.showAdminLogin = false;
        SetUI.Run(this.adminLogin, false, 0f);
    }

    public void BackToHome()
    {
        Debug.Log("reload scene");
        SceneManager.LoadScene(1);
    }


    private void OnApplicationQuit()
    {
        if(VirtualKeyboard.Instance != null)
        {
            VirtualKeyboard.Instance.HideOnScreenKeyboard();
            VirtualKeyboard.Instance.HideTouchKeyboard();
        }
    }
}

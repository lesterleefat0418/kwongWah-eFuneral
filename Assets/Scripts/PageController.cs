using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public Language language;
    public int languageId;
    public CanvasGroup captureBg, adminLogin;
    public InputField adminPasswordField;
    public CountDownTimer countDownTimer;
    [HideInInspector]
    public bool showAdminLogin = false;
    public GameObject adminBtn, logoutBtn;

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
        if (this.adminBtn != null) this.adminBtn.SetActive(LoaderConfig.Instance != null ? !LoaderConfig.Instance.configData.isLogined : false);
        if (this.logoutBtn != null) this.logoutBtn.SetActive(LoaderConfig.Instance != null ? LoaderConfig.Instance.configData.isLogined : false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLang(int langId)
    {
        this.SelectedLanguageId = langId;
        string lang = "";
        switch (this.SelectedLanguageId)
        {
            case 0:
                lang = "TC";
                this.language.setTC();
                break;
            case 1:
                lang = "CN";
                this.language.setCN();
                break;
            case 2:
                lang = "Eng";
                this.language.setENG();
                break;

        }
        Debug.Log("current lang: " + lang);
    }

    public void ChangePage(int toPageId)
    {
        this.pageController.setPage(toPageId);
        if (toPageId >= 4) SetUI.Run(this.captureBg, true, 0f);
        if (toPageId >= 5) if (this.countDownTimer != null) this.countDownTimer.showTimer();
        if (toPageId == 5) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
        if (toPageId == 6) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(-532f, 0f);
        if (toPageId == 7) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
    }

    public void controlAdminLogin(bool isLogined)
    {
        this.showAdminLogin = isLogined;
        SetUI.Run(this.adminLogin, isLogined, 0f);
    }

    public void loginBtn()
    {
        if(this.adminPasswordField != null && LoaderConfig.Instance != null)
        {
            if (!string.IsNullOrEmpty(this.adminPasswordField.text))
            {
                if(this.adminPasswordField.text == LoaderConfig.Instance.configData.adminPassword)
                {
                    LoaderConfig.Instance.configData.isLogined = true;
                    Debug.Log("Admin logined");
                    this.controlAdminLogin(false);
                    this.adminPasswordField.text = "";
                    VirtualKeyboard.Instance.HideOnScreenKeyboard();
                    if (this.adminBtn != null) this.adminBtn.SetActive(false);
                    if (this.logoutBtn != null) this.logoutBtn.SetActive(true);
                }
                else
                {
                    LoaderConfig.Instance.configData.isLogined = false;
                    Debug.Log("Wrong Password");
                    this.adminPasswordField.text = "";
                }
            }
        }
    }

    public void logout()
    {
        if(LoaderConfig.Instance != null)
        {
            LoaderConfig.Instance.configData.isLogined = false;
            if (this.adminBtn != null) this.adminBtn.SetActive(true);
            if (this.logoutBtn != null) this.logoutBtn.SetActive(false);
        }
    }

    public void BackToHome()
    {
        Debug.Log("reload scene");
        SceneManager.LoadScene(1);
    }


}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public Language language;
    public LanguageUI[] languageUI;
    public Font tc, sc;
    public CanvasGroup captureBg;
    public CountDownTimer countDownTimer;
    public Timer idlingTimer;
    public CanvasGroup[] HuabaoStage;
    public CanvasGroup leavePopup;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.pageController.Init();
        this.SetLang();
        SetUI.Run(this.captureBg, false, 0f);
        SetUI.Run(this.leavePopup, false, 0f);
        this.showHuabaoStage(LoaderConfig.Instance.skipToHuabaoStage);
    }

    public void showHuabaoStage(bool skip)
    {
        if(skip) { 
            this.HuabaoStage[0].DOFade(1f, 0f);
            this.HuabaoStage[0].interactable = true;
            this.HuabaoStage[0].blocksRaycasts = true;
            this.HuabaoStage[1].DOFade(0f, 0f);
            this.HuabaoStage[1].interactable = false;
            this.HuabaoStage[1].blocksRaycasts = false;
            this.ChangePage(4);
            if (this.countDownTimer != null)
            {
                this.countDownTimer.totalTime = LoaderConfig.Instance.configData.onlyHuabaoTime;
                this.countDownTimer.init();
                this.countDownTimer.showTimer();
                this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
            }
        }
        else
        {
             if (this.countDownTimer != null)
            {
                this.countDownTimer.totalTime = LoaderConfig.Instance.configData.fullGameTime;
                this.countDownTimer.init();
            }
            this.HuabaoStage[0].DOFade(0f, 0f);
            this.HuabaoStage[0].interactable = false;
            this.HuabaoStage[0].blocksRaycasts = false;
            this.HuabaoStage[1].DOFade(1f, 0f);
            this.HuabaoStage[1].interactable = true;
            this.HuabaoStage[1].blocksRaycasts = true;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetLang()
    {
        string lang = "";
        switch (LoaderConfig.Instance.SelectedLanguageId)
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
        for (int i = 0; i < this.languageUI.Length; i++)
        {
            if (this.languageUI[i] != null) this.languageUI[i].setLang();
        }
    }

    public void ChangePage(int toPageId)
    {
        if(toPageId == 2)
        {
            if (SettingHall.Instance != null && SettingHall.Instance.skipToFeelingPage)
            {
                toPageId += 1;
            }
        }

        this.pageController.setPage(toPageId, ()=>changePageFunction(toPageId));
    }

    void changePageFunction(int toPageId)
    {
        SetUI.Run(this.captureBg, toPageId >= 2 ? true : false, 0f);

        switch (toPageId)
        {
            case 3:
                if (this.countDownTimer != null) { 
                    this.countDownTimer.showTimer();
                    this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
                }
                break;
            case 4:
                if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(-532f, 0f);
                if (Huabao.Instance != null) Huabao.Instance.setHuaBao(true);
                break;
            case 5:
                if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(692f, 0f);
                if (Huabao.Instance != null) Huabao.Instance.setHuaBao(false);
                break;
        }
    }

    public void BackToSelectReligion()
    {
        LoaderConfig.Instance.selectReligionSceneLastPageId = 1;
        SceneManager.LoadScene(1);
    }
    
    public void BackToHome()
    {
        Debug.Log("Show leave popup box");
        SetUI.Run(this.leavePopup, true, 0.5f);
    }

    public void showPopup(CanvasGroup popup)
    {
        if(popup.interactable == false) { 
            Debug.Log("Show popup box");
            SetUI.Run(popup, true);
        }
    }

    public void closePopup(CanvasGroup popup)
    {
        if (popup.interactable) { 
            Debug.Log("Close popup box");
            SetUI.Run(popup, false);
        }
    }

    public void confirmBackToHome()
    {
        Debug.Log("reload scene");
        LoaderConfig.Instance.selectReligionSceneLastPageId = 0;
        SceneManager.LoadScene(1);
    }

    public void countDownFinished()
    {
        if (LoaderConfig.Instance.skipToHuabaoStage)
        {
            this.confirmBackToHome();
        }
        else
        {
            this.ChangePage(5);
            if (this.idlingTimer != null) this.idlingTimer.showTimer();
        }
    }
}

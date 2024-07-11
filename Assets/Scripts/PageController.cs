using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public Language language;
    public LanguageUI[] languageUI;
    public CanvasGroup captureBg, beginningBox;
    public CountDownTimer countDownTimer;
    public Timer idlingTimer;
    public CanvasGroup[] HuabaoStage;
    public CanvasGroup leavePopup;
    private float showBeginFeelingBoxDuration = 1.0f;

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

    IEnumerator showBeginFeelingBox(float _delay = 2f)
    {
        SetUI.Run(this.beginningBox, true, 0.5f);
        yield return new WaitForSeconds(_delay);
        SetUI.Run(this.beginningBox, false, 0.5f);
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

        switch (toPageId)
        {
            case 0:
                if (PeoplePhotoLoader.Instance != null)
                {
                    PeoplePhotoLoader.Instance.enabled = true;
                }
                break;
            case 1:
                if(PeoplePhotoLoader.Instance != null)
                {
                    PeoplePhotoLoader.Instance.enabled = false;
                    PeoplePhotoLoader.Instance.StopCheckUploadPhoto();
                }
                break;
        }
    }

    void changePageFunction(int toPageId)
    {
        SetUI.Run(this.captureBg, toPageId >= 2 ? true : false, 0f);

        switch (toPageId)
        {
            case 3:
                if (this.countDownTimer != null) {
                    if (!this.countDownTimer.triggerToStart)
                    {
                        StartCoroutine(this.showBeginFeelingBox(this.showBeginFeelingBoxDuration));
                    }
                    this.countDownTimer.showTimer();
                    this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
                }
                if (Huabao.Instance != null) Huabao.Instance.setHuaBao(false);
                break;
            case 4:
                if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(-532f, 0f);
                if (Huabao.Instance != null) Huabao.Instance.setHuaBao(true);
                if (VirtualKeyboard.Instance != null) VirtualKeyboard.Instance.HideTouchKeyboard();
                break;
            case 5:
                if (Huabao.Instance != null) Huabao.Instance.setHuaBao(false);
                if (VirtualKeyboard.Instance != null) VirtualKeyboard.Instance.HideTouchKeyboard();
                if (this.countDownTimer != null)
                {
                    this.countDownTimer.transform.DOLocalMoveX(692f, 0f);


                    if(this.countDownTimer.currentTime <= 180f)
                    {
                        if (this.countDownTimer.currentTime < 150f)
                        {
                            this.countDownTimer.triggeredRemindFinalMinutes = true;
                            this.countDownTimer.triggerToStart = false;
                            this.countDownTimer.totalTime = LoaderConfig.Instance.configData.goodByePageDuration;
                            this.countDownTimer.init();
                            this.countDownTimer.showTimer();

                            /*if (this.idlingTimer != null)
                            {
                                //this.idlingTimer.totalTime = LoaderConfig.Instance.configData.goodByePageDuration;
                                this.idlingTimer.totalTime = 15;
                                this.idlingTimer.showTimer();
                            }*/
                        }
                        /*else
                        {
                            if (this.idlingTimer != null)
                            {
                                this.idlingTimer.totalTime = this.countDownTimer.currentTime;
                                this.idlingTimer.showTimer();
                            }
                        }*/
                    }
                    else
                    {
                        this.countDownTimer.triggeredRemindFinalMinutes = true;
                        this.countDownTimer.triggerToStart = false;
                        this.countDownTimer.totalTime = LoaderConfig.Instance.configData.goodByePageDuration;
                        this.countDownTimer.init();
                        this.countDownTimer.showTimer();
                    }


                }              
                break;
        }
    }

    public void BackToSelectReligion()
    { 
        if(LoaderConfig.Instance != null) LoaderConfig.Instance.selectReligionSceneLastPageId = 1;
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
            if(this.pageController.currentId == 5)
            {
                this.confirmBackToHome();
            }
            else
            {
                this.ChangePage(5);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public Language language;
    public CanvasGroup captureBg;
    public CountDownTimer countDownTimer;
    public CanvasGroup[] HuabaoStage;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.pageController.init();
        SetUI.Run(this.captureBg, false, 0f);
        if (SettingHall.Instance != null) SettingHall.Instance.setGameMode();
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
            if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
            this.ChangePage(4);
        }
        else
        {
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
        switch (LoaderConfig.Instance.languageId)
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
        SetUI.Run(this.captureBg, toPageId >= 2 ? true : false, 0f);
        if (toPageId >= 3) if (this.countDownTimer != null) this.countDownTimer.showTimer();
        if (toPageId == 3) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
        if (toPageId == 4) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(-532f, 0f);
        if (toPageId == 5) if (this.countDownTimer != null) this.countDownTimer.transform.DOLocalMoveX(532f, 0f);
    }

    public void BackToSelectReligion()
    {
        LoaderConfig.Instance.selectReligionSceneLastPageId = 1;
        SceneManager.LoadScene(1);
    }
    
    public void BackToHome()
    {
        Debug.Log("reload scene");
        LoaderConfig.Instance.selectReligionSceneLastPageId = 0;
        SceneManager.LoadScene(1);
    }


}

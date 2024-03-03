using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    public static PageController Instance = null;
    public Page pageController;
    public int languageId;
    public CanvasGroup captureBg;

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
        if(this.captureBg != null) this.captureBg.alpha = 0;
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
        if(toPageId >= 4) if (this.captureBg != null) this.captureBg.alpha = 1;
    }

    public void BackToHome()
    {
        Debug.Log("reload scene");
        SceneManager.LoadScene(1);
    }
}

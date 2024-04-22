using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[System.Serializable]
public class ProcessSteps
{
    public List<int> simpleStepIds = new List<int>();
    public GameObject[] processSteps;
    public Image[] stepsHintFrame;
}


public class SettingHall : MonoBehaviour
{
    public static SettingHall Instance = null;
    public int selectedHallId = 1;
    public Page steps;
    public Color stepColor;
    public ProcessSteps processSteps;
    public Select[] settingSteps;
    public SelectFood[] food;
    public HallSpeakText hallSpeakText;
    public bool skipToFeelingPage = true;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void setGameMode()
    {
        for (int i = 0; i < this.settingSteps.Length; i++)
        {
            this.settingSteps[i].init();
        }

        if (!LoaderConfig.Instance.configData.isLogined)
        {
            Debug.Log("Public Mode");
            for (int i = 0; i < this.processSteps.processSteps.Length; i++)
            {
                if (this.processSteps.simpleStepIds.Contains(i))
                {
                    this.processSteps.processSteps[i].SetActive(true);
                    this.setStepFrame(i);
                    this.steps.init(null, i);
                    this.steps.currentId = i;
                    skipToFeelingPage = false;
                }
                else
                {
                    this.processSteps.processSteps[i].SetActive(false);
                }
            }

            for (int i = 0; i < this.settingSteps.Length; i++)
            {
                if(this.settingSteps[i].decoration.Length > 0) {

                    int publicDefaultId = this.settingSteps[i].decoration[this.selectedHallId].publicDefaultId;
                    this.settingSteps[i].set(publicDefaultId, this.selectedHallId);
                }
            }

            this.hallSpeakText.setDefaultPublic(this.selectedHallId);

            if (SendFeelings.Instance != null)
                SendFeelings.Instance.setGameMode(true);
        }
        else
        {
            Debug.Log("Detail Mode");
            this.skipToFeelingPage = false;
            for (int i = 0; i < this.processSteps.processSteps.Length; i++)
            {
                this.processSteps.processSteps[i].SetActive(true);
            }

            this.setStepFrame(0);
            this.steps.init();
            this.hallSpeakText.reset(this.selectedHallId);

            for (int i = 0; i < this.settingSteps.Length; i++)
            {
                this.settingSteps[i].init();
            }

            if (SendFeelings.Instance != null)
                SendFeelings.Instance.setGameMode(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeStep()
    {
        if (LoaderConfig.Instance.configData.isLogined) {
            this.steps.toNextPage(
                () => this.setStepFrame(this.steps.currentId),
                () => PageController.Instance.ChangePage(PageController.Instance.pageController.currentId + 1)
            );
        }
        else
        {
            this.setStepFrame(this.steps.currentId);
            PageController.Instance.ChangePage(PageController.Instance.pageController.currentId + 1);
        }
    }

    public void changePreviousStep()
    {
        if (LoaderConfig.Instance.configData.isLogined)
        {
            this.steps.toPreviousPage(
                () => this.setStepFrame(this.steps.currentId),
                () => resetHallSelected()
            );
        }
        else
        {
            this.setStepFrame(this.steps.currentId);
            PageController.Instance.ChangePage(PageController.Instance.pageController.currentId - 1);
        }
    }

    public void resetHallSelected()
    {
        for (int i = 0; i < this.settingSteps.Length; i++)
        {
            this.settingSteps[i].reset();
        }
        this.SelectSpeakText(-1);
        for (int i = 0; i < this.food.Length; i++)
        {
            if (this.food[i] != null)
            {
                this.food[i].NotSelect();
            }
        }
        PageController.Instance.ChangePage(PageController.Instance.pageController.currentId - 1);
    }

    public void skipStep()
    {
        switch (this.steps.currentId)
        {
            case 0:
                this.SetSettingSteps(-1);
                break;
            case 1:
                this.SetSettingSteps(-1);
                break;
            case 2:
                this.SetSettingSteps(-1);
                break;
            case 3:
                this.SelectSpeakText(-1);// not select
                break;
            case 4:
                this.SetSettingSteps(-1);
                break;
            case 5:
                for(int i=0; i< this.food.Length; i++)
                {
                    if(this.food[i] != null)
                    {
                        this.food[i].NotSelect();
                    }
                }
                break;

        }
        this.changeStep();

    }

    public void SetSettingSteps(int id)
    {
        if (this.settingSteps[this.steps.currentId].selected != id) { 
            this.settingSteps[this.steps.currentId].set(id, this.selectedHallId);
        }
        else {
            this.settingSteps[this.steps.currentId].selected = -1;
            this.settingSteps[this.steps.currentId].set(-1, this.selectedHallId);

        }
    }


    public void SelectSpeakText(int id)
    {
        int currentStep = this.steps.currentId;
        if (id == -1 || this.settingSteps[currentStep].selected == id) {

            this.settingSteps[currentStep].selected = -1;
            this.settingSteps[currentStep].set(-1, this.selectedHallId);
            this.hallSpeakText.reset(this.selectedHallId);
        }
        else
        {
            this.settingSteps[currentStep].set(id, this.selectedHallId);

            Text wordtext = this.settingSteps[currentStep].options[id].GetComponentInChildren<Text>();

            if (LoaderConfig.Instance.languageId == 0)
            {
                char[] wordsArray = wordtext.text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setTC(wordsArray, this.selectedHallId);
            }
            else if (LoaderConfig.Instance.languageId == 1)
            {
                char[] wordsArray = wordtext.text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setSC(wordsArray, this.selectedHallId);
            }
            else if (LoaderConfig.Instance.languageId == 2)
            {
                char[] wordsArray = wordtext.text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setEng(wordsArray, this.selectedHallId);
            }
        }    
    }

    public void SelectSpeakTextSwitchPage(bool right)
    {
        this.settingSteps[3].changePage(right);
    }

    void setStepFrame(int id)
    {
        for(int i = 0; i < this.processSteps.stepsHintFrame.Length; i++)
        {
            if (this.processSteps.stepsHintFrame[i] != null)
            {
                if (i == id)
                {
                    this.processSteps.stepsHintFrame[id].DOFade(1f, 0f);
                    var childTexts = this.processSteps.stepsHintFrame[id].GetComponentsInChildren<Text>();
                    for (int k=0; k< childTexts.Length; k++)
                    {
                        childTexts[k].color = stepColor;
                    }
                }
                else
                {
                    this.processSteps.stepsHintFrame[i].DOFade(0f, 0f);
                    var childTexts = this.processSteps.stepsHintFrame[i].GetComponentsInChildren<Text>();
                    for (int k = 0; k < childTexts.Length; k++)
                    {
                        childTexts[k].color = Color.white;
                    }
                }
            }
        }
    }


}

[System.Serializable]
public class Decoration
{
    public string name;
    public CanvasGroup[] choice;
    public int publicDefaultId;
    public int selected = -1;

    public void publicInit()
    {
        this.set(this.publicDefaultId);
    }


    public void set(int id, float _duration = 0f)
    {
        this.selected = id;
        for (int i = 0; i < this.choice.Length; i++)
        {
            if (i == id)
            {
                this.choice[id].DOFade(1f, _duration);
                this.choice[id].interactable = true;
                this.choice[id].blocksRaycasts = true;
            }
            else
            {
                this.choice[i].DOFade(0f, _duration);
                this.choice[i].interactable = false;
                this.choice[i].blocksRaycasts = false;
            }
        }
    }
}

[System.Serializable]
public class HallSpeakText
{
    public char[] defaultTCWords = new char[4] { '永', '遠', '懷', '念' };
    public char[] defaultCNWords = new char[4] { '永', '远', '怀', '念' };
    public HallTitle[] halltc, hallsc, halleng;

    public void setDefaultPublic(int hallId)
    {
        switch (LoaderConfig.Instance.languageId)
        {
            case 0:
                this.setTC(this.defaultTCWords, hallId);
                break;
            case 1:
                this.setSC(this.defaultCNWords, hallId);
                break;
            case 2:
                //this.hallSpeakText.setTC(wordsArray);
                break;
        }
    }


    public void setTC(char[] _words, int hallId)
    {
        if(this.halltc.Length > hallId)
            this.halltc[hallId].setWords(_words);
    }

    public void setSC(char[] _words, int hallId)
    {
        if (this.hallsc.Length > hallId)
            this.hallsc[hallId].setWords(_words);
    }

    public void setEng(char[] _words, int hallId)
    {
        if (this.halleng.Length > hallId)
            this.halleng[hallId].setWords(_words);
    }

    public void reset(int hallId)
    {
        if (this.halltc.Length > hallId)
            this.halltc[hallId].resetWords();
        if (this.hallsc.Length > hallId)
            this.hallsc[hallId].resetWords();
        if (this.halleng.Length > hallId)
            this.halleng[hallId].resetWords();
    }
}

[System.Serializable]
public class HallTitle
{
    public string name;
    public Text[] words;
    public Text finalPageTitle;

    public void setWords(char[] _words)
    {
        string finalWords = "";
        for(int i=0; i< words.Length; i++)
        {
            Debug.Log(_words[i]);
            if(this.words[i] != null)
            {
                this.words[i].text = _words[i].ToString();
            }

            finalWords += _words[i];
        }

        if (this.finalPageTitle != null) this.finalPageTitle.text = finalWords;
    }

    public void resetWords()
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (this.words[i] != null)
            {
                this.words[i].text = "";
            }
        }

        if(this.finalPageTitle != null) this.finalPageTitle.text = "";
    }
}

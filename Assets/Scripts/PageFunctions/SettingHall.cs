using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public Page steps;
    public Color stepColor;
    public ProcessSteps processSteps;
    public Select selectFlower, selectFlowerStand, selectFlowerBasket, selectSpeakText, selectOfferFood;
    public Decoration flowers, flowerStands, flowerBaskets, offerFoods;
    public HallSpeakText hallSpeakText;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.selectFlower.init();
        this.selectFlowerStand.init();
        this.selectFlowerBasket.init();
        this.selectSpeakText.init();
        this.selectOfferFood.init();
        this.offerFoods.init();
    }


    public void setGameMode()
    {
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
                    this.flowers.init(2);
                    this.flowerStands.init(0);
                    this.flowerBaskets.init(2);

                    switch (LoaderConfig.Instance.languageId)
                    {
                        case 0:
                            this.hallSpeakText.setTC(new char[4]{'永', '遠', '懷', '念' });
                            break;
                        case 1:
                            this.hallSpeakText.setSC(new char[4] { '永', '远', '怀', '念' });
                            break;
                        case 2:
                            //this.hallSpeakText.setTC(wordsArray);
                            break;
                    }

                    this.steps.currentId = this.steps.pages.Length-1;
                }
                else
                {
                    this.processSteps.processSteps[i].SetActive(false);
                }
            }

            if (SendFeelings.Instance != null)
                SendFeelings.Instance.setGameMode(true);
        }
        else
        {
            Debug.Log("Detail Mode");
            for (int i = 0; i < this.processSteps.processSteps.Length; i++)
            {
                this.processSteps.processSteps[i].SetActive(true);
            }

            this.setStepFrame(0);
            this.steps.init();
            this.hallSpeakText.reset();
            this.flowers.init();
            this.flowerStands.init();
            this.flowerBaskets.init();

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
        this.steps.toNextPage(
            () => this.setStepFrame(this.steps.currentId),
            () => PageController.Instance.ChangePage(PageController.Instance.pageController.currentId + 1)
        );
    }

    public void skipStep()
    {
        this.changeStep();
        switch (this.steps.currentId)
        {
            case 1:
                this.SelectFlower(-1); // not select
                break;
            case 2:
                this.SelectFlowerStand(-1); // not select
                break;
            case 3:
                this.SelectFlowerBasket(-1); // not select
                break;
            case 4:
                this.SelectSpeakText(-1);// not select
                break;
            case 5:
                this.SelectOfferFood(-1);// not select
                break;
            case 6:
                //this.SelectFoodToBuddle(-1);// not select
                break;

        }
    }

    public void SelectFlower(int id)
    {
        this.selectFlower.set(id);
        this.flowers.set(id);
    }

    public void SelectFlowerStand(int id)
    {
        this.selectFlowerStand.set(id);
        this.flowerStands.set(id);
    }

    public void SelectFlowerBasket(int id)
    {
        this.selectFlowerBasket.set(id);
        this.flowerBaskets.set(id);
    }

    public void SelectSpeakText(int id)
    {
        if(id == -1)
            return;

        this.selectSpeakText.set(id);

        Text[] wordtexts = this.selectSpeakText.options[id].GetComponentsInChildren<Text>();

        for(int i=0 ;i < wordtexts.Length; i++)
        {
            if(wordtexts[i].name == "Text-TC" && LoaderConfig.Instance.languageId == 0)
            {
                char[] wordsArray = wordtexts[i].text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setTC(wordsArray);
            }
            else if (wordtexts[i].name == "Text-CN" && LoaderConfig.Instance.languageId == 1)
            {
                char[] wordsArray = wordtexts[i].text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setSC(wordsArray);
            }
            else if (wordtexts[i].name == "Text-Eng" && LoaderConfig.Instance.languageId == 2)
            {
                char[] wordsArray = wordtexts[i].text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setEng(wordsArray);
            }
        }

    }

    public void SelectOfferFood(int id)
    {
        this.selectOfferFood.set(id);
        this.offerFoods.set(id);
    }


    public void SelectSpeakTextSwitchPage(bool right)
    {
        this.selectSpeakText.changePage(right);
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
    public CanvasGroup[] choice;
    public int selected = -1;

    public void init(int defaultId = -1)
    {
        this.set(defaultId);
    }


    public void set(int id, float _duration = 0f)
    {
        this.selected = id;
        for (int i = 0; i < this.choice.Length; i++)
        {
            if (this.choice[i] != null)
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
}

[System.Serializable]
public class HallSpeakText
{
    public GameObject[] langs;
    public HallTitle tc, sc, eng;


    public void setTC(char[] _words)
    {
        this.tc.setWords(_words);
    }

    public void setSC(char[] _words)
    {
        this.sc.setWords(_words);
    }

    public void setEng(char[] _words)
    {
        this.eng.setWords(_words);
    }

    public void reset()
    {
        this.tc.resetWords();
        this.sc.resetWords();
        this.eng.resetWords();
    }
}

[System.Serializable]
public class HallTitle
{
    public Text[] words;
    public Text finalPageTitle;

    public void setWords(char[] _words)
    {
        string finalWords = "";
        for(int i=0; i< _words.Length; i++)
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

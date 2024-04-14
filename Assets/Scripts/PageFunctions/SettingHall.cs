using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingHall : MonoBehaviour
{
    public Page steps;
    public Color stepColor;
    public Image[] stepsHintFrame;
    public Select selectFlower, selectFlowerStand, selectFlowerBasket, selectSpeakText, selectOfferFood, selectFoodToBuddle;
    public Decoration flowers, flowerStands, flowerBaskets, offerFoods;
    public HallSpeakText hallSpeakText;
    // Start is called before the first frame update
    void Start()
    {
        this.setStepFrame(0);
        this.steps.init();
        this.selectFlower.init();
        this.selectFlowerStand.init();
        this.selectFlowerBasket.init();
        this.selectSpeakText.init();
        this.selectOfferFood.init();
        this.selectFoodToBuddle.init();
        this.hallSpeakText.reset();
        this.flowers.init();
        this.flowerStands.init();
        this.flowerBaskets.init();
        this.offerFoods.init();
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
                this.SelectFoodToBuddle(-1);// not select
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
        this.selectSpeakText.set(id);

        Text[] wordtexts = this.selectSpeakText.options[id].GetComponentsInChildren<Text>();

        for(int i=0 ;i < wordtexts.Length; i++)
        {
            if(wordtexts[i].name == "Text-TC" && PageController.Instance.languageId == 0)
            {
                char[] wordsArray = wordtexts[i].text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setTC(wordsArray);
            }
            else if (wordtexts[i].name == "Text-CN" && PageController.Instance.languageId == 1)
            {
                char[] wordsArray = wordtexts[i].text.ToCharArray();
                Debug.Log(wordsArray);
                this.hallSpeakText.setSC(wordsArray);
            }
            else if (wordtexts[i].name == "Text-Eng" && PageController.Instance.languageId == 2)
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

    public void SelectFoodToBuddle(int id)
    {
        this.selectFoodToBuddle.set(id);
    }


    public void SelectSpeakTextSwitchPage(bool right)
    {
        this.selectSpeakText.changePage(right);
    }

    void setStepFrame(int id)
    {
        for(int i = 0; i < this.stepsHintFrame.Length; i++)
        {
            if (this.stepsHintFrame[i] != null)
            {
                if (i == id)
                {
                    this.stepsHintFrame[id].DOFade(1f, 0f);
                    var childTexts = this.stepsHintFrame[id].GetComponentsInChildren<Text>();
                    for (int k=0; k< childTexts.Length; k++)
                    {
                        childTexts[k].color = stepColor;
                    }
                }
                else
                {
                    this.stepsHintFrame[i].DOFade(0f, 0f);
                    var childTexts = this.stepsHintFrame[i].GetComponentsInChildren<Text>();
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

    public void init()
    {
        this.set(this.selected);
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

    public void setWords(char[] _words)
    {
        for(int i=0; i< _words.Length; i++)
        {
            Debug.Log(_words[i]);
            if(this.words[i] != null)
            {
                this.words[i].text = _words[i].ToString();
            }
        }
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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingHall : MonoBehaviour
{
    public Page steps;
    public Image[] stepsHintFrame;
    public Select selectFlower, selectFlowerStand, selectFlowerBasket, selectSpeakText, selectOfferFood, selectFoodToBuddle;
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
    }

    public void SelectFlowerStand(int id)
    {
        this.selectFlowerStand.set(id);
    }

    public void SelectFlowerBasket(int id)
    {
        this.selectFlowerBasket.set(id);
    }

    public void SelectSpeakText(int id)
    {
        this.selectSpeakText.set(id);
    }

    public void SelectOfferFood(int id)
    {
        this.selectOfferFood.set(id);
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
                    this.stepsHintFrame[i].DOFade(1f, 0f);
                }
                else
                {
                    this.stepsHintFrame[i].DOFade(0f, 0f);
                }
            }
        }
    }
}

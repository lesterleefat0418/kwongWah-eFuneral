using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlowerOffer : MonoBehaviour
{
    public Select flowersOffer;
    public CanvasGroup animationFlower;
    private Image flower = null;
    public bool isAnimated = false;
    public SceneFlower[] scenesFlowers;
    // Start is called before the first frame update
    void Start()
    {
        this.flowersOffer.init();
        if(this.animationFlower != null) this.flower = this.animationFlower.GetComponent<Image>();

        for(int i= 0; i < this.scenesFlowers.Length; i++)
        {
            if (this.scenesFlowers[i] != null)
            {
                this.scenesFlowers[i].init();
            }
        }
    }

    public void showFlowerPannel(CanvasGroup cg = null)
    {
        SetUI.Set(cg, true);
    }

    public void closeFlowerPannel()
    {
        int hallId = SettingHall.Instance.selectedHallId;
        this.flowersOffer.set(-1, hallId);
        this.flower.sprite = null;
    }

    public void selectFlower(int id)
    {
        this.triggerAnimation(id);
    }

    void triggerAnimation(int id)
    {
        if(!isAnimated)
        {
            int hallId = SettingHall.Instance.selectedHallId;
            this.flowersOffer.set(id, hallId);
            this.flower.sprite = this.flowersOffer.btnImages[id].sprite;
            isAnimated = true;
            this.flower.transform.DOScale(1f, 0f);
            if (this.animationFlower != null) this.animationFlower.DOFade(1f, 0.5f).OnComplete(() => zooming(id));
        }
    }

    void zooming(int id)
    {
        this.flower.transform.DOScale(0f, 1f).OnComplete(()=> showDecoration(id));
    }

    void showDecoration(int id)
    {
        isAnimated = false;
        if (this.scenesFlowers.Length > SettingHall.Instance.selectedHallId)
        {
            if (this.scenesFlowers[SettingHall.Instance.selectedHallId] != null)
                this.scenesFlowers[SettingHall.Instance.selectedHallId].decoration[id].SetActive(true);
        }
    }


}

[System.Serializable]
public class SceneFlower
{
    public string flowerName;
    public List<GameObject> decoration = new List<GameObject>();

    public void init()
    {
        for(int i=0; i< this.decoration.Count;i++)
        {
            if (this.decoration[i] != null)
            {
                decoration[i].SetActive(false);
            }
        }
    }
}

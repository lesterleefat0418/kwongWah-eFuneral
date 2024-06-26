using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectFood : MonoBehaviour
{
    private Image select;
    public Image foodImage;
    private AspectRatioFitter aspectRatio;
    public bool selected = false;
    public GameObject[] scenesFood;
    // Start is called before the first frame update
    void Start()
    {
        this.select = this.GetComponent<Image>();
        this.select.DOFade(0f, 0f);
        if(this.foodImage != null) { 
            var width = this.foodImage.sprite.rect.width;
            var height = this.foodImage.sprite.rect.height;
            this.aspectRatio = this.foodImage.GetComponent<AspectRatioFitter>();
            this.aspectRatio.aspectRatio = width/height;
        }
        this.selected = false;
        this.setFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {  
        this.selected = !this.selected;
        setFood();
    }


    void setFood()
    {
        this.select.DOFade(this.selected ? 1f : 0f, 0f);
        if(this.scenesFood.Length > SettingHall.Instance.selectedHallId) { 
            if (this.scenesFood[SettingHall.Instance.selectedHallId] != null)
                this.scenesFood[SettingHall.Instance.selectedHallId].SetActive(this.selected);
        }
    }

    public void NotSelect()
    {
        this.selected = false;
        setFood();
    }
}

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectFood : MonoBehaviour
{
    private Image select;
    public Image foodImage;
    private AspectRatioFitter aspectRatio;
    public bool selected = false;
    public GameObject sceneFood;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        this.selected = !this.selected;
        this.select.DOFade(this.selected ? 1f : 0f, 0f);

        if(this.sceneFood != null)
            this.sceneFood.SetActive(this.selected);
    }

    public void NotSelect()
    {
        this.selected = false;
        this.select.DOFade(this.selected ? 1f : 0f, 0f);

        if (this.sceneFood != null)
            this.sceneFood.SetActive(this.selected);
    }
}

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectBurn : MonoBehaviour
{
    public CanvasGroup sceneburn;
    private Button burnBtn;
    // Start is called before the first frame update
    void Start()
    {
        if(this.sceneburn != null)
        {
            this.sceneburn.alpha = 0f;
        }

        this.burnBtn = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireBurn()
    {
        if (this.sceneburn != null && this.burnBtn.interactable)
        {
            this.sceneburn.DOFade(1f, 1f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.InOutBack);
            this.sceneburn.transform.DOScale(0.5f, 2f).SetEase(Ease.Linear);
            this.burnBtn.interactable = false;
        }
    }
}

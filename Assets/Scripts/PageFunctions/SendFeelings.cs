using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SendFeelings : MonoBehaviour
{
    public CanvasGroup feelingTag;
    public bool showFeelingBox = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFeelingTag()
    {
        this.showFeelingBox = !this.showFeelingBox;
        if (this.feelingTag != null) this.feelingTag.transform.DOLocalMove(new Vector3(0f, this.showFeelingBox ? -450f : -650f, 0f), 0.5f).SetEase(Ease.OutBack);
    }
}

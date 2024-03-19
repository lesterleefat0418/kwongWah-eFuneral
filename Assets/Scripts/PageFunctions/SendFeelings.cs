using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SendFeelings : MonoBehaviour
{
    public CanvasGroup feelingTag, feedbackTag, feedbackBtn;
    public bool showFeedbackBox = false;
    public bool showFeelingBox = false;
    private float originalY;
    // Start is called before the first frame update
    void Start()
    {
        if(this.feelingTag != null) this.originalY = this.feelingTag.transform.localPosition.y;
        if(this.feedbackBtn != null) this.feedbackBtn.alpha = 1f;
        SetUI.Run(this.feedbackTag, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFeedbackBoxTag()
    {
        this.showFeedbackBox = !this.showFeedbackBox;
        SetUI.Run(this.feedbackTag, this.showFeedbackBox);
        bool reviseStatus = !this.showFeedbackBox;
        SetUI.Run(this.feedbackBtn, reviseStatus);
    }

    public void showFeelingTag()
    {
        this.showFeelingBox = !this.showFeelingBox;
        if (this.feelingTag != null) this.feelingTag.transform.DOLocalMove(new Vector3(0f, this.showFeelingBox ? -450f : this.originalY, 0f), 0.5f).SetEase(Ease.OutBack);
    }
}


public static class SetUI
{
    public static void Run(CanvasGroup cg= null, bool status=false)
    {
        if(cg != null)
        {
            cg.DOFade(status? 1f: 0f, status? 0.5f : 0f);
            cg.blocksRaycasts = status;
            cg.interactable = status;
        }
    }
}
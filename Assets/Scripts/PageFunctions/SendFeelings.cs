using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SendFeelings : MonoBehaviour
{
    public static SendFeelings Instance = null;
    public CanvasGroup feelingTag, feedbackTag, feedbackBtn;
    public bool showFeedbackBox = false;
    public bool showFeelingBox = false;
    private float originalY;
    public FeedbackView feedbackView;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.feedbackView.Init();
        if (this.feelingTag != null) this.originalY = this.feelingTag.transform.localPosition.y;
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

[System.Serializable]
public class FeedbackView
{
    public ScrollRect scrollView;
    public GameObject page;
    public List<CanvasGroup> totalPages = null;

    public void Init()
    {
        this.totalPages = new List<CanvasGroup>();
        if(this.scrollView != null)
        {
            this.totalPages.Add(this.CurrentContent.GetComponent<CanvasGroup>());
        }
    }

    public RectTransform CurrentContent
    {
        get
        {
            return this.scrollView.content;
        }
    }

    public bool IsContentFull()
    {
        RectTransform contentTransform = this.CurrentContent;
        float contentHeight = contentTransform.rect.height;
        float viewHeight = this.scrollView.viewport.rect.height - (contentTransform.rect.height * 0.1f);
        return contentHeight >= viewHeight;
    }

    public void changePage(int toId)
    {
        for(int i=0; i< this.totalPages.Count; i++)
        {
            if(this.totalPages[i] != null)
            {
                if(i == toId)
                {
                    this.totalPages[toId].DOFade(1f, 0f);
                }
                else
                {
                    this.totalPages[i].DOFade(0f, 0f);
                }
            }
        }
    }

    public void AddComponent(GameObject newFeedback=null)
    {
        if (!IsContentFull())
        {
            if(newFeedback != null) 
                newFeedback.transform.SetParent(this.CurrentContent, false);
        }
        else
        {
            var newContent = GameObject.Instantiate(this.page, scrollView.viewport);
            newContent.name = "Page" + (this.totalPages.Count + 1);

            if (newFeedback != null)
                newFeedback.transform.SetParent(newContent.transform, false);

            if (this.scrollView != null)
            {
                this.totalPages.Add(newContent.GetComponent<CanvasGroup>());
            }
            //this.scrollView.content = newContent.GetComponent<RectTransform>();
        }
    }

}
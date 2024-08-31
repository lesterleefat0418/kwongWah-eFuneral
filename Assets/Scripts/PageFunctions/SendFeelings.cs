using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class SendFeelings : MonoBehaviour
{
    public static SendFeelings Instance = null;
    public CanvasGroup feelingTag, feedbackTag, feedbackBtn, giveFlowerBtn, giveMoneyBtn;
    public bool showFeedbackBox = false;
    public bool showFeelingBox = false;
    private float originalY;
    public FeedbackView feedbackView;
    public CanvasGroup inputField, drawingPanel, audioPanel;
    public GameObject message, recordResult;
    public CanvasGroup[] processes;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void setGameMode(bool isPublic)
    {
        // SetUI.Run(this.feelingTag, isPublic ? false : true);
        // SetUI.Run(this.feedbackBtn, isPublic ? false : true);

         SetUI.Run(this.feelingTag, true);
         SetUI.Run(this.feedbackBtn, true);

        if (LoaderConfig.Instance.religionId < 4) { 
            SetUI.Run(this.giveFlowerBtn, isPublic ? false : true);
        }
        else
        {
            if (LoaderConfig.Instance.religionId != 5)
                SetUI.Run(this.giveFlowerBtn, false);
            else
                SetUI.Run(this.giveFlowerBtn, true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.feedbackView.Init();
        if (this.feelingTag != null) this.originalY = this.feelingTag.transform.localPosition.y;
        if(this.feedbackBtn != null) this.feedbackBtn.alpha = 1f;
        SetUI.Run(this.feedbackTag, false);
        this.showInputType(-1);

        if (this.inputField != null) { 
            var input = this.inputField.GetComponent<InputField>();
            if (input != null)
            {
                EventTrigger eventTrigger = input.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry = new EventTrigger.Entry();
                clickEntry.eventID = EventTriggerType.PointerClick;
                UnityAction<BaseEventData> clickAction = new UnityAction<BaseEventData>(ShowTibpad);
                clickEntry.callback.AddListener(clickAction);
                eventTrigger.triggers.Add(clickEntry);
            }
        }
        this.resetAllProcesses();
    }


    public void resetAllProcesses(int apartId = -1)
    {
        if(this.processes == null)
            return;

        for(int i=0;i< this.processes.Length; i++)
        {
            if(i == apartId) { 
                SetUI.Run(this.processes[apartId], true);
            }
            else
            {
                SetUI.Run(this.processes[i], false);
            }
        }

    }

    private void ShowTibpad(BaseEventData eventData)
    {
        if (VirtualKeyboard.Instance == null)
            return;

        VirtualKeyboard.Instance.ShowTouchKeyboard();
        this.MoveTag(true);
    }

    public void MoveTag(bool up)
    {
        if(this.feedbackTag != null) this.feedbackTag.transform.localPosition = new Vector2(-732.39f, up ? 220f : 0f);
    }

    public void showFeedbackBoxTag()
    {
        this.showFeedbackBox = !this.showFeedbackBox;
        showFeedBackFrame();
    }

    public void closeFeedbackBoxTag()
    {
        this.showFeedbackBox = false;
        showFeedBackFrame();
    }

    void showFeedBackFrame()
    {
        SetUI.Run(this.feedbackTag, this.showFeedbackBox);
        bool reviseStatus = !this.showFeedbackBox;
        SetUI.Run(this.feedbackBtn, reviseStatus);
        if (VirtualKeyboard.Instance != null) VirtualKeyboard.Instance.HideTouchKeyboard();
        this.MoveTag(false);
    }
    public void showFeelingTag()
    {
        this.showFeelingBox = !this.showFeelingBox;
        if (this.feelingTag != null) this.feelingTag.transform.DOLocalMove(new Vector3(0f, this.showFeelingBox ? -475f : this.originalY, 0f), 0.5f).SetEase(Ease.OutBack);
    }

    public void closeFeelingTag()
    {
        this.showFeelingBox = false;
        if (this.feelingTag != null) this.feelingTag.transform.DOLocalMove(new Vector3(0f, this.showFeelingBox ? -475f : this.originalY, 0f), 0.5f).SetEase(Ease.OutBack);
    }

    public void showInputType(int type)
    {
        if (!this.showFeedbackBox && type != -1)
        {
            this.showFeedbackBox = true;
            showFeedBackFrame();
        }
        switch (type)
        {
            case -1:
                Debug.Log("Default settings");
                SetUI.Run(this.inputField, false);
                SetUI.Run(this.drawingPanel, true);
                SetUI.Run(this.audioPanel, false);
                break;
            case 0:
                Debug.Log("Inputfield");
                SetUI.Run(this.inputField, true);
                SetUI.Run(this.drawingPanel, false);
                SetUI.Run(this.audioPanel, false);
                break;
            case 1:
                Debug.Log("DrawingPanel");
                SetUI.Run(this.inputField, false);
                SetUI.Run(this.drawingPanel, true);
                SetUI.Run(this.audioPanel, false);
                if (VirtualKeyboard.Instance != null) VirtualKeyboard.Instance.HideTouchKeyboard();
                this.MoveTag(false);
                break;
            case 2:
                Debug.Log("Audio Record");
                SetUI.Run(this.inputField, false);
                SetUI.Run(this.drawingPanel, false);
                SetUI.Run(this.audioPanel, true);
                VirtualKeyboard.Instance.HideTouchKeyboard();
                if (VirtualKeyboard.Instance != null) VirtualKeyboard.Instance.HideTouchKeyboard();
                this.MoveTag(false);
                break;
        }
    }


    

    public void sendInputField()
    {
        if(this.inputField.GetComponent<InputField>() == null || this.message == null)
            return;

        var newMessage = Instantiate(this.message);
        newMessage.name = "message";
        var txt = newMessage.GetComponent<Text>();
        var inputTxt = this.inputField.GetComponent<InputField>();

        Font ft = null;
        switch (LoaderConfig.Instance.SelectedLanguageId)
        {
            case 0:
                ft = LoaderConfig.Instance.tc;
                break;
            case 1:
                ft = LoaderConfig.Instance.sc;
                break;
            case 2:
                ft = LoaderConfig.Instance.tc;
                break;
        }

        txt.font = ft;
        //txt.text = ChineseConvertTool.ToSimplified(inputTxt.text);
        txt.text = inputTxt.text;
        txt.color = Color.white;
        inputTxt.text = "";
        this.feedbackView.AddComponent(newMessage);
    }

    public void inputFieldToSimple()
    {
        if(LoaderConfig.Instance.SelectedLanguageId == 1) { 
            if(this.inputField.GetComponent<InputField>() == null)
                return;
            var input = this.inputField.GetComponent<InputField>();
            input.text = ChineseConvertTool.ToSimplified(input.text);
        }

    }

    public bool hasNewRecording = false;

    public bool HasNewRecording()
    {
        if (hasNewRecording)
        {
            // Reset the flag after checking
            hasNewRecording = false;
            return true;
        }
        return false;
    }

    public void sendAudioRecord()
    {
        if (this.recordResult == null)
            return;

        if(this.HasNewRecording())
        {
            var newAR = Instantiate(this.recordResult);
            newAR.name = "audio";
            this.feedbackView.AddComponent(newAR);
        }
        else
        {
            Debug.Log("no record audio");
        }
    }
}


public static class SetUI
{
    public static void Set(CanvasGroup cg = null, bool status = false, float targetValue=1f, float duration = 0f)
    {
        if (cg != null)
        {
            cg.DOFade(targetValue, duration);
            cg.blocksRaycasts = status;
            cg.interactable = status;
        }
    }

    public static void Run(CanvasGroup cg= null, bool status=false, float duration = 0f)
    {
        if(cg != null)
        {
            cg.DOFade(status? 1f: 0f, status? duration : 0f);
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
    public bool usePagination = false;

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
        if(usePagination) { 
            if (!IsContentFull())
            {
                if(newFeedback != null) 
                    newFeedback.transform.SetParent(this.CurrentContent, false);
            }
            else
            {
                var newContent = GameObject.Instantiate(this.page, scrollView.viewport);
                newContent.name = "Page" + (this.totalPages.Count + 1);

                if (newFeedback != null) {
                    newFeedback.transform.SetAsFirstSibling();
                    newFeedback.transform.SetParent(newContent.transform, false);
                }

                if (this.scrollView != null)
                {
                    this.totalPages.Add(newContent.GetComponent<CanvasGroup>());
                }
                //this.scrollView.content = newContent.GetComponent<RectTransform>();
            }
        }
        else
        {
            if (newFeedback != null) {
                newFeedback.transform.SetParent(this.CurrentContent, false);
                newFeedback.transform.SetAsFirstSibling();
            }

            float totalHeight = 0f;
            foreach (Transform child in this.CurrentContent.transform)
            {
                totalHeight += child.GetComponent<RectTransform>().sizeDelta.y;
            }
            this.CurrentContent.sizeDelta = new Vector2(this.CurrentContent.sizeDelta.x, totalHeight);
        }
    }

}
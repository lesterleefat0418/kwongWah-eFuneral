using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class Page
{
    public Group[] pages;
    public int currentId;
    public bool isAnimated = false;
    public Idling[] pageIdling;

    public void Init(float[] _pageTotalTime = null)
    {
        this.setPage(this.currentId);
        this.isAnimated = false;

        for (int i = 0; i < this.pageIdling.Length; i++)
        {
            if (this.pageIdling[i] != null)
            {
                this.pageIdling[i].init();
            }
        }
    }

    public void init(float[] _pageTotalTime = null, int defaultId = 0)
    {
        this.currentId = defaultId;
        this.setPage(this.currentId);
        this.isAnimated = false;

        for(int i=0; i < this.pageIdling.Length; i++)
        {
            if(this.pageIdling[i] != null)
            {
                this.pageIdling[i].init();
            }
        }
    }

    public void controlPage(int id, bool status)
    {
        if (pages[id] != null)
        {
            if (status == true)
            {
                //pages[id].DOFade(1f, 1f).OnComplete(() => this.isAnimated = false);
                pages[id].showAnimation(true, 1f, ()=> this.isAnimated = false);
            }
            else
            {
                //pages[id].DOFade(0f, 0.5f).OnComplete(() => this.isAnimated = false);
                pages[id].showAnimation(false, 0.5f, () => this.isAnimated = false);
            }
        }
    }

    public void setPage(int toPageId, Action tasks = null)
    {
        if (!this.isAnimated)
        {
            tasks?.Invoke();
            this.isAnimated = true;
            this.currentId = toPageId;
            for (int i = 0; i < pages.Length; i++)
            {
                if (this.pages[i] != null)
                {
                    if (i == toPageId)
                    {
                        this.pages[toPageId].showAnimation(true, 0.5f, () => reset(toPageId));
                    }
                    else
                    {
                        this.pages[i].showAnimation(false, 0f);
                        if (this.pageIdling.Length > 0) 
                            if (this.pageIdling[i].countDown != null) this.pageIdling[i].countDown.init();
                    }
                }
            }
        }
    }

    public void toNextPage(Action stepEvent = null, Action completedEvent = null)
    {
        if(this.isAnimated)
            return;
        else
        {
            if (this.currentId < this.pages.Length - 1)
            {
                this.currentId += 1;
                this.setPage(this.currentId);
                stepEvent.Invoke();
            }
            else
            {
                completedEvent.Invoke();
            }
        }
    }

    public void toPreviousPage(Action stepEvent = null, Action completedEvent = null)
    {
        if (this.isAnimated)
            return;
        else
        {
            if (this.currentId > 0)
            {
                this.currentId -= 1;
                this.setPage(this.currentId);
                stepEvent.Invoke();
            }
            else
            {
                completedEvent.Invoke();
            }
        }
    }



    void reset(int id)
    {
        this.isAnimated = false;
        this.pageIdling[id].init();
    }

    public void resetIdling()
    {
        this.pageIdling[this.currentId].init();
        this.reset(this.currentId);
    }

}


[Serializable]
public class Idling
{
    public string pageName;
    public CountDownTimer countDown;

    public void init()
    {
        this.countDown?.init();
        this.countDown?.showTimer();
    }
}


[Serializable]
public class Frame
{
    public Group[] selected;
    public Group[] webcamFrames, previewFrames, stickerFrames;
    public int selectedId = 0;
    public void init()
    {
        this.selectedId = 0;
        this.changeFrameById(0);
    }

    public void changeFrameById(int id)
    {
        for(int i=0; i< this.selected.Length; i++)
        {
            if(this.selected[i] != null && this.previewFrames[i] != null)
            {
                if (i == id)
                {
                    this.selectedId = id;
                    this.selected[id].show(true);
                    this.webcamFrames[id].show(true);
                    this.previewFrames[id].show(true);
                    this.stickerFrames[id].show(true);
                }
                else
                {
                    this.selected[i].show(false);
                    this.webcamFrames[i].show(false);
                    this.previewFrames[i].show(false);
                    this.stickerFrames[i].show(false);
                }
            }
        }
    }
}


[Serializable]
public class Group
{
    public string name;
    public CanvasGroup group;

    public void showAnimation(bool status, float duration = 0.5f, TweenCallback callback=null)
    {
        if(this.group != null)
        {
            this.group.DOFade(status ? 1f: 0f, status ? duration: 0f).OnComplete(callback);
            this.group.interactable = status;
            this.group.blocksRaycasts = status;
        } 
    }

    public void show(bool status)
    {
        if(this.group != null) { 
            this.group.alpha = status ? 1f:0f;
            this.group.interactable = status;
            this.group.blocksRaycasts = status;
        }
    }
}

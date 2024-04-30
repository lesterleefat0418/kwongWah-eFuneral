using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource = null;
    public bool loop = false;
    public CanvasGroup playBtn, pauseBtn;
    public Button loopBtn;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        SetUI.Set(this.playBtn, true, 1f);
        SetUI.Set(this.pauseBtn, false, 0f);
        this.loop = false;
        if (this.loopBtn != null)
            this.loopBtn.GetComponent<Image>().DOColor(this.loop ? Color.gray : Color.white, 0f);
    }


    public void Play(int _clipId=0)
    {
        SetUI.Set(this.playBtn, false, 0f);
        SetUI.Set(this.pauseBtn, true, 1f);
        if (this.audioSource != null && !this.audioSource.isPlaying && this.clips.Length > 0)
        {
            this.audioSource.clip = this.clips[_clipId];
            this.audioSource.loop = this.loop;
            this.audioSource.Play();
        }
    }

    public void Stop()
    {
        SetUI.Set(this.playBtn, true, 1f);
        SetUI.Set(this.pauseBtn, false, 0f);
        this.loop = false;
        if (this.loopBtn != null)
            this.loopBtn.GetComponent<Image>().DOColor(this.loop ? Color.gray : Color.white, 0f);
        if (this.audioSource != null && this.audioSource.isPlaying)
        {
            this.audioSource.Stop();
        }
    }

    public void loopPlay(int _clipId=0)
    {
        this.loop = !this.loop;
        if(this.loopBtn != null) 
            this.loopBtn.GetComponent<Image>().DOColor(this.loop ? Color.gray : Color.white, 0f);
        if (this.audioSource !=null) this.audioSource.loop = this.loop;
        this.Play(_clipId);
    }

}

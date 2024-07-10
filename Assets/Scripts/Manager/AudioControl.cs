using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour
{
    public int activatedPageId = 0;
    public AudioClip[] clips;
    private AudioSource audioSource = null;
    public bool playOnAwake = false;
    public bool loop = false;
    public CanvasGroup playBtn, pauseBtn;
    public bool isPlaying = false;
    public Button loopBtn;
    public Button[] musicBtns;
    public int selectedClipId = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        SetUI.Set(this.playBtn, true, 1f);
        SetUI.Set(this.pauseBtn, false, 0f);

        if(this.loopBtn != null) this.loopBtn.GetComponent<Image>().DOColor(Color.white, 0f);

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }


    private void Update()
    {
        if(PageController.Instance.pageController.currentId == activatedPageId)
        {
            if (this.playOnAwake)
            {
                this.Play();
                this.playOnAwake = false;
            }
        }
    }

    public void PlayOnce(int _clipId)
    {
        SetUI.Set(this.playBtn, false, 0f);
        SetUI.Set(this.pauseBtn, true, 1f);
        if (this.audioSource != null && this.clips.Length > 0)
        {
            this.loop = false;
            this.selectedClipId = _clipId;
            this.audioSource.clip = this.clips[this.selectedClipId];
            this.audioSource.loop = this.loop;
            this.audioSource.Play();

        }
        this.isPlaying = true;

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null)
            {
                if (i == this.selectedClipId)
                {
                    this.musicBtns[this.selectedClipId].image.DOColor(Color.gray, 0f);
                }
                else
                {
                    this.musicBtns[i].image.DOColor(Color.white, 0f);
                }
            }
        }
    }

    public void Play()
    {
        SetUI.Set(this.playBtn, false, 0f);
        SetUI.Set(this.pauseBtn, true, 1f);
        if (this.audioSource != null && this.clips.Length > 0 && !this.audioSource.isPlaying)
        {
            this.audioSource.clip = this.clips[this.selectedClipId];
            this.audioSource.loop = this.loop;
            this.audioSource.Play();

        }
        this.isPlaying = true;

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null)
            {
                if (i == this.selectedClipId)
                {
                    this.musicBtns[this.selectedClipId].image.DOColor(Color.gray, 0f);
                }
                else
                {
                    this.musicBtns[i].image.DOColor(Color.white, 0f);
                }
            }
        }
    }

    public void changeAudio(int _clipId = 0)
    {
        if (this.audioSource != null && this.clips.Length > 0)
        {
            this.selectedClipId = _clipId;
            this.audioSource.clip = this.clips[_clipId];
            this.audioSource.loop = this.loop;
            this.audioSource.Play();

        }

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null)
            {
                if (i == this.selectedClipId)
                {
                    this.musicBtns[this.selectedClipId].image.DOColor(Color.gray, 0f);
                }
                else
                {
                    this.musicBtns[i].image.DOColor(Color.white, 0f);
                }
            }
        }
    }

    public void Stop()
    {
        SetUI.Set(this.playBtn, true, 1f);
        SetUI.Set(this.pauseBtn, false, 0f);
        this.loop = false;
        if (this.audioSource != null)
        {
            this.audioSource.Stop();
        }
        this.isPlaying = false;
        if (this.loopBtn != null) this.loopBtn.GetComponent<Image>().DOColor(this.loop ? Color.gray : Color.white, 0f);

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }

    public void loopPlay(int _clipId=0)
    {
        this.loop = true;
        if (this.audioSource !=null) this.audioSource.loop = this.loop;
        this.selectedClipId = _clipId;
        this.Play();
        if (this.loopBtn != null) this.loopBtn.GetComponent<Image>().DOColor(this.loop? Color.gray : Color.white, 0f);
    }

}

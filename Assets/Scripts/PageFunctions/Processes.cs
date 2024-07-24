using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Processes : MonoBehaviour
{
    public int musicTypeId = 2;
    public AudioClip[] processAudio_ch, processAudio_cn, processAudio_eng;
    public AudioClip[] audioMusics_ch, audioMusics_cn, audioMusics_eng;
    public Button[] musicBtns;
    AudioSource audioPlayer;
    public AudioControl bgmAudio;
    // Start is called before the first frame update
    void Start()
    {
        this.audioPlayer = GetComponent<AudioSource>();

        if(this.musicBtns == null) return;
        for(int i=0; i< this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }

    public void stopType(int id)
    {
        if(id == this.musicTypeId || id == -1) this.audioPlayer.Stop();
        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }

    public void playType(int id)
    {
        if(LoaderConfig.Instance == null) return;
        if (this.bgmAudio != null && !this.bgmAudio.isPlaying && id != this.musicTypeId) this.bgmAudio.Play();
        switch (LoaderConfig.Instance.SelectedLanguageId)
        {
            case 0:
                this.audioPlayer.clip = this.processAudio_ch[id];
                break;
            case 1:
                this.audioPlayer.clip = this.processAudio_cn[id];
                break;
            case 2:
                this.audioPlayer.clip = this.processAudio_eng[id];
                break;
        }

        if (this.audioPlayer.clip != null) 
            this.audioPlayer.Play();
        else
            this.audioPlayer.Stop();

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }

    public void playMusics(int id)
    {
        if (this.bgmAudio != null && this.bgmAudio.isPlaying) this.bgmAudio.Stop();
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null)
            {
                if(i == id) { 
                    this.musicBtns[id].image.DOColor(Color.gray, 0f);
                }
                else { 
                    this.musicBtns[i].image.DOColor(Color.white, 0f);
                }
            }
        }


        int langId = LoaderConfig.Instance.SelectedLanguageId;

        switch (langId)
        {
            case 0:
                if (this.audioMusics_ch[id] != null)
                {
                    this.audioPlayer.clip = this.audioMusics_ch[id];
                }
                break;
            case 1:
                if (this.audioMusics_cn[id] != null)
                {
                    this.audioPlayer.clip = this.audioMusics_cn[id];
                }
                break;
            case 2:
                if (this.audioMusics_eng[id] != null)
                {
                    this.audioPlayer.clip = this.audioMusics_eng[id];
                }
                break;
        }

        this.audioPlayer.Play();

    }

    public void close()
    {
        this.audioPlayer.Stop();
        if (this.bgmAudio != null && !this.bgmAudio.isPlaying && PageController.Instance.pageController.currentId < 5) this.bgmAudio.Play();
        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }
}

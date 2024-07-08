using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Processes : MonoBehaviour
{
    public AudioClip[] processAudio_ch, processAudio_cn, processAudio_eng;
    public AudioClip[] audioMusics;
    public Button[] musicBtns;
    AudioSource audioPlayer;
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

    
    public void playType(int id)
    {
        if(LoaderConfig.Instance == null) return;

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

        if (this.audioMusics[id] != null)
        {
            this.audioPlayer.clip = this.audioMusics[id];
            this.audioPlayer.Play();
        }
    }

    public void close()
    {
        this.audioPlayer.Stop();

        if (this.musicBtns == null) return;
        for (int i = 0; i < this.musicBtns.Length; i++)
        {
            if (this.musicBtns[i] != null) this.musicBtns[i].image.DOColor(Color.white, 0f);
        }
    }
}

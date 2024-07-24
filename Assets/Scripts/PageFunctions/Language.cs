using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Language
{
    public GameObject[] TC;
    public GameObject[] CN;
    public GameObject[] ENG;

    public void setLang(int lang)
    {
        for (int i = 0; i < this.TC.Length; i++)
        {
            if (this.TC[i] != null)
            {
                this.TC[i].SetActive(lang == 0 ? true : false);
            }
        }

        for (int i = 0; i < this.CN.Length; i++)
        {
            if (this.CN[i] != null)
            {
                this.CN[i].SetActive(lang == 1 ? true : false);
            }
        }

        for (int i = 0; i < this.ENG.Length; i++)
        {
            if (this.ENG[i] != null)
            {
                this.ENG[i].SetActive(lang == 2 ? true : false);
            }
        }
    }

    public void setTC()
    {
        for(int i=0; i< this.TC.Length; i++)
        {
            if(this.TC[i] != null)
            {
                this.TC[i].SetActive(true);
            }
        }

        for (int i = 0; i < this.CN.Length; i++)
        {
            if (this.CN[i] != null)
            {
                this.CN[i].SetActive(false);
            }
        }

        for (int i = 0; i < this.ENG.Length; i++)
        {
            if (this.ENG[i] != null)
            {
                this.ENG[i].SetActive(false);
            }
        }
    }

    public void setCN()
    {
        for (int i = 0; i < this.TC.Length; i++)
        {
            if (this.TC[i] != null)
            {
                this.TC[i].SetActive(false);
                this.CN[i].SetActive(true);
                this.ENG[i].SetActive(false);
            }
        }
    }

    public void setENG()
    {
        for (int i = 0; i < this.TC.Length; i++)
        {
            if (this.TC[i] != null)
            {
                this.TC[i].SetActive(false);
                this.CN[i].SetActive(false);
                this.ENG[i].SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Language
{
    public GameObject[] TC;
    public GameObject[] CN;
    public GameObject[] ENG;


    public void setTC()
    {
        for(int i=0; i< this.TC.Length; i++)
        {
            if(this.TC[i] != null)
            {
                this.TC[i].SetActive(true);
                this.CN[i].SetActive(false);
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

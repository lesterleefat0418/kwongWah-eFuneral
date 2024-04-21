using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageUI : MonoBehaviour
{
    public Font tc, cn;
    public Text text;
    public string tcText, cnText, engText;
    public int chineseFontSize, englishFontSize;

    public void setLang()
    {
        switch (LoaderConfig.Instance.SelectedLanguageId)
        {
            case 0:
                if(this.text != null)
                {
                    this.text.font = tc;
                    this.text.text = tcText;
                    this.text.fontSize = chineseFontSize;
                }
                break;
            case 1:
                if (this.text != null)
                {
                    this.text.font = cn;
                    this.text.text = cnText;
                    this.text.fontSize = chineseFontSize;
                }
                break;
            case 2:
                if (this.text != null)
                {
                    this.text.font = tc;
                    this.text.text = engText;
                    this.text.fontSize = englishFontSize;
                }
                break;
        }
    }
}

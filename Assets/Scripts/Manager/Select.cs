using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class HallSelection
{
    public int selected = 1;
    public Image[] options;
    public Page page;

    public void init()
    {
        this.set(this.selected);
        this.page.init();
    }

    public void set(int id)
    {
        for (int i = 0; i < this.options.Length; i++)
        {
            if (i == id)
            {
                this.options[id].DOFade(1f, 0f);
                this.selected = id;
            }
            else
            {
                this.options[i].DOFade(0f, 0f);
            }

        }
    }
}



[System.Serializable]
public class Select
{
    public string stepName;
    public int selected = 0;
    public Image[] options, btnImages;
    public Decoration[] decoration;
    public Page page;
    private Color32 originalColor;
    // Start is called before the first frame update

    public void init()
    {
        this.set(this.selected, 1);
        this.page.init();

        this.originalColor = new Color32(235, 219, 174, 255);
    }

    public void reset()
    {
        this.set(-1, 1);
        this.page.init();
    }

    public void set(int id, int hallType)
    {
        for (int i = 0; i < this.options.Length; i++)
        {
            if (i == id)
            {
                if(this.options[id] != null)
                {
                    this.options[id].DOFade(1f, 0f);
                    if(this.options[id].GetComponent<LanguageUI>() != null)
                    {
                        this.options[id].GetComponent<LanguageUI>().text.color = Color.white;
                    }
                }
                if (this.decoration.Length > 0) this.decoration[hallType].set(id);
                this.selected = id;
            }
            else
            {
                if (this.options[i] != null)
                {
                    if (this.options[i].GetComponent<LanguageUI>() != null)
                    {
                        this.options[i].GetComponent<LanguageUI>().text.color = this.originalColor;
                    }
                    this.options[i].DOFade(0f, 0f);
                }
                if(this.decoration.Length > 0) this.decoration[hallType].set(id);
            }

        }
    }

    public void changePage(bool right)
    {
        if (right)
        {
            if (this.page.currentId < this.page.pages.Length - 1) {
                this.page.currentId += 1;
            }
        }
        else
        {
            if (this.page.currentId > 0)
            {
                this.page.currentId -= 1;
            }
        }

        this.page.setPage(this.page.currentId);
    }
}

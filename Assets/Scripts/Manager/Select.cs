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
    public Image[] options;
    public Decoration[] decoration;
    public Page page;
    // Start is called before the first frame update

    public void init()
    {
        this.set(this.selected, 1);
        this.page.init();
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
                this.options[id].DOFade(1f, 0f);
                if (this.decoration.Length > 0) this.decoration[hallType].set(id);
                this.selected = id;
            }
            else
            {
                this.options[i].DOFade(0f, 0f);
                if(this.decoration.Length > 0) this.decoration[hallType].set(id);
            }

        }
    }

    public void changePage(bool right)
    {
        if (right)
        {
            if(this.page.currentId < this.page.pages.Length - 1)
                this.page.currentId += 1;
        }
        else
        {
            if(this.page.currentId > 0)
                this.page.currentId -= 1;
        }

        this.page.setPage(this.page.currentId);
    }
}

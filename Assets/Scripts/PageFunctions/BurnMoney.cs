using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Windows.Forms;

public class BurnMoney : MonoBehaviour
{
    public CanvasGroup[] burner;
    public GameObject fireParticle;
    public Transform money;
    public bool isAnimated = false;
    public GameObject loopBtn;
    public bool isLoop = false;
    // Start is called before the first frame update
    void Start()
    {
        this.resetPaper();
        if(this.loopBtn != null) this.loopBtn.GetComponent<Image>().DOFade(1f, 0f);
    }

    public void setLoop()
    {
        this.isLoop = !this.isLoop;
        if (this.loopBtn != null) this.loopBtn.GetComponent<Image>().DOFade(this.isLoop? 0.5f : 1f, 0f);
        burnPaper();
    }

    public void burnPaper(float _delay = 0f)
    {
        if (this.money != null && !this.isAnimated)
        {
            this.isAnimated = true;
            this.money.DOLocalMove(new Vector2(0f, 0f), 1f).SetDelay(_delay).SetEase(Ease.InOutBack).OnComplete(()=> resetPaper());
        }
    }

    void resetPaper()
    {
        this.money.DOLocalMove(new Vector2(0f, 130f), 0f).OnComplete(()=> loopPlay());
    }

    void loopPlay()
    {
        isAnimated = false;
        if (this.isLoop)
        {
            burnPaper(0.5f);
        }
    }

    public void set(int id)
    {
        if(id == 0)
        {
            SetUI.Set(this.burner[0], true);
            SetUI.Set(this.burner[1], false, 0f);
            if (this.fireParticle != null)
            {
                this.fireParticle.SetActive(false);
            }
        }
        else
        {
            SetUI.Set(this.burner[1], true);
            SetUI.Set(this.burner[0], false, 0f);
            if (this.fireParticle != null)
            {
                this.fireParticle.SetActive(true);
            }
        }

    }
}

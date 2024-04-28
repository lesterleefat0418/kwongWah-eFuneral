using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurnMoney : MonoBehaviour
{
    public CanvasGroup[] burner;
    public GameObject fireParticle;
    public Transform money;
    public bool isAnimated = false;
    // Start is called before the first frame update
    void Start()
    {
        this.resetPaper();
    }

    public void burnPaper()
    {
        if (this.money != null && !this.isAnimated)
        {
            this.isAnimated = true;
            this.money.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.InOutBack).OnComplete(()=> resetPaper());
        }
    }

    void resetPaper()
    {
        isAnimated = false;
        this.money.DOLocalMove(new Vector2(0f, 130f), 0f);
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

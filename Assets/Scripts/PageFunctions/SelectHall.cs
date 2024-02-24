using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectHall : MonoBehaviour
{
    public int selectedHall = 0;
    public Image[] hallSelected;
    // Start is called before the first frame update
    void Start()
    {
        //init
        this.reset();
    }

    public void reset()
    {
        this.Select(1);
    }

    public void Select(int id)
    {
        for(int i=0; i< this.hallSelected.Length; i++)
        {
            if(this.hallSelected[i] != null) { 
                if(i == id)
                {
                    this.hallSelected[i].DOFade(1f, 0f);
                    this.selectedHall = i;
                }
                else
                {
                    this.hallSelected[i].DOFade(0f, 0f);
                }
            }
        }
    }

}

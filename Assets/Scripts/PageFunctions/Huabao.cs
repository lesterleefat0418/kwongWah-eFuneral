using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huabao : MonoBehaviour
{
    public static Huabao Instance =null;
    public bool allowAutoBurn = false;
    public float count = 0f;
    public float delayTime = 3f;
    public Drag[] burnItemsSkip, burnItems;
    public int burnId = 0;
    public CanvasGroup[] loopDropUI, manualDropUI, hints;
    public GameObject fireParticles;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.resetAutoBurnTime();
        this.setHuaBao(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.allowAutoBurn)
        {
            if(this.burnId < this.dragItems.Length)
            {
                if (this.count > 0f)
                {
                    this.count -= Time.deltaTime;
                }
                else
                {
                    if (this.dragItems[this.burnId].enabled)
                        this.dragItems[this.burnId].fireBurn();

                    this.resetAutoBurnTime();
                    this.burnId += 1;
                }
            }
            else
            {
                Debug.Log("Restart the loop auto burn");
                //this.allowAutoBurn = false;
                this.burnId = 0;
            }          
        }
    }

    public void setHuaBao(bool status)
    {
        this.allowAutoBurn = status;
        if (this.fireParticles != null) this.fireParticles.SetActive(status);

        if (status)
        {
            this.burnId = 0;
        }

        for (int i=0; i<this.loopDropUI.Length; i++)
        {
            SetUI.Set(this.loopDropUI[i], !status, status? 0.5f: 1f);
            SetUI.Set(this.manualDropUI[i], status, status ? 1f : 0.5f);
            SetUI.Set(this.hints[i], status, status? 1f: 0f);
        }
    }

    public void resetAutoBurnTime()
    {
        this.count = this.delayTime;
    }

    Drag[] dragItems
    {
        get
        {
            return LoaderConfig.Instance.skipToHuabaoStage ? this.burnItemsSkip : burnItems;
        }
    }
}

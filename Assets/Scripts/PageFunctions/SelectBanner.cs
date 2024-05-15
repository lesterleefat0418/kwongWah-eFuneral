using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class SelectBanner : MonoBehaviour
{
    public Select bannerBtns;
    public List<Banner> scenesBanner, goodByeBanner;

    // Update is called once per frame
    void Start()
    {
        this.bannerBtns.init();

        if(LoaderConfig.Instance.configData.isLogined)
            this.setBanner(-1);
        else
            this.setBanner(0);
    }

    public void Selected(int id)
    {
        this.bannerBtns.set(id, -1);
        this.setBanner(id);
    }



    void setBanner(int id)
    {
        for(int i=0; i< this.scenesBanner.Count; i++)
        {
            if (this.scenesBanner[i] != null)
            {
                if(id == i)
                {
                    if (this.scenesBanner[id].scenesBanner[LoaderConfig.Instance.SelectedLanguageId] != null)
                        this.scenesBanner[id].scenesBanner[LoaderConfig.Instance.SelectedLanguageId].DOFade(1f, 0f);

                    if (this.goodByeBanner[id].scenesBanner[LoaderConfig.Instance.SelectedLanguageId] != null)
                        this.goodByeBanner[id].scenesBanner[LoaderConfig.Instance.SelectedLanguageId].DOFade(1f, 0f);
                }
                else
                {
                    if (this.scenesBanner[i].scenesBanner[LoaderConfig.Instance.SelectedLanguageId] != null)
                        this.scenesBanner[i].scenesBanner[LoaderConfig.Instance.SelectedLanguageId].DOFade(0f, 0f);

                    if (this.goodByeBanner[i].scenesBanner[LoaderConfig.Instance.SelectedLanguageId] != null)
                        this.goodByeBanner[i].scenesBanner[LoaderConfig.Instance.SelectedLanguageId].DOFade(0f, 0f);
                }
                
            }
        }

       

        
    }

}

[System.Serializable]
public class Banner
{
    public string name;
    public CanvasGroup[] scenesBanner;
}

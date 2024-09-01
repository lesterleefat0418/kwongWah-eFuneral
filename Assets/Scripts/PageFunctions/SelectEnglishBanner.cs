using UnityEngine;

public class SelectEnglishBanner : MonoBehaviour
{
    public Select bannerBtns;
    // Start is called before the first frame update
    void Start()
    {
        this.Selected(-1);
    }

    public void Selected(int id)
    {
        this.bannerBtns.set(id, -1);
    }
}

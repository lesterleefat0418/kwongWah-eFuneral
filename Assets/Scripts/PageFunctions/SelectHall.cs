using UnityEngine;

public class SelectHall : MonoBehaviour
{
    public HallSelection selectHall;
    public CanvasGroup nextBtn;
    // Start is called before the first frame update
    void Start()
    {
        this.selectHall.init();
        this.selectHall.page.init(null, 1);
        SetUI.Run(this.nextBtn, false);
    }


    public void select(int id)
    {
        this.selectHall.page.setPage(id, () => this.finishedSelection(id));
    }

    void finishedSelection(int id)
    {
        this.selectHall.set(id);
        if (SettingHall.Instance != null)
        {
            SettingHall.Instance.selectedHallId = id;
            SettingHall.Instance.setGameMode();
        }
        SetUI.Run(this.nextBtn, true);
    }
}


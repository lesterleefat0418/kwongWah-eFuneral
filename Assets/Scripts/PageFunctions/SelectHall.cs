using UnityEngine;

public class SelectHall : MonoBehaviour
{
    public static SelectHall Instance = null;
    public HallSelection selectHall;
    public CanvasGroup nextBtn;
    public Transform hallPeoplePhoto;
    public Vector2[] hallTypePositions;

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
        this.selectHall.init();
        this.selectHall.page.init(null, 1);
        SetUI.Run(this.nextBtn, false);
        if(this.hallPeoplePhoto != null && hallTypePositions != null) 
            this.hallPeoplePhoto.localPosition = hallTypePositions[1];
    }


    public void select(int id)
    {
        this.selectHall.page.setPage(id, () => this.finishedSelection(id));
        if (this.hallPeoplePhoto != null && hallTypePositions != null)
            this.hallPeoplePhoto.localPosition = hallTypePositions[this.selectHall.selected];
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


using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Events;
using DG.Tweening;

public class CountDownTimer : MonoBehaviour
{
    public DisplayFormat displayFormat = DisplayFormat.Timer;
    public bool triggerToStart = false;
    public float totalTime = 5 * 60;
    public float currentTime;
    public Text countdownText;
    private StringBuilder sb = null;
    public Image bground;
    public Color32 shineColor;
    public CanvasGroup remindLastFiveMinutes;
    public bool triggeredRemindLastFiveMinutes = false;
    //public string[] titleHeads = new string[3]{ "剩下時間: ", "剩下时间: ", "Timer: "};
    public string[] heads = new string[3] { "儀式會在: ", "仪式会在: ", "The ceremony will be end after: " };
    public string[] goodByeheads = new string[3] { "禮成會在: ", "礼成会在: ", "The Funeral will be end after: " };
    public string[] ends = new string[3] { "後結束！", "后结束！", "!" };

    public enum DisplayFormat
    {
        OnlyUnits = 0,
        Timer = 1,
    }

    [SerializeField]
    private UnityEvent finished = null;

    public UnityEvent Finished
    {
        get { return finished; }
        set { finished = value; }
    }

    // Example method to invoke the delegate
    private void InvokeFinishedDelegate()
    {
        if (finished != null)
        {
            finished.Invoke();
        }
    }


    private string DefaultHead
    {
        get
        {
            string head = "";
            if(PageController.Instance != null)
            {
                head = PageController.Instance.pageController.currentId != PageController.Instance.pageController.pages.Length - 1 ? heads[LoaderConfig.Instance.SelectedLanguageId] : goodByeheads[LoaderConfig.Instance.SelectedLanguageId];

            }

            return head;
        }
    }

    private string Minute
    {
        get
        {
            string unit = "";

            switch (LoaderConfig.Instance.SelectedLanguageId)
            {
                case 0:
                    unit = "分鍾" + ends[0];
                    break;
                case 1:
                    unit = "分钟" + ends[1];
                    break;
                case 2:
                    unit = " minute(s)" + ends[2];
                    break;
            }

            return unit;
        }
    }

    private string Second
    {
        get
        {
            string unit = "";

            switch (LoaderConfig.Instance.SelectedLanguageId)
            {
                case 0:
                    unit = "秒" + ends[0];
                    break;
                case 1:
                    unit = "秒" + ends[1];
                    break;
                case 2:
                    unit = " second(s)" + ends[2];
                    break;
            }

            return unit;
        }
    }


    public void showTimer()
    {
        SetUI.Set(this.GetComponent<CanvasGroup>(), true, 1f);
        this.triggerToStart = true;
    }

    public void init()
    {
        this.currentTime = this.totalTime;
        this.sb = new StringBuilder();
        SetUI.Set(this.GetComponent<CanvasGroup>(), false, 0f);
    }

    private void Update()
    {
        if(triggerToStart) {

            if(currentTime > 0f)
            {
                this.sb.Clear();
                currentTime -= Time.deltaTime;

                // Calculate minutes and seconds
                int minutes = Mathf.FloorToInt(currentTime / 60);
                int seconds = Mathf.FloorToInt(currentTime % 60);

                if(currentTime <= 360f && !this.triggeredRemindLastFiveMinutes)
                {
                    if(this.bground != null) this.bground.DOColor(shineColor, 1f).SetLoops(-1, LoopType.Yoyo);
                    if(this.remindLastFiveMinutes != null) this.remindLastFiveMinutes.DOFade(1f, 1f).SetLoops(6, LoopType.Yoyo);
                    this.triggeredRemindLastFiveMinutes = true;
                }

                switch (displayFormat)
                {
                    case DisplayFormat.OnlyUnits:
                        if (currentTime <= 60f)
                        {
                            this.sb.Append(this.DefaultHead);
                            this.sb.Append(seconds.ToString("0"));
                            this.sb.Append(Second);
                        }
                        else
                        {
                            this.sb.Append(this.DefaultHead);
                            this.sb.Append(minutes.ToString("0"));
                            this.sb.Append(Minute);
                        }
                        break;
                    case DisplayFormat.Timer:
                        this.sb.Append(this.DefaultHead);
                        this.sb.Append(minutes.ToString("00")); // Format minutes as two digits
                        this.sb.Append(":");
                        this.sb.Append(seconds.ToString("00")); // Format seconds as two digits
                        this.sb.Append(this.ends[LoaderConfig.Instance.SelectedLanguageId]);
                        break;
                }

                
            }
            else
            {
                this.triggerToStart = false;
                Debug.Log("Finished");
                this.sb.Clear();
                this.sb.Append(this.DefaultHead);

                switch (displayFormat)
                {
                    case DisplayFormat.OnlyUnits:
                        this.sb.Append("0");
                        this.sb.Append(Second);
                        break;
                    case DisplayFormat.Timer:
                        this.sb.Append("00:00");
                        this.sb.Append(this.ends[LoaderConfig.Instance.SelectedLanguageId]);
                        break;
                }
                InvokeFinishedDelegate();
            }

            if (this.countdownText != null) { 
                this.countdownText.font = LoaderConfig.Instance.SelectedLanguageId == 1 ? LoaderConfig.Instance.sc: LoaderConfig.Instance.tc;
                this.countdownText.text = this.sb.ToString();
            }

        }
    }
}

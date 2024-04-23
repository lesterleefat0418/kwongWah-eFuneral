using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Events;

public class CountDownTimer : MonoBehaviour
{
    
    public bool triggerToStart = false;
    public float totalTime = 5 * 60;
    public float currentTime;
    public Text countdownText;
    private StringBuilder sb = null;
    public CanvasGroup remindLastFiveMinutes;
    public bool triggeredRemindLastFiveMinutes = false;

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

            switch (LoaderConfig.Instance.SelectedLanguageId)
            {
                case 0:
                    head = "剩下時間: ";
                    break;
                case 1:
                    head = "剩下时间: ";
                    break;
                case 2:
                    head = "Remaining Time: ";
                    break;
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
                    unit = "分鍾";
                    break;
                case 1:
                    unit = "分鍾";
                    break;
                case 2:
                    unit = " minitues";
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
                    unit = "秒";
                    break;
                case 1:
                    unit = "秒";
                    break;
                case 2:
                    unit = " seconds";
                    break;
            }

            return unit;
        }
    }


    public void showTimer()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.triggerToStart = true;
    }

    public void init()
    {
        this.currentTime = this.totalTime;
        this.sb = new StringBuilder();
        this.GetComponent<CanvasGroup>().alpha = 0f;
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

                if(currentTime <= 300f && !this.triggeredRemindLastFiveMinutes)
                {
                    SetUI.Run(this.remindLastFiveMinutes, true, 0.5f);
                    this.triggeredRemindLastFiveMinutes = true;
                }

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
            }
            else
            {
                this.triggerToStart = false;
                Debug.Log("Finished");

                this.sb.Clear();
                this.sb.Append(this.DefaultHead);
                this.sb.Append("0");
                this.sb.Append(Second);

                InvokeFinishedDelegate();
            }

            if (this.countdownText != null) { 
                this.countdownText.font = LoaderConfig.Instance.SelectedLanguageId == 1 ? PageController.Instance.sc: PageController.Instance.tc;
                this.countdownText.text = this.sb.ToString();
            }

        }
    }
}

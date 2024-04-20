using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    public bool triggerToStart = false;
    public float totalTime = 5 * 60;
    public float currentTime;
    public Text countdownText;
    private StringBuilder sb = null;
    public CanvasGroup idlingRemind;
    public bool triggeredIdlingRemind = false;

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

            switch (LoaderConfig.Instance.languageId)
            {
                case 0:
                    head = "儀式剩餘";
                    break;
                case 1:
                    head = "仪式剩余";
                    break;
                case 2:
                    head = "";
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

            switch (LoaderConfig.Instance.languageId)
            {
                case 0:
                    unit = "分鍾";
                    break;
                case 1:
                    unit = "分鍾";
                    break;
                case 2:
                    unit = "minitues";
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

            switch (LoaderConfig.Instance.languageId)
            {
                case 0:
                    unit = "秒";
                    break;
                case 1:
                    unit = "秒";
                    break;
                case 2:
                    unit = "seconds";
                    break;
            }

            return unit;
        }
    }

    private string DefaultEnd
    {
        get
        {
            string head = "";

            switch (LoaderConfig.Instance.languageId)
            {
                case 0:
                    head = "便會自動結束";
                    break;
                case 1:
                    head = "便会自动结束";
                    break;
                case 2:
                    head = "left";
                    break;
            }

            return head;
        }
    }

    public void showTimer()
    {
        this.init();
        this.triggerToStart = true;
    }

    public void init()
    {
        this.currentTime = this.totalTime;
        this.sb = new StringBuilder();
        this.triggeredIdlingRemind = false;
    }

    private void Update()
    {
        if (triggerToStart)
        {

            if (currentTime > 0f)
            {
                this.sb.Clear();
                currentTime -= Time.deltaTime;

                // Calculate minutes and seconds
                int minutes = Mathf.FloorToInt(currentTime / 60);
                int seconds = Mathf.FloorToInt(currentTime % 60);

                if (currentTime <= 15f && !this.triggeredIdlingRemind)
                {
                    SetUI.Run(this.idlingRemind, true, 0.5f);
                    this.triggeredIdlingRemind = true;
                }

                if (currentTime <= 60f)
                {
                    this.sb.Append(this.DefaultHead);
                    this.sb.Append(seconds.ToString("0"));
                    this.sb.Append(this.Second);
                    this.sb.Append(this.DefaultEnd);
                }
                else
                {
                    this.sb.Append(this.DefaultHead);
                    this.sb.Append(minutes.ToString("0"));
                    this.sb.Append(this.Minute);
                    this.sb.Append(this.DefaultEnd);
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

            if (this.countdownText != null)
            {
                this.countdownText.font = LoaderConfig.Instance.SelectedLanguageId == 1 ? PageController.Instance.sc : PageController.Instance.tc;
                this.countdownText.text = this.sb.ToString();
            }

        }
    }
}



using UnityEngine;

public class TimerController : MonoBehaviour
{
    public Sprite[] digits;
    public UnityEngine.UI.Image tenDigitImage;
    public UnityEngine.UI.Image oneDigitImage;

    public void UpdateUI(int time) {
        int lastDigit = time % 10;
        int lastSecondDigit = (time / 10) % 10;
        tenDigitImage.sprite = digits[lastSecondDigit];
        oneDigitImage.sprite = digits[lastDigit];
    }

}

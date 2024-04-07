using UnityEngine;
using UnityEngine.UI;

public class ScrollDrag : MonoBehaviour
{
    private ScrollRect scrollRect;
    private Vector2 previousTouchPosition;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Debug.Log(touch.phase);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    previousTouchPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    Vector2 delta = touch.position - previousTouchPosition;
                    scrollRect.horizontalNormalizedPosition += delta.x / Screen.width;
                    previousTouchPosition = touch.position;
                    break;
            }
        }
    }
}

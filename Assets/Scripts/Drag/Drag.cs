using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    public float offsetX, offsetY;
    private HorizontalLayoutGroup layoutGroup;
    private ContentSizeFitter contentSizeFitter;
    public GameObject targetObject;
    private CanvasGroup dragUI = null;
    public CanvasGroup sceneburn;

    private void Start()
    {
        layoutGroup = transform.parent.GetComponent<HorizontalLayoutGroup>();
        contentSizeFitter = transform.parent.GetComponent<ContentSizeFitter>();
        dragUI = transform.GetComponent<CanvasGroup>();
        if (this.sceneburn != null)
        {
            this.sceneburn.alpha = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        layoutGroup.enabled = false;
        contentSizeFitter.enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        layoutGroup.enabled = true;
        contentSizeFitter.enabled = true;

        if (this.targetObject != null && eventData.pointerCurrentRaycast.gameObject == targetObject)
        {
           this.fireBurn();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPosition);
            transform.localPosition = new Vector3(localPosition.x - offsetX, localPosition.y - offsetY, transform.localPosition.z);
        }
    }

    public void fireBurn()
    {
        if (this.sceneburn != null)
        {
            this.sceneburn.DOFade(1f, 1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBack);
            this.sceneburn.transform.DOScale(0.5f, 2f).SetEase(Ease.Linear);
        }
        SetUI.Set(this.dragUI, false, 0.5f);
        this.enabled = false;
    }
}

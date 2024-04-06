using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollDrag : MonoBehaviour
{
    private Vector2 dragStartPosition;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 dragDelta = touch.position - dragStartPosition;

                if (dragDelta.magnitude > 10f)
                {
                    Vector2 normalizedDragDirection = dragDelta.normalized;

                    if (Mathf.Abs(normalizedDragDirection.x) > Mathf.Abs(normalizedDragDirection.y))
                    {
                        if (normalizedDragDirection.x > 0)
                        {
                            Debug.Log("Drag Right");
                        }
                        else
                        {
                            Debug.Log("Drag Left");
                        }
                    }
                    else
                    {
                        if (normalizedDragDirection.y > 0)
                        {
                            Debug.Log("Drag Up");
                        }
                        else
                        {
                            Debug.Log("Drag Down");
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnHover : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnHoverEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke();
    }
}

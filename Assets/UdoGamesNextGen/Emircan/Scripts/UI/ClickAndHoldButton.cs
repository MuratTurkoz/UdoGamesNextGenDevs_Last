using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHeldDown => isHeldDown;
    private bool isHeldDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeldDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isHeldDown = false;
    }
    /* public void OnPointerEnter(PointerEventData eventData)
    {
    
    } */
}

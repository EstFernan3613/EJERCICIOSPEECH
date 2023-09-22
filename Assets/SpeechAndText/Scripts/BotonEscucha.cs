using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class BotonEscucha : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent OnPress, OnUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke();
    }


}
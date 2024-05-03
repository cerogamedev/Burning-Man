using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace BurningMan.Controller
{
    public class LeftButtonPressed : MonoSingleton<LeftButtonPressed>, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsLeftButtonPressed = false;

        public static Action IsLeftPress;
        public static Action IsLeftPressDone;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsLeftButtonPressed = true;
            IsLeftPress?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsLeftButtonPressed = false;
            IsLeftPressDone?.Invoke();
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
        }
    }
}

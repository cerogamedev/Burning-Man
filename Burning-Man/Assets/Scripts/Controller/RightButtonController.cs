using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace BurningMan.Controller
{
    public class RightButtonController : MonoSingleton<RightButtonController>, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsRightButtonPressed = false;

        public static Action IsRightPress;
        public static Action IsRightPressDone;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsRightButtonPressed = true;
            IsRightPress?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsRightButtonPressed = false;
            IsRightPressDone?.Invoke();
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
        }
    }
}

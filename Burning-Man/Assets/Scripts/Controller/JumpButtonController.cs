using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace BurningMan.Controller
{
    public class JumpButtonController : MonoSingleton<JumpButtonController>, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsJumpButtonPressed = false;
        public static Action JumpButtonPressed;
        public static Action JumpButtonRelease;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsJumpButtonPressed = true;
            JumpButtonPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsJumpButtonPressed = false;
            JumpButtonRelease?.Invoke();
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
        }
    }
}

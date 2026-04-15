using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Gamegaard.Utils
{
    public static class UiUtils
    {
        private static Camera cachedMainCamera;
        private static PointerEventData cachedPointerData;
        private static readonly List<RaycastResult> raycastResults = new(8);

        public static Camera MainCamera
        {
            get
            {
                if (cachedMainCamera == null)
                    cachedMainCamera = Camera.main;
                return cachedMainCamera;
            }
        }

        public static Vector2 MousePos => MainCamera.ScreenToWorldPoint(Input.mousePosition);

        public static bool IsOverUI(Vector2 inputPosition)
        {
            EventSystem eventSystem = EventSystem.current;
            if (eventSystem == null) return false;

            if (cachedPointerData == null)
                cachedPointerData = new PointerEventData(eventSystem);
            else
                cachedPointerData.Reset();

            cachedPointerData.position = inputPosition;
            cachedPointerData.pointerId = -1;

            raycastResults.Clear();
            eventSystem.RaycastAll(cachedPointerData, raycastResults);
            return raycastResults.Count > 0;
        }

        public static bool IsOverUI(Vector2 inputPosition, string tag)
        {
            EventSystem eventSystem = EventSystem.current;
            if (eventSystem == null) return false;
            if (!eventSystem.IsPointerOverGameObject()) return false;

            if (cachedPointerData == null)
                cachedPointerData = new PointerEventData(eventSystem);
            else
                cachedPointerData.Reset();

            cachedPointerData.position = inputPosition;
            cachedPointerData.pointerId = -1;

            raycastResults.Clear();
            eventSystem.RaycastAll(cachedPointerData, raycastResults);

            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.CompareTag(tag))
                    return true;
            }

            return false;
        }

        public static bool IsOverUI(string tag)
        {
            return IsOverUI(GetPointerPosition(), tag);
        }

        public static bool IsOverUI()
        {
            return IsOverUI(GetPointerPosition());
        }

        private static Vector2 GetPointerPosition()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current != null)
                return Mouse.current.position.ReadValue();

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                return Touchscreen.current.primaryTouch.position.ReadValue();
#endif
            return Input.mousePosition;
        }

        public static Vector2 GetCanvasElementWorldPosition(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MainCamera, out var result);
            return result;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gamegaard.Utils
{
    public static class UiUtils
    {
        private static Camera _mainCamera;
        public static Camera MainCamera => _mainCamera ?? (_mainCamera = Camera.main);
        public static Vector2 MousePos { get => MainCamera.ScreenToWorldPoint(Input.mousePosition); }

        public static bool IsOverUI(Vector2 inputPosition)
        {
            EventSystem eventSystem = EventSystem.current;

            if (eventSystem == null) return false;
            PointerEventData pointerData = new(eventSystem)
            {
                position = inputPosition,
                pointerId = -1,
            };

            List<RaycastResult> results = new();
            eventSystem.RaycastAll(pointerData, results);
            return results.Count > 0;
        }

        public static bool IsOverUI(Vector2 inputPosition, string tag)
        {
            EventSystem eventSystem = EventSystem.current;

            if (EventSystem.current == null) return false;
            bool isOverTaggedElement = false;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new(EventSystem.current)
                {
                    position = inputPosition,
                    pointerId = -1,
                };

                List<RaycastResult> results = new();
                EventSystem.current.RaycastAll(pointerData, results);

                if (results.Count > 0)
                {
                    for (int i = 0; i < results.Count; ++i)
                    {
                        if (results[i].gameObject.CompareTag(tag))
                            isOverTaggedElement = true;
                    }
                }
            }
            return isOverTaggedElement;
        }

        public static bool IsOverUI(string tag)
        {
            return IsOverUI(Input.mousePosition, tag);
        }

        public static bool IsOverUI()
        {
            return IsOverUI(Input.mousePosition);
        }

        public static Vector2 GetCanvasElementWorldPosition(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MainCamera, out var result);
            return result;
        }
    }
}
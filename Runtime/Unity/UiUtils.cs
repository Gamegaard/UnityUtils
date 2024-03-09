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

        public static bool IsOverUI()
        {
            PointerEventData pointerData = new(EventSystem.current)
            {
                position = Input.mousePosition,
                pointerId = -1,
            };

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0;
        }

        public static bool IsOverUI(string tag)
        {
            bool isOverTaggedElement = false;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new(EventSystem.current)
                {
                    position = Input.mousePosition,
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

        public static Vector2 GetCanvasElementWorldPosition(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MainCamera, out var result);
            return result;
        }
    }
}
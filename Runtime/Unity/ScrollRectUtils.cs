using UnityEngine;
using UnityEngine.UI;

namespace Gamegaard.Utils
{
    public static class ScrollRectUtils
    {
        public static void SnapToChild(this ScrollRect element, RectTransform targetChild)
        {
            element.content.localPosition = element.GetSnapChildPosition(targetChild);
        }

        public static Vector2 GetSnapChildPosition(this ScrollRect element, RectTransform targetChild)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = element.viewport.localPosition;
            Vector2 childLocalPosition = targetChild.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
            return result;
        }
    }
}
using UnityEngine;

namespace Gamegaard.Utils.Runtime
{
    public static class TMPUtils
    {
        public static string GetTMPColor(this Color color, string text)
        {
            string[] texts = { "<color=#", ColorUtility.ToHtmlStringRGB(color), ">", text, "</color>" };
            return string.Concat(texts);
        }

        public static string ChangeTagBy(this string originalText, string tag, string text)
        {
            return originalText.Replace($"<{tag}>", text);
        }
    }
}
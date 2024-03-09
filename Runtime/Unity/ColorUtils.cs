using System;
using UnityEngine;

namespace Gamegaard.Utils
{
    //Corrigir. Códigos do CodeMonkey!
    public static class ColorUtils
    {
        // Returns 00-FF, value 0->255
        public static string DecimalToHex(int value)
        {
            return value.ToString("X2");
        }

        // Returns 0-255
        public static int HexToDecimal(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        // Returns a hex string based on a number between 0->1
        public static string Decimal01ToHex(float value)
        {
            return DecimalToHex((int)Mathf.Round(value * 255f));
        }

        // Returns a float between 0->1
        public static float HexToDecimal01(string hex)
        {
            return HexToDecimal(hex) / 255f;
        }

        // Get Hex Color FF00FF
        public static string GetHexFromColor(Color color)
        {
            string red = Decimal01ToHex(color.r);
            string green = Decimal01ToHex(color.g);
            string blue = Decimal01ToHex(color.b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetHexFromAlphaColor(Color color)
        {
            string alpha = Decimal01ToHex(color.a);
            return GetHexFromColor(color) + alpha;
        }

        // Sets out values to Hex String 'FF'
        public static void GetHexFromColor(Color color, out string red, out string green, out string blue, out string alpha)
        {
            red = Decimal01ToHex(color.r);
            green = Decimal01ToHex(color.g);
            blue = Decimal01ToHex(color.b);
            alpha = Decimal01ToHex(color.a);
        }

        // Get Hex Color FF00FF
        public static string GetHexFromColor(float r, float g, float b)
        {
            string red = Decimal01ToHex(r);
            string green = Decimal01ToHex(g);
            string blue = Decimal01ToHex(b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetHexFromColor(float r, float g, float b, float a)
        {
            string alpha = Decimal01ToHex(a);
            return GetHexFromColor(r, g, b) + alpha;
        }

        // Get Color from Hex string FF00FFAA
        public static Color GetColorFromHex(string color)
        {
            float red = HexToDecimal01(color.Substring(0, 2));
            float green = HexToDecimal01(color.Substring(2, 2));
            float blue = HexToDecimal01(color.Substring(4, 2));
            float alpha = 1f;
            if (color.Length >= 8)
            {
                alpha = HexToDecimal01(color.Substring(6, 2));
            }
            return new Color(red, green, blue, alpha);
        }

        // Return a color going from Red to Yellow to Green, like a heat map
        public static Color GetRedGreenColor(float value)
        {
            float r = 0f;
            float g = 0f;
            if (value <= .5f)
            {
                r = 1f;
                g = value * 2f;
            }
            else
            {
                g = 1f;
                r = 1f - (value - .5f) * 2f;
            }
            return new Color(r, g, 0f, 1f);
        }

        public static Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        }

        public static Color GetColor255(float red, float green, float blue, float alpha = 255f)
        {
            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }

        public static bool IsColorSimilar255(Color colorA, Color colorB, int maxDiff)
        {
            return IsColorSimilar(colorA, colorB, maxDiff / 255f);
        }

        public static bool IsColorSimilar(Color colorA, Color colorB, float maxDiff)
        {
            float rDiff = Mathf.Abs(colorA.r - colorB.r);
            float gDiff = Mathf.Abs(colorA.g - colorB.g);
            float bDiff = Mathf.Abs(colorA.b - colorB.b);
            float aDiff = Mathf.Abs(colorA.a - colorB.a);

            float totalDiff = rDiff + gDiff + bDiff + aDiff;
            return totalDiff < maxDiff;
        }

        public static float GetColorDifference(Color colorA, Color colorB)
        {
            float rDiff = Mathf.Abs(colorA.r - colorB.r);
            float gDiff = Mathf.Abs(colorA.g - colorB.g);
            float bDiff = Mathf.Abs(colorA.b - colorB.b);
            float aDiff = Mathf.Abs(colorA.a - colorB.a);

            float totalDiff = rDiff + gDiff + bDiff + aDiff;
            return totalDiff;
        }
    }
}
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class GizmosUtils
    {
        public static void DrawArrow(Transform caster, Vector3 direction, float arrowLength = 1f, float arrowHeadSize = 0.2f)
        {
            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = caster.localToWorldMatrix;

            Vector3 arrowDirection = direction.normalized;
            Gizmos.DrawLine(Vector3.zero, arrowDirection * arrowLength);

            Vector3 arrowEnd = arrowDirection * arrowLength;
            Vector3 arrowLeft = arrowEnd + Quaternion.Euler(0, 0, 30) * -arrowDirection * arrowHeadSize;
            Vector3 arrowRight = arrowEnd + Quaternion.Euler(0, 0, -30) * -arrowDirection * arrowHeadSize;
            Gizmos.DrawLine(arrowEnd, arrowLeft);
            Gizmos.DrawLine(arrowEnd, arrowRight);

            Gizmos.matrix = originalMatrix;
        }

        public static void DrawText(string text, Vector3 position, int fontSize, bool isRichText = false)
        {
            GUIStyle style = new GUIStyle();
            style.richText = isRichText;
            style.normal.textColor = Gizmos.color;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = fontSize;

#if UNITY_EDITOR
            Handles.Label(position, text, style);
#endif
        }

        public static void DrawCircle(Vector3 center, float radius, Color color, int segments = 20)
        {
            Gizmos.color = color;
            const float TWO_PI = Mathf.PI * 2;
            float step = TWO_PI / (float)segments;
            float theta = 0;
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = center + new Vector3(x, y, 0);
            Vector3 newPos;
            Vector3 lastPos = pos;

            for (theta = step; theta < TWO_PI; theta += step)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                newPos = center + new Vector3(x, y, 0);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }
            Gizmos.DrawLine(pos, lastPos);
        }

        public static void DrawSquare(Vector3 center, float size, float rotationAngleInDegrees = 0f)
        {
            float halfSize = size * 0.5f;
            float angleInRadians = rotationAngleInDegrees * Mathf.Deg2Rad;

            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.identity;

            Vector3 topLeft = new Vector3(-halfSize, halfSize, 0f);
            Vector3 topRight = new Vector3(halfSize, halfSize, 0f);
            Vector3 bottomLeft = new Vector3(-halfSize, -halfSize, 0f);
            Vector3 bottomRight = new Vector3(halfSize, -halfSize, 0f);

            float cosAngle = Mathf.Cos(angleInRadians);
            float sinAngle = Mathf.Sin(angleInRadians);
            Vector3 rotatedTopLeft = new Vector3(cosAngle * topLeft.x - sinAngle * topLeft.y, sinAngle * topLeft.x + cosAngle * topLeft.y, 0f);
            Vector3 rotatedTopRight = new Vector3(cosAngle * topRight.x - sinAngle * topRight.y, sinAngle * topRight.x + cosAngle * topRight.y, 0f);
            Vector3 rotatedBottomLeft = new Vector3(cosAngle * bottomLeft.x - sinAngle * bottomLeft.y, sinAngle * bottomLeft.x + cosAngle * bottomLeft.y, 0f);
            Vector3 rotatedBottomRight = new Vector3(cosAngle * bottomRight.x - sinAngle * bottomRight.y, sinAngle * bottomRight.x + cosAngle * bottomRight.y, 0f);

            Vector3 worldTopLeft = center + rotatedTopLeft;
            Vector3 worldTopRight = center + rotatedTopRight;
            Vector3 worldBottomLeft = center + rotatedBottomLeft;
            Vector3 worldBottomRight = center + rotatedBottomRight;

            Gizmos.DrawLine(worldTopLeft, worldTopRight);
            Gizmos.DrawLine(worldTopRight, worldBottomRight);
            Gizmos.DrawLine(worldBottomRight, worldBottomLeft);
            Gizmos.DrawLine(worldBottomLeft, worldTopLeft);

            Gizmos.matrix = originalMatrix;
        }

        public static void DrawTriangle(Vector3 center, float size, float angleInDegrees = 0)
        {
            float halfSize = size * 0.5f;
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.identity;

            Vector3 top = new Vector3(0f, halfSize, 0f);
            Vector3 bottomLeft = new Vector3(-halfSize, -halfSize * 0.57735f, 0f);
            Vector3 bottomRight = new Vector3(halfSize, -halfSize * 0.57735f, 0f);

            float cosAngle = Mathf.Cos(angleInRadians);
            float sinAngle = Mathf.Sin(angleInRadians);
            Vector3 rotatedTop = new Vector3(cosAngle * top.x - sinAngle * top.y, sinAngle * top.x + cosAngle * top.y, 0f);
            Vector3 rotatedBottomLeft = new Vector3(cosAngle * bottomLeft.x - sinAngle * bottomLeft.y, sinAngle * bottomLeft.x + cosAngle * bottomLeft.y, 0f);
            Vector3 rotatedBottomRight = new Vector3(cosAngle * bottomRight.x - sinAngle * bottomRight.y, sinAngle * bottomRight.x + cosAngle * bottomRight.y, 0f);

            Vector3 worldTop = center + rotatedTop;
            Vector3 worldBottomLeft = center + rotatedBottomLeft;
            Vector3 worldBottomRight = center + rotatedBottomRight;

            Gizmos.DrawLine(worldTop, worldBottomLeft);
            Gizmos.DrawLine(worldBottomLeft, worldBottomRight);
            Gizmos.DrawLine(worldBottomRight, worldTop);

            Gizmos.matrix = originalMatrix;
        }

        public static void DrawSolidPolygon(Vector3[] vertices)
        {
            if (vertices.Length < 3) return;

            for (int i = 1; i < vertices.Length - 1; i++)
            {
                Gizmos.DrawLine(vertices[0], vertices[i]);
                Gizmos.DrawLine(vertices[i], vertices[i + 1]);
                Gizmos.DrawLine(vertices[i + 1], vertices[0]);
            }
        }

        public static void DrawThickLine(Vector3 start, Vector3 end, float thickness)
        {
#if UNITY_EDITOR
            Handles.DrawBezier(start, end, start, end, Gizmos.color, null, thickness);
#endif
        }
    }
}
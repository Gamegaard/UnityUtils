using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gamegaard
{
    public abstract class BaseNamedGizmoDrawer : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Gizmo Display")]
        [SerializeField] private bool showAlways = true;
        [Range(0, 1)]
        [SerializeField] protected float opacity = 1;

        [Header("Label")]
        [SerializeField] private bool showLabel = true;
        [SerializeField] private Color labelColor = Color.white;
        [SerializeField] protected Vector3 labelOffset = new(0, 0.5f, 0);
        [SerializeField] private int baseFontSize = 13;
        [SerializeField] private float fontScale = 1.0f;
        [SerializeField] private float maxDistanceToShow = 40f;

        public Vector3 CenterPosition => transform.position + labelOffset;
        public virtual Vector3 LabelPosition => CenterPosition;
        public float CameraDistance
        {
            get
            {
                Camera sceneCamera = SceneView.lastActiveSceneView.camera;
                return Vector3.Distance(sceneCamera.transform.position, CenterPosition);
            }
        }
        public virtual float Alpha
        {
            get
            {
                float fadeStart = maxDistanceToShow * 0.8f;
                float alpha = CameraDistance > fadeStart ? Mathf.InverseLerp(maxDistanceToShow, fadeStart, CameraDistance) : 1f;
                return alpha * opacity;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (showAlways && enabled)
            {
                DrawGizmos();
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (!showAlways && enabled)
            {
                DrawGizmos();
            }
        }

        protected virtual void DrawGizmos()
        {
            DrawMainGizmo();
            if (showLabel)
            {
                DrawLabel(LabelPosition);
            }
        }

        protected abstract void DrawMainGizmo();
        protected virtual string GetLabelText() => gameObject.name;

        protected virtual void DrawLabel(Vector3 position)
        {
            if (!IsInSceneView(position)) return;

            if (CameraDistance > maxDistanceToShow)
                return;

            Vector2 screenPoint = HandleUtility.WorldToGUIPoint(position);

            Handles.BeginGUI();

            float handleSize = HandleUtility.GetHandleSize(position);
            int fontSize = Mathf.Max(10, Mathf.RoundToInt(baseFontSize * (1f / handleSize) * fontScale));

            string label = GetLabelText();
            GUIContent content = new GUIContent(label);

            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = fontSize,
                fontStyle = FontStyle.Bold,
                normal = { textColor = labelColor },
                hover = { textColor = labelColor },
                padding = new RectOffset(2, 2, 1, 1)
            };

            Vector2 size = style.CalcSize(content);
            Rect rect = new Rect(screenPoint.x - size.x / 2, screenPoint.y - size.y / 2, size.x, size.y);

            Color shadowColor = new Color(0f, 0f, 0f, Alpha);
            Color mainColor = new Color(labelColor.r, labelColor.g, labelColor.b, Alpha);

            GUI.color = shadowColor;
            GUI.Label(new Rect(rect.x + 1, rect.y + 1, rect.width, rect.height), content, style);

            GUI.color = mainColor;
            GUI.Label(rect, content, style);

            GUI.color = Color.white;
            Handles.EndGUI();
        }

        protected bool IsInSceneView(Vector3 worldPosition)
        {
            if (SceneView.lastActiveSceneView == null || SceneView.lastActiveSceneView.camera == null)
                return false;

            Camera cam = SceneView.lastActiveSceneView.camera;
            Vector3 viewportPos = cam.WorldToViewportPoint(worldPosition);

            return viewportPos.z > 0 &&
                   viewportPos.x >= 0 && viewportPos.x <= 1 &&
                   viewportPos.y >= 0 && viewportPos.y <= 1;
        }
#endif
    }
}
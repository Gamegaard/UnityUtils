using UnityEngine;

namespace Gamegaard.Utils
{
    public static class Vector2Utils
    {
        #region Common
        /// <summary>
        /// Retorna um vetor2 com adiçao de offsets.
        /// </summary>
        public static Vector2 Get2DOffset(Vector2 pos, float xOff, float yOff)
        {
            Vector2 offset = pos;
            offset.x += xOff;
            offset.y += yOff;
            return offset;
        }

        /// <summary>
        /// Retorna um vetor2 com adiçao de offsets baseado no tamanho y do render.
        /// </summary>
        public static Vector2 Get2DOffset(Vector2 pos, SpriteRenderer renderer, float xOff, float yOff)
        {
            Vector2 offset = pos;
            offset.x += xOff;
            offset.y += renderer.sprite.bounds.size.y / 2 + yOff;
            return offset;
        }

        /// <summary>
        /// Retorna a distancia entre dois vetores. (Mais otimizado que o padrao da unity)
        /// </summary>
        public static float GetDistance(Vector2 a, Vector2 b)
        {
            return Vector2.SqrMagnitude(b - a);
        }

        /// <summary>
        /// Retorna uma direçao randomica.
        /// </summary>
        public static Vector2 GetRandomDir()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Retorna a direçao normalizada da posiçao ao alvo.
        /// </summary>
        public static Vector2 GetDirection(Vector2 position, Vector2 target)
        {
            return (target - position).normalized;
        }

        /// <summary>
        /// Retorna a direçao normalizada da posiçao ao alvo multiplicado por um número.
        /// </summary>
        public static Vector2 GetDirection(Vector2 position, Vector2 target, float power)
        {
            return (target - position).normalized * power;
        }

        /// <summary>
        /// Transforma radiano em Vector2.
        /// </summary>
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Transforma graus em Vector2.
        /// </summary>
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static Vector2 ProjectOnLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 lineDirection = lineEnd - lineStart;
            Vector2 pointDirection = point - lineStart;

            float t = Vector2.Dot(pointDirection, lineDirection) / Vector2.Dot(lineDirection, lineDirection);

            t = Mathf.Clamp01(t);

            Vector2 projectedPos = lineStart + t * lineDirection;

            return projectedPos;
        }
        #endregion

        #region Extensions
        /// <summary>
        /// Retorna o angulo em float entre 0 e 360.
        /// </summary>
        public static float GetFloatAngle(this Vector2 vector)
        {
            vector = vector.normalized;
            float n = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        /// <summary>
        /// Retorna o angulo em int entre 0 e 360.
        /// </summary>
        public static int GetIntAngle(this Vector2 vector)
        {
            vector = vector.normalized;
            float n = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }

        /// <summary>
        /// Retorna a direçao normalizada até um alvo.
        /// </summary>
        public static Vector2 GetDirTo(this Vector2 vector, Vector2 target)
        {
            return (target - vector).normalized;
        }

        /// <summary>
        /// Incrementa x graus a rotaçao do vetor.
        /// </summary>
        public static Vector2 IncreaseAngleToVector2(this Vector2 iniVec, float degree)
        {
            return Quaternion.Euler(0, 0, degree) * iniVec;
        }

        /// <summary>
        /// Retorna o valor com clamp nos 2 valores
        /// </summary>
        public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
        {
            float clampedX = Mathf.Clamp(vector.x, min.x, max.x);
            float clampedY = Mathf.Clamp(vector.y, min.y, max.y);

            return new Vector2(clampedX, clampedY);
        }
        #endregion
    }
}
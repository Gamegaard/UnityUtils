using UnityEngine;

namespace Gamegaard.Utils
{
    public static class Vector3Utils
    {
        #region Common
        /// <summary>
        /// Retorna o angulo em float de uma direçao entre 0 e 360.
        /// </summary>
        public static float GetVector3ToFloatAngle(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        /// <summary>
        /// Retorna o angulo em int de uma direçao entre 0 e 360.
        /// </summary>
        public static int GetVector3ToIntAngle(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }

        /// <summary>
        /// Retorna um vetor3 com adiçao de offsets.
        /// </summary>
        public static Vector3 Get3DOffset(Vector3 pos, float xOff, float yOff, float zOff)
        {
            Vector3 offset = pos;
            offset.x += xOff;
            offset.y += yOff;
            offset.z += zOff;
            return offset;
        }

        /// <summary>
        /// Retorna um vetor3 com adiçao de offsets baseado no tamanho y do render.
        /// </summary>
        public static Vector3 Get3DOffset(Vector3 pos, Renderer renderer, float xOff, float yOff, float zOff)
        {
            Vector3 offset = pos;
            offset.x += xOff;
            offset.y += yOff;
            offset.z += renderer.bounds.size.z / 2 + zOff;
            return offset;
        }

        /// <summary>
        /// Retorna a distancia entre dois vetores. (Mais otimizado que o padrao da unity)
        /// </summary>
        public static float GetDistance(Vector3 a, Vector3 b)
        {
            return Vector3.SqrMagnitude(b - a);
        }

        /// <summary>
        /// Retorna uma direçao randomica.
        /// </summary>
        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Retorna a direçao normalizada da posiçao ao alvo.
        /// </summary>
        public static Vector3 GetDirection(Vector3 position, Vector3 target)
        {
            return (target - position).normalized;
        }

        /// <summary>
        /// Retorna a direçao normalizada da posiçao ao alvo multiplicado por um número.
        /// </summary>
        public static Vector3 GetDirection(Vector3 position, Vector3 target, float power)
        {
            return (target - position).normalized * power;
        }

        /// <summary>
        /// Retorna um Vector3 com o valor de 180 no eixo X.
        /// </summary>
        public static Vector3 XFlip() => new Vector3(180, 0, 0);

        /// <summary>
        /// Adiciona 180 ao eixo X.
        /// </summary>
        public static Vector3 XFlip(this Vector3 vector3) => new Vector3(vector3.x + 180, vector3.y, vector3.z);

        /// <summary>
        /// Retorna um Vector3 com o valor de 180 no eixo Y.
        /// </summary>
        public static Vector3 YFlip() => new Vector3(0, 180, 0);

        /// <summary>
        /// Adiciona 180 ao eixo Y.
        /// </summary>
        public static Vector3 YFlip(this Vector3 vector3) => new Vector3(vector3.x, vector3.y + 180, vector3.z);

        /// <summary>
        /// Retorna um Vector3 com o valor de 180 no eixo Z.
        /// </summary>
        public static Vector3 ZFlip() => new Vector3(0, 0, 180);

        /// <summary>
        /// Adiciona 180 ao eixo Z.
        /// </summary>
        public static Vector3 ZFlip(this Vector3 vector3) => new Vector3(vector3.x, vector3.y, vector3.z + 180);

        public static Vector3 RandomPointInBounds(this Bounds bounds)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            return new Vector3(randomX, randomY, randomZ);
        }
        #endregion

        #region Extensions
        /// <summary>
        /// Retorna a direçao normalizada até um alvo.
        /// </summary>
        public static Vector3 GetDirTo(this Vector3 vector, Vector3 target)
        {
            return (target - vector).normalized;
        }

        /// <summary>
        /// Retorna o angulo em float entre 0 e 360.
        /// </summary>
        public static float GetFloatAngle(this Vector3 vector)
        {
            vector = vector.normalized;
            float n = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        /// <summary>
        /// Retorna o angulo em int entre 0 e 360.
        /// </summary>
        public static int GetIntAngle(this Vector3 vector)
        {
            vector = vector.normalized;
            float n = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }

        /// <summary>
        /// Rotaciona o vetor em angulos, e retorna um novo Vector3.
        /// </summary>
        public static Vector3 ApplyRotationToVector(this Vector3 vector, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vector;
        }

        /// <summary>
        /// Rotates a point around a 2D center by X degrees
        /// </summary>
        public static Vector3 RotateAround(this Vector3 vec, Vector2 center, float rotDegrees)
        {
            rotDegrees *= Mathf.Deg2Rad;

            float tempX = vec.x - center.x;
            float tempY = vec.y - center.y;

            float rotatedX = tempX * Mathf.Cos(rotDegrees) - tempY * Mathf.Sin(rotDegrees);
            float rotatedY = tempX * Mathf.Sin(rotDegrees) + tempY * Mathf.Cos(rotDegrees);

            vec.x = rotatedX + center.x;
            vec.y = rotatedY + center.y;

            return vec;
        }

        /// <summary>
        /// Retorna um novo Vector3 com X alterado e demais valores identicos.
        /// </summary>
        public static Vector3 SetVectorXValue(this Vector3 vector, float newXvalue = 0)
        {
            return new Vector3(newXvalue, vector.y, vector.z);
        }

        /// <summary>
        /// Retorna um novo Vector3 com Y alterado e demais valores identicos.
        /// </summary>
        public static Vector3 SetVectorYValue(this Vector3 vector, float newYvalue = 0)
        {
            return new Vector3(vector.x, newYvalue, vector.z);
        }

        /// <summary>
        /// Retorna um novo Vector3 com Z alterado e demais valores identicos.
        /// </summary>
        public static Vector3 SetVectorZValue(this Vector3 vector, float newZvalue = 0)
        {
            return new Vector3(vector.x, vector.y, newZvalue);
        }

        /// <summary>
        /// Retorna o valor com clamp nos 3 valores
        /// </summary>
        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            float clampedX = Mathf.Clamp(vector.x, min.x, max.x);
            float clampedY = Mathf.Clamp(vector.y, min.y, max.y);
            float clampedZ = Mathf.Clamp(vector.z, min.z, max.z);

            return new Vector3(clampedX, clampedY, clampedZ);
        }
        #endregion
    }
}
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// Checa se o GameObject tem um componente específico.
        /// </summary>
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponent<T>();
        }

        /// <summary>
        /// Ativa ou desativa todos os componentes do GameObject.
        /// </summary>
        public static void SetAllComponentsEnabled(this GameObject go, bool isActive)
        {
            MonoBehaviour[] itemz = go.GetComponents<MonoBehaviour>();
            for (int i = 0; i < itemz.Length; i++)
            {
                MonoBehaviour c = itemz[i];
                c.enabled = isActive;
            }
        }

        /// <summary>
        /// Retorna verdadeiro quando o objeto não é nulo.
        /// </summary>
        public static bool IsAlive(this object aObj)
        {
            Object unityObj = aObj as Object;
            return unityObj != null;
        }
    }
}
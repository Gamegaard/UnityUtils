using UnityEngine;

namespace Gamegaard.Utils.Runtime
{
    public static class LayerMaskUtils
    {
        /// <summary>
        /// Retorna True se a layer está contida na LayerMask.
        /// </summary>
        public static bool IsInLayerMask(LayerMask mask, LayerMask layer)
        {
            return (mask & 1 << layer) == 1 << layer;
        }

        /// <summary>
        /// Checa se o gameobject do collider2D está na layer.
        /// </summary>
        public static bool IsOnLayer(this Collider2D collider, LayerMask mask)
        {
            return mask == (mask | (1 << collider.gameObject.layer));
        }

        /// <summary>
        /// Checa se o gameobject do collider2D está na layer.
        /// </summary>
        public static bool IsOnLayer(this Collider2D collider, string maskName)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer(maskName);
        }
    }
}
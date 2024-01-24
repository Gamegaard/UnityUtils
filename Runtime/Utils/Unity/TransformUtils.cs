using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class TransformUtils
    {
        /// <summary>
        /// Define o objeto para a layer desejada, inluindo seus filhos.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="layer"></param>
        public static void SetAllChildrenLayer(Transform parent, int layer)
        {
            parent.gameObject.layer = layer;
            foreach (Transform trans in parent)
            {
                SetAllChildrenLayer(trans, layer);
            }
        }

        /// <summary>
        /// Resseta a posiçao, rotaçao e escala para o valor padrao.
        /// </summary>
        public static void ResetTransformation(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }

        /// <summary>
        /// Destroi todos os objetos filhos.
        /// </summary>
        public static void DestroyAllChildrens(this Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Destroi todos os objetos filhos, ignorando aqueles com os nomes desejados.
        /// </summary>
        public static void DestroyChildren(Transform parent, params string[] ignoreArr)
        {
            foreach (Transform transform in parent)
            {
                if (ignoreArr.Contains(transform.name)) continue;
                Object.Destroy(transform.gameObject);
            }
        }

        /// <summary>
        /// Ativa ou desativa todos os objetos filhos.
        /// </summary>
        public static void SetAllChildrenEnabled(this Transform parent, bool enabled)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject child = parent.GetChild(i).gameObject;
                child.SetActive(enabled);
            }
        }

        /// <summary>
        /// Ativa ou desativa todos os objetos filhos.
        /// </summary>
        public static void SetFirstChildrensEnabled(this Transform parent, int amount, bool enabled)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                bool isEnabled = !(i < amount && enabled || i >= amount && !enabled);
                GameObject child = parent.GetChild(i).gameObject;
                child.SetActive(isEnabled);
            }
        }
    }
}
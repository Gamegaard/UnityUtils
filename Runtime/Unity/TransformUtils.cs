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
        public static void SetAllChildrenLayer(this Transform parent, int layer)
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

        /// <summary>
        /// Retorna o transform mais próximo de alguma posição especifica.
        /// </summary>
        /// <param name="referencePoint">Posição a ser comparada.</param>
        /// <param name="transforms">Grupo de elementos para serem checados.</param>
        /// <returns>Retorna o objeto mais proximo da posição especificada.</returns>
        public static Transform GetClosestTransform(Vector3 referencePoint, IEnumerable<Transform> transforms)
        {
            Transform closestTransform = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach (Transform transform in transforms)
            {
                float distanceSqr = (transform.position - referencePoint).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestTransform = transform;
                }
            }

            return closestTransform;
        }

        /// <summary>
        /// Retorna o transform mais longe de alguma posição especifica.
        /// </summary>
        /// <param name="referencePoint">Posição a ser comparada.</param>
        /// <param name="transforms">Grupo de elementos para serem checados.</param>
        /// <returns>Retorna o objeto mais longe da posição especificada.</returns>
        public static Transform GetFarestTransform(Vector3 referencePoint, IEnumerable<Transform> transforms)
        {
            Transform farestTransform = null;
            float farestDistanceSqr = 0;

            foreach (Transform transform in transforms)
            {
                float distanceSqr = (transform.position - referencePoint).sqrMagnitude;
                if (distanceSqr > farestDistanceSqr)
                {
                    farestDistanceSqr = distanceSqr;
                    farestTransform = transform;
                }
            }

            return farestTransform;
        }

        /// <summary>
        /// Troca dois filhos de um mesmo pai, baseado nos seus índices na hierarquia.
        /// </summary>
        /// <param name="parent">O Transform pai que contém os filhos.</param>
        /// <param name="child01Index">Índice do primeiro filho.</param>
        /// <param name="child02Index">Índice do segundo filho.</param>
        public static void SwapChildrens(this Transform parent, int child01Index, int child02Index)
        {
            Transform child01 = parent.GetChild(child01Index);
            Transform child02 = parent.GetChild(child02Index);

            child01.SetSiblingIndex(child02Index);
            child02.SetSiblingIndex(child01Index);
        }

        /// <summary>
        /// Troca dois Transforms filhos, mantendo suas posições relativas na hierarquia.
        /// </summary>
        /// <param name="child01">O primeiro Transform filho.</param>
        /// <param name="child02">O segundo Transform filho.</param>
        public static void SwapChildrens(Transform child01, Transform child02)
        {
            int index01 = child01.GetSiblingIndex();
            int index02 = child02.GetSiblingIndex();

            child01.SetSiblingIndex(index02);
            child02.SetSiblingIndex(index01);
        }

        /// <summary>
        /// Troca dois Transforms filhos e, opcionalmente, suas posições, rotações e escalas.
        /// </summary>
        /// <param name="child01">O primeiro Transform filho.</param>
        /// <param name="child02">O segundo Transform filho.</param>
        /// <param name="swapPosition">Indica se a posição deve ser trocada.</param>
        /// <param name="swapRotation">Indica se a rotação deve ser trocada.</param>
        /// <param name="swapScale">Indica se a escala deve ser trocada.</param>
        public static void SwapChildrens(Transform child01, Transform child02, bool swapPosition, bool swapRotation, bool swapScale)
        {
            int index01 = child01.GetSiblingIndex();
            int index02 = child02.GetSiblingIndex();

            child01.SetSiblingIndex(index02);
            child02.SetSiblingIndex(index01);

            if (swapPosition)
            {
                Vector3 tempPosition = child01.localPosition;
                child01.localPosition = child02.localPosition;
                child02.localPosition = tempPosition;
            }

            if (swapRotation)
            {
                Quaternion tempRotation = child01.localRotation;
                child01.localRotation = child02.localRotation;
                child02.localRotation = tempRotation;
            }

            if (swapScale)
            {
                Vector3 tempScale = child01.localScale;
                child01.localScale = child02.localScale;
                child02.localScale = tempScale;
            }
        }

        /// <summary>
        /// Retorna um array contendo todos os filhos diretos de um Transform.
        /// </summary>
        /// <param name="parent">O Transform pai cujos filhos serão retornados.</param>
        /// <returns>Um array de Transforms contendo todos os filhos diretos do Transform pai.</returns>
        public static Transform[] GetAllChildrens(this Transform parent)
        {
            Transform[] children = new Transform[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                children[i] = parent.GetChild(i);
            }

            return children;
        }
    }
}
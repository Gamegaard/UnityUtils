using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamegaard.Utils
{
    public static class ExtraUtils
    {
        /// <summary>
        /// Torna a classe um Singleton.
        /// </summary>
        public static void SetSingleton<T>(GameObject go, ref T Instance, T classRef, bool ddol) where T : class
        {
            if (Instance != null && Instance != classRef)
            {
                Object.Destroy(go);
                return;
            }

            Instance = classRef;

            if (ddol)
                Object.DontDestroyOnLoad(go);
        }

        /// <summary>
        /// Remove o objeto do "DontDestroyOnLoad", voltando a ser destruído ao transitar entre cenas.
        /// </summary>
        public static void DestroyOnLoad(GameObject targetGo)
        {
            SceneManager.MoveGameObjectToScene(targetGo, SceneManager.GetActiveScene());
        }
    }
}
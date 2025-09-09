using UnityEngine;

namespace DPX.Utilities
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        instance = singletonObject.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
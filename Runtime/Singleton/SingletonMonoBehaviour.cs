using UnityEngine;

namespace Produktivkeller.SimpleCodePatterns.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Initialize()
        {
        }

        protected virtual bool ShouldDestroyOnLoad()
        {
            return false;
        }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;

                if (!ShouldDestroyOnLoad())
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(this);
                }

                Initialize();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
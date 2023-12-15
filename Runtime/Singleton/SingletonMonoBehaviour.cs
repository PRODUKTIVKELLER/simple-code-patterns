using UnityEngine;

namespace Produktivkeller.SimpleCodePatterns.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        /**
         * Useful to reset the instance in a static method with [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
         * when 'Domain Reload' is disabled as documented in https://docs.unity3d.com/Manual/DomainReloading.html.
         *
         * We can't use a static method in the parent class 'SingletonMonoBehaviour' as the static method will not be called for child classes:
         * https://forum.unity.com/threads/runtimeinitializeonloadmethod-doesnt-run-on-children.790130/
         */
        protected void ResetInstance()
        {
            Instance = null;
        }

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
using UnityEngine;

namespace Produktivkeller.SimpleCodePatterns.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
        
        /**
         * Reset the instance field even if 'Domain Reload' is disabled in the player settings.
         *
         * https://docs.unity3d.com/Manual/DomainReloading.html
         */
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetEvenWithoutDomainReload()
        {
            if (!Instance)
            {
                return;
            }

            SingletonMonoBehaviour<T> singletonMonoBehaviour = Instance as SingletonMonoBehaviour<T>;
            
            if (singletonMonoBehaviour && singletonMonoBehaviour.ShouldResetEvenWithoutDomainReload())
            {
                Instance = null;
            }
        }

        protected virtual void Initialize()
        {
        }

        protected virtual bool ShouldDestroyOnLoad()
        {
            return false;
        }

        protected virtual bool ShouldResetEvenWithoutDomainReload()
        {
            return true;
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
using UnityEngine;

namespace N.Package.Core 
{
    /// <summary>
    /// Extend this class if you want your monobehaviour to be a singleton!
    /// Your child class should implement Init() rather than Awake()
    /// </summary>
    /// <typeparam name="T">The concrete type of your extending class</typeparam>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        static T instance;

        /// <summary>
        /// Whether or not this object should persist when loading new scenes.
        /// This should be set in the child classes Init() method.
        /// </summary>
        bool persist = false;

        /// <summary>
        /// The instance of this Singleton Monobehaviour
        /// </summary>
        public static T Instance {
            get {
                // This would only EVER be null if some other MonoBehavior requests the instance
                // in its' Awake method.
                if(instance == null) {
                    createInstance();
                }
                return instance;
            }
        }

    #region Basic getters/setters
        public bool Persist {
            get { return persist; }
            protected set { persist = value; }
        }
        #endregion

        /// <summary>
        /// This will initialize our instance, if it hasn't already been prompted to do so by
        /// another MonoBehavior's Awake() requesting it first.
        /// </summary>
        public virtual void Awake() {
            Debug.Log("[MonoBehaviourSingleton] Awake");
            if(instance == null) {
                Debug.Log("[MonoBehaviourSingleton] Initializing Singleton in Awake");
                instance = this as T;
                instance.Init();

				if (persist)
					DontDestroyOnLoad(gameObject);
			}
        }

        /// <summary>
        /// Override this in child classes instead of Awake()
        /// </summary>
        virtual protected void Init() { }

        public void OnApplicationQuit() {
            instance = null;
        }
        
        private static void createInstance() {

			Debug.Log(
                "[MonoBehaviourSingleton] Finding instance of '" + 
                typeof(T).ToString() + "' object."
            );
            instance = FindObjectOfType(typeof(T)) as T;
            // This should only occur if 'T' hasn't been attached to any game
            // objects in the scene.
            if(instance == null)
            {
                if (typeof(IAutoSingleton).IsAssignableFrom(typeof(T)))
                {
                    var gameObject = new GameObject();
                    instance = gameObject.AddComponent<T>();
                    instance.transform.name =
                        ((IAutoSingleton)instance).AutoSingletonName;
				}
				else {
                    Debug.LogError(
                        "[MonoBehaviourSingleton] No instance of " +
                        typeof(T).ToString() + " found!"
                    );
                    Debug.LogError(
                        "[MonoBehaviourSingleton] Implement IAutoSingleton " +
                        " for an auto-creating singleton"
                    );
                    instance = null;
                    return;
                }                        
            }
            instance.Init();
        }
    }
}
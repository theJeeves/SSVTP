using UnityEngine;
using System.Collections;

public class Singleton<Type> : MonoBehaviour where Type : MonoBehaviour {

    private static Type _instance;

    public static Type Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<Type>();

                if (_instance == null) {
                    GameObject singleton = new GameObject(typeof(Type).Name);
                    _instance = singleton.AddComponent<Type>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake() {
        if (_instance == null) {
            _instance = this as Type;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Debug.Log("destroyed");
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum WindowIDs {
    StartWindow = 0,
    PauseWindow = 1,
    GameOver = 2
}

public class GenericWindow : MonoBehaviour {

    protected EventSystem _eventSystem {
        get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
    }

    protected GameManager _gameManager;
    protected WindowManager _windowManager;

    [SerializeField]
    private GameObject _firstSelected;
    public GameObject FirstSelected {
        get { return _firstSelected; }
    }


    public virtual void OnFocus() {
        _eventSystem.SetSelectedGameObject(_firstSelected);
    }

    protected virtual void Display(bool value) {
        gameObject.SetActive(value);
    }

    public virtual void Open() {
        Display(true);
        OnFocus();
    }

    public virtual void Close() {
        Display(false);
    }

    protected virtual void Awake() {
        _gameManager = GameManager.Instance;
        _windowManager = WindowManager.Instance;
    }
}


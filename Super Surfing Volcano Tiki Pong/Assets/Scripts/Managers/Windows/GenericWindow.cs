using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum WindowIDs {
    MainMenuWindow = 0,
    PauseWindow = 1,
    Credits = 2,
    Options = 3,
    Won = 4,
    None = -1,
}

public class GenericWindow : MonoBehaviour {

    protected GameManager _gameManager;
    protected WindowManager _windowManager;
    protected EventSystem _eventSystem;

    [SerializeField]
    private GameObject _firstSelected;

    public virtual void Select() {
        _eventSystem.SetSelectedGameObject(_firstSelected);
    }

    public virtual void Deselect() {
        _eventSystem.SetSelectedGameObject(null);
    }

    protected virtual void Display(bool value) {
        gameObject.SetActive(value);
    }

    public virtual void Open() {
        Display(true);
        Select();
    }

    public virtual void Close() {
        Display(false);
        Deselect();
    }

    protected virtual void Awake() {
        _gameManager = GameManager.Instance;
        _windowManager = WindowManager.Instance;
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
}


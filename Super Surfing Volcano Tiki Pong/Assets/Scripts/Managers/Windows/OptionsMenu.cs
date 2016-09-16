using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : GenericWindow {

    public delegate void OptionsMenuEvent();
    public static event OptionsMenuEvent OnDisplayUI;

    [SerializeField]
    private Toggle UIToggle;

    void Start() {
        UIToggle.isOn = _gameManager.Profile.DisplayUI;
    }

    public void OnValueChanged() {
        _gameManager.Profile.DisplayUI = UIToggle.isOn;
        OnDisplayUI();
    }
}

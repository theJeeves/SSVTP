using UnityEngine;
using System.Collections;

public class ToggleUI : MonoBehaviour {

    private bool _displayUI;
    public bool DisplayUI {
        get { return _displayUI; }
        set { _displayUI = value; }
    }

    void Awake() {
        _displayUI = false;
    }
}

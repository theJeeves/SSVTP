using UnityEngine;
using System.Collections;

public class WindowManager : Singleton<WindowManager> {

    private GameManager _gameManager;

    [SerializeField]
    private WindowIDs defaultWindowID;
    [SerializeField]
    private WindowIDs currentWindowID;

    [SerializeField]
    private GenericWindow[] windows;

    public GenericWindow GetWindow(int windowID) {
        return windows[windowID];
    }

    private void ToggleVisability(int windowID) {
        if (windows[windowID].gameObject.activeInHierarchy) {
            windows[windowID].Close();
        }
        else if (!windows[windowID].gameObject.activeInHierarchy) {
            windows[windowID].Open();
        }
    }

    public GenericWindow Open(WindowIDs windowID) {

        currentWindowID = windowID;
        ToggleVisability((int)currentWindowID);

        return GetWindow((int)currentWindowID);
    }

	// Use this for initialization
	public override void Awake () {
        _gameManager = GameManager.Instance;
        Open(defaultWindowID);
    }

    void Update() {
        if (_gameManager.GameState == GameStates.Playing) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (!windows[(int)WindowIDs.PauseWindow].gameObject.activeInHierarchy) {
                    windows[(int)WindowIDs.PauseWindow].Open();
                }
                else if (windows[(int)WindowIDs.PauseWindow].gameObject.activeInHierarchy) {
                    windows[(int)WindowIDs.PauseWindow].Close();
                }
            }
        }
    }
}

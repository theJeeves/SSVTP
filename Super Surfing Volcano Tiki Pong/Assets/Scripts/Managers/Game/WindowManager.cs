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

    private void OnEnable() {
        StartMenu.OnStartGame += ToggleWindows;
        StartMenu.OnCredits += ToggleWindows;
        PauseMenu.OnReturnToMainMenu += ToggleWindows;
        PauseMenu.OnResume += ToggleWindows;
    }

    private void OnDisable() {
        StartMenu.OnStartGame -= ToggleWindows;
        StartMenu.OnCredits -= ToggleWindows;
        PauseMenu.OnReturnToMainMenu -= ToggleWindows;
        PauseMenu.OnResume -= ToggleWindows;
    }

    public override void Awake() {
        _gameManager = GameManager.Instance;
        ToggleWindows(WindowIDs.None, WindowIDs.StartWindow);
    }

    void Update() {
        if (_gameManager.GameState == GameStates.Playing ||
            _gameManager.GameState == GameStates.Paused) {

            if (Input.GetKeyDown(KeyCode.Escape)) {

                if (!windows[(int)WindowIDs.PauseWindow].gameObject.activeInHierarchy) {
                    ToggleWindows(WindowIDs.None, WindowIDs.PauseWindow);
                    _gameManager.GameState = GameStates.Paused;
                }
                else if (windows[(int)WindowIDs.PauseWindow].gameObject.activeInHierarchy) {
                    ToggleWindows(WindowIDs.PauseWindow, WindowIDs.None);
                    _gameManager.GameState = GameStates.Playing;
                }
            }
        }
        if (_gameManager.GameState == GameStates.InMenu) {

            if (Input.anyKeyDown) {
                if (currentWindowID == WindowIDs.Credits || currentWindowID == WindowIDs.Options) {
                    ToggleWindows(currentWindowID, WindowIDs.StartWindow);
                }
            }
        }
    }

    //public GenericWindow GetWindow(WindowIDs windowID) {
    //    return windows[(int)windowID];
    //}

    private void ToggleWindows(WindowIDs close, WindowIDs open) {
        if (close != WindowIDs.None) {
            windows[(int)close].Close();
        }
        currentWindowID = open;

        if (currentWindowID != WindowIDs.None) {
            windows[(int)currentWindowID].Open();
        }
    }
}

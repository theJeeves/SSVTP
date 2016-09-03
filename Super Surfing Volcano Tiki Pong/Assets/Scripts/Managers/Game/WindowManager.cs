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
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnGameWon += OnGameOver;
    }

    private void OnDisable() {
        StartMenu.OnStartGame -= ToggleWindows;
        StartMenu.OnCredits -= ToggleWindows;
        PauseMenu.OnReturnToMainMenu -= ToggleWindows;
        PauseMenu.OnResume -= ToggleWindows;
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnGameWon -= OnGameOver;
    }

    public override void Awake() {
        _gameManager = GameManager.Instance;
        ToggleWindows(WindowIDs.None, WindowIDs.MainMenuWindow);
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
        if (_gameManager.GameState == GameStates.InMenu && currentWindowID == WindowIDs.Credits) {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.KeypadEnter) && !Input.GetKeyDown(KeyCode.Return)) {
                ToggleWindows(WindowIDs.Credits, WindowIDs.MainMenuWindow);
            }
        }
    }

    private void ToggleWindows(WindowIDs close, WindowIDs open) {
        if (close != WindowIDs.None) {
            windows[(int)close].Close();
        }
        currentWindowID = open;

        if (currentWindowID != WindowIDs.None) {
            windows[(int)currentWindowID].Open();
        }
    }

    private void OnGameOver() {
        StartCoroutine(BackToMainMenu());
    }

    private IEnumerator BackToMainMenu() {
        while (!_gameManager.GameOver) {
            yield return 0;
        }

        if (_gameManager.GameState == GameStates.Won) {

            ToggleWindows(WindowIDs.None, WindowIDs.Credits);
            while (_gameManager.GameOver) {
                yield return 0;
            }
            ToggleWindows(WindowIDs.Credits, WindowIDs.MainMenuWindow);
        }
        else {
            ToggleWindows(WindowIDs.None, WindowIDs.MainMenuWindow);
        }
    }
}

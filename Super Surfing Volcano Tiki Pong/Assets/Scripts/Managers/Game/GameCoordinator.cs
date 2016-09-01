using UnityEngine;
using System.Collections;

public class GameCoordinator : MonoBehaviour {

    [SerializeField]
    private float _displayDelay = 3.0f;
    [SerializeField]
    private float _fasterTransitionSpeed = 0.0f;
    [SerializeField]
    private float _normalTransisitonSpeed = 0.0f;

    [SerializeField]
    private GameObject _surferGirl;
    [SerializeField]
    private GameObject _tiki;
    [SerializeField]
    private GameObject _UI;

    private GameManager _gameManager;

    // Use this for initialization
    void Start() {
        _gameManager = GameManager.Instance;
    }

    private void OnEnable() {
        StartMenu.OnStartGame += OnStartGame;
        PauseMenu.OnResume += OnResume;
        PauseMenu.OnReturnToMainMenu += OnReturnToMainMenu;
        GameManager.GameOver += OnGameOver;
    }

    private void OnDisable() {
        StartMenu.OnStartGame -= OnStartGame;
        PauseMenu.OnResume -= OnResume;
        PauseMenu.OnReturnToMainMenu -= OnReturnToMainMenu;
        GameManager.GameOver -= OnGameOver;
    }

    private void OnStartGame(WindowIDs close, WindowIDs open) {
        StartCoroutine(GameSetup());
    }

    private IEnumerator GameSetup() {
        while (_surferGirl.transform.position.x >= -120) {
            _surferGirl.transform.Translate(Vector2.left * _fasterTransitionSpeed * Time.deltaTime);
            yield return 0;
        }
        while (_tiki.transform.position.x >= 114) {
            _tiki.transform.Translate(Vector2.left * _normalTransisitonSpeed * Time.deltaTime);
            yield return 0;
        }

        _UI.SetActive(true);
        _gameManager.ResetAttack = true;
        _gameManager.GameState = GameStates.Playing;
    }

    private void OnResume(WindowIDs close, WindowIDs open) {
        _gameManager.GameState = GameStates.Playing;
    }

    private void OnReturnToMainMenu(WindowIDs close, WindowIDs open) {
        _surferGirl.transform.position = new Vector3(0, -36.34501f, _surferGirl.transform.position.z);
        _tiki.transform.position = new Vector3(180, 11, 0);
        _UI.SetActive(false);
        _gameManager.GameState = GameStates.InMenu;
    }

    private void OnGameOver() {
        _surferGirl.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(GameOverSurferGirl());
        StartCoroutine(GameOverTiki());
        _UI.SetActive(false);
    }

    private IEnumerator GameOverSurferGirl() {
        while (Camera.main.WorldToViewportPoint(_surferGirl.transform.position).y > -0.15f) {
            _surferGirl.transform.Translate(Vector2.down * _normalTransisitonSpeed * Time.deltaTime);
            yield return 0;
        }
        _surferGirl.GetComponent<CircleCollider2D>().enabled = true;
    }

    private IEnumerator GameOverTiki() {
        while (Camera.main.WorldToViewportPoint(_tiki.transform.position).x > -0.1f) {
            _tiki.transform.Translate(Vector2.left * _fasterTransitionSpeed * Time.deltaTime);
            yield return 0;
        }

        float baselineTime = Time.time;
        while (Time.time - baselineTime < _displayDelay) {
            yield return 0;
        }
        _gameManager.ResetGame(WindowIDs.None, WindowIDs.None);
        OnReturnToMainMenu(WindowIDs.None, WindowIDs.None);
    }
}

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
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnGameWon += OnGameWon;
    }

    private void OnDisable() {
        StartMenu.OnStartGame -= OnStartGame;
        PauseMenu.OnResume -= OnResume;
        PauseMenu.OnReturnToMainMenu -= OnReturnToMainMenu;
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnGameWon -= OnGameWon;
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
        DisableUI();
        _gameManager.GameState = GameStates.InMenu;
    }

    private void OnGameOver() {
        _surferGirl.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(GameOverSurferGirl());
        StartCoroutine(GameOverTiki());
        DisableUI();
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

        yield return new WaitForSeconds(_displayDelay);

        _gameManager.ResetGame(WindowIDs.None, WindowIDs.None);
        OnReturnToMainMenu(WindowIDs.None, WindowIDs.None);
        _gameManager.GameOver = true;
    }

    private void OnGameWon() {
        StartCoroutine(GameWonSurferGirl());
        StartCoroutine(GameWonTiki());
        DisableUI();
    }

    private IEnumerator GameWonSurferGirl() {
        while (_surferGirl.transform.position.x <= 0.0f) {
            _surferGirl.transform.Translate(Vector2.right * (_normalTransisitonSpeed * 0.75f) * Time.deltaTime);
            yield return 0;
        }
        while (Camera.main.WorldToViewportPoint(_tiki.transform.position).x < 1.1f) {
            yield return 0;
        }

        yield return new WaitForSeconds(_displayDelay - 1.0f);
        _gameManager.GameOver = true;
        yield return new WaitForSeconds(_displayDelay);

        _gameManager.ResetGame(WindowIDs.None, WindowIDs.None);
        OnReturnToMainMenu(WindowIDs.None, WindowIDs.None);
    }

    private IEnumerator GameWonTiki() {
        while (Camera.main.WorldToViewportPoint(_tiki.transform.position).x < 1.1f) {
            _tiki.transform.Translate(Vector2.right * _normalTransisitonSpeed * Time.deltaTime);
            yield return 0;
        }
    }

    private void DisableUI() {
        _UI.SetActive(false);
    }
}

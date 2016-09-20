using UnityEngine;
using System.Collections;

public enum GameStates {
    GameOver = -1,
    Playing = 0,
    Won = 1,
    Paused = 10,
    InMenu = 5
}

public class GameManager : Singleton<GameManager> {

    public delegate void GameManagerEvent();
    public static event GameManagerEvent OnGSPlaying;
    public static event GameManagerEvent OnGameOver;
    public static event GameManagerEvent OnGameWon;
    public static event GameManagerEvent OnInMenu;

    public class PlayerProfile {

        private bool _displayUI = false;
        public bool DisplayUI {
            get { return _displayUI; }
            set {
                PlayerPrefs.SetInt("DisplayUI", value == true ? 1 : 0);
                _displayUI = PlayerPrefs.GetInt("DisplayUI") == 1 ? true : false;
            }
        }
    }

    private PlayerProfile _profile;
    public PlayerProfile Profile {
        get { return _profile; }
    }

    private int _maxTikiHealth = 165;
    public int MaxTikiHealth {
        get { return _maxTikiHealth; }
    }

    [SerializeField]
    private int _tikiHealth;
    public int TikiHealth {
        get { return _tikiHealth; }
        set { _tikiHealth = value; }
    }

    private int _maxVolcanoHealth = 3;

    [SerializeField]
    private int _volcanoHealth;
    public int VolcanoHealth {
        get { return _volcanoHealth; }
    }

    private bool _gameOver;
    public bool GameOver {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    [SerializeField]
    private GameStates _gameState;
    public GameStates GameState {
        get { return _gameState; }
        set {
            _gameState = value;
            if (value == GameStates.Playing && OnGSPlaying != null) {
                OnGSPlaying();
            }
            else if (value == GameStates.InMenu && OnInMenu != null) {
                OnInMenu();
            }
            else if (value == GameStates.GameOver && OnGameOver != null) {
                OnGameOver();
            }
            else if (value == GameStates.Won && OnGameWon != null) {
                OnGameWon();
            }
            if (value != GameStates.Paused) {
                Time.timeScale = _defaultTimeScale;
            }
            else {
                Time.timeScale = 0.0f;
            }
        }
    }

    private float _defaultTimeScale;

    // Use this for initialization
    public override void Awake () {
        base.Awake();
        _gameState = GameStates.InMenu;
        ResetGame(WindowIDs.None, WindowIDs.None);
    }

    void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += DecrementVolcanoHealth;
        FBCollisionEvent.onTikiCollision += DecrementTikiHealth;
        StartMenu.OnStartGame += ResetGame;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= DecrementVolcanoHealth;
        FBCollisionEvent.onTikiCollision -= DecrementTikiHealth;
        StartMenu.OnStartGame -= ResetGame;
    }

    void Start() {
        _profile = new PlayerProfile();
        _profile.DisplayUI = PlayerPrefs.GetInt("DisplayUI") == 1 ? true : false;
    }

    public void ResetGame(WindowIDs close, WindowIDs open) {
        _volcanoHealth = _maxVolcanoHealth;
        _tikiHealth = _maxTikiHealth;
        _defaultTimeScale = 1.0f;
        _gameOver = false;
    }

    private void DecrementVolcanoHealth() {
        if (--_volcanoHealth < 0) {
            GameState = GameStates.GameOver;
        }
    }

    private void DecrementTikiHealth() {
        _tikiHealth -= 11;
        if (_tikiHealth <= 0) {
            GameState = GameStates.Won;
        }
    }
}

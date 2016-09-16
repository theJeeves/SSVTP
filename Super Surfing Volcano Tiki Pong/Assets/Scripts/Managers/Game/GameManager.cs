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
    public static event GameManagerEvent OnGameOver;
    public static event GameManagerEvent OnGameWon;

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

    private float _attackDelay = 1.0f;

    private int _maxTikiHealth = 165;
    public int MaxTikiHealth {
        get { return _maxTikiHealth; }
    }

    [SerializeField]
    private int _tikiHP;
    public int TikiHP {
        get { return _tikiHP; }
        set { _tikiHP = value; }
    }

    private int _maxVolcanoHealth = 3;

    [SerializeField]
    private int _volcanoHealth;
    public int VolcanoHealth {
        get { return _volcanoHealth; }
    }

    private bool _tikiDamageTaken;
    public bool TikiDamageTaken {
        get { return _tikiDamageTaken; }
        set { _tikiDamageTaken = value; }
    }

    private bool _resetAttack;
    public bool ResetAttack {
        get { return _resetAttack; }
        set { _resetAttack = value; }
    }

    [SerializeField]
    private GameStates _gameState;
    public GameStates GameState {
        get { return _gameState; }
        set { _gameState = value; }
    }

    private bool _hitSurferGirl;
    public bool HitSurferGirl {
        get { return _hitSurferGirl; }
        set { _hitSurferGirl = value; }
    }

    private bool _hitTiki;
    public bool HitTiki {
        get { return _hitTiki; }
        set { _hitTiki = value; }
    }

    private bool _hitLeftWall;
    public bool HitLeftWall {
        get { return _hitLeftWall; }
    }

    private bool _gameOver;
    public bool GameOver {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    private float _defaultTimeScale;

    // Use this for initialization
    public override void Awake () {
        base.Awake();
        _gameState = GameStates.InMenu;
        ResetGame(WindowIDs.None, WindowIDs.None);
    }

    void Start() {
        _profile = new PlayerProfile();
        _profile.DisplayUI = PlayerPrefs.GetInt("DisplayUI") == 1 ? true : false;
    }

    void Update() {
        if (_gameState == GameStates.Paused) {
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = _defaultTimeScale;
        }
    }

    void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision += BounceFB;
        FBCollisionEvent.onTikiCollision += DecrementTikiHealth;
        StartMenu.OnStartGame += ResetGame;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision -= BounceFB;
        FBCollisionEvent.onTikiCollision -= DecrementTikiHealth;
        StartMenu.OnStartGame -= ResetGame;
    }

    public void ResetGame(WindowIDs close, WindowIDs open) {
        _resetAttack = false;
        _hitTiki = false;
        _hitSurferGirl = false;
        _hitLeftWall = false;
        _volcanoHealth = _maxVolcanoHealth;
        _tikiDamageTaken = false;
        _tikiHP = _maxTikiHealth;
        _defaultTimeScale = 1.0f;
        _gameOver = false;
    }

    private void BounceFB(GameObject surferGirl) {
        _hitSurferGirl = true;
        surferGirl.GetComponent<CollisionState>().BlockedFB = true;
    }

    private void DecrementVolcanoHealth(GameObject leftWall) {
        _hitLeftWall = true;
        if (--_volcanoHealth < 0) {
            _gameState = GameStates.GameOver;
            OnGameOver();
        }
        else {
            StartCoroutine(ResetDelay(_attackDelay));
        }
    }

    private void DecrementTikiHealth(GameObject tiki) {
        _hitTiki = true;
        _tikiHP -= 11;
        if (_tikiHP <= 0) {
            _gameState = GameStates.Won;
            OnGameWon();
        }
    }

    private IEnumerator ResetDelay(float delay) {
        yield return new WaitForSeconds(delay);
        _hitLeftWall = false;
        _resetAttack = true;
    }
}

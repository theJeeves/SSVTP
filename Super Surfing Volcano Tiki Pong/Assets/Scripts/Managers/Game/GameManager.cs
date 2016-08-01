using UnityEngine;
using System.Collections;

public enum GameStates {
    GameOver = -1,
    Playing = 0,
    Won = 1
}

public class GameManager : Singleton<GameManager> {

    private int _maxTikiHealth = 9;
    private int _maxVolcanoHealth = 3;
    private float resetDelay = 1.0f;

    [SerializeField]
    private int _tikiHP;
    public int TikiHP {
        get { return _tikiHP; }
        set { _tikiHP = value; }
    }

    [SerializeField]
    private bool _tikiDamageTaken;
    public bool TikiDamageTaken {
        get { return _tikiDamageTaken; }
        set { _tikiDamageTaken = value; }
    }

    private bool _reset;
    public bool Reset {
        get { return _reset; }
        set { _reset = value; }
    }

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
    private int _volcanoHealth;
    public int VolcanoHealth {
        get { return _volcanoHealth; }
    }

	// Use this for initialization
	void Start () {
        _reset = true;
        _gameState = 0;
        _hitSurferGirl = false;
        _hitLeftWall = false;
        _volcanoHealth = _maxVolcanoHealth;
        _tikiDamageTaken = false;
        _tikiHP = _maxTikiHealth;
	}
	
	void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision += BounceFB;
        FBCollisionEvent.onTikiCollision += DecrementTikiHealth;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision -= BounceFB;
        FBCollisionEvent.onTikiCollision -= DecrementTikiHealth;
    }

    private void DecrementVolcanoHealth(GameObject leftWall) {
        _hitLeftWall = true;
        if (--_volcanoHealth < 0) {
            _gameState = GameStates.GameOver;
        }
        else {
            StartCoroutine(ResetDelay());
        }
    }

    private void BounceFB(GameObject surferGirl) {
        _hitSurferGirl = true;
        surferGirl.GetComponent<CollisionState>().BlockedFB = true;
    }

    private void DecrementTikiHealth(GameObject tiki) {
        _hitTiki = true;

        if (--_tikiHP <= 0) {
            _gameState = GameStates.Won;
        }
    }

    private IEnumerator ResetDelay() {
        yield return new WaitForSeconds(resetDelay);
        _hitLeftWall = false;
        _reset = true;
    }
}

using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private float resetDelay = 1.0f;

    private int maxVolcanoHealth = 3;

    private bool _reset;
    public bool Reset {
        get { return _reset; }
        set { _reset = value; }
    }

    private int _gameState;
    public int GameState {
        get { return _gameState; }
        set { _gameState = value; }
    }
    private bool _hitSurferGirl;
    public bool HitSurferGirl {
        get { return _hitSurferGirl; }
        set { _hitSurferGirl = value; }
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
        _volcanoHealth = maxVolcanoHealth;
	}
	
	void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision += BounceFB;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= DecrementVolcanoHealth;
        FBCollisionEvent.onSGCollision -= BounceFB;
    }

    private void DecrementVolcanoHealth(GameObject leftWall) {
        _hitLeftWall = true;
        if (--_volcanoHealth < 0) {
            _gameState = -1;
        }
        else {
            StartCoroutine(ResetDelay());
        }
    }

    private void BounceFB(GameObject surferGirl) {
        _hitSurferGirl = true;
        surferGirl.GetComponent<CollisionState>().BlockedFB = true;
    }

    private IEnumerator ResetDelay() {
        yield return new WaitForSeconds(resetDelay);
        _hitLeftWall = false;
        _reset = true;
    }
}

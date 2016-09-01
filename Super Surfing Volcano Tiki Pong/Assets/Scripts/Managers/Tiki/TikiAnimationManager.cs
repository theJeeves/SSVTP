using UnityEngine;
using System.Collections;

public enum TikiAnimations {
    Attacking = 0,
    Resting = 1,
    Damaged = 2
}

public class TikiAnimationManager : MonoBehaviour {

    public delegate void TikiEvent();

    public static event TikiEvent ToggleUI;

    [SerializeField]
    private float damageDelay;

    private GameManager _gameManager;
    private Animator _animator;

	// Use this for initialization
	void Start () {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<Animator>();
        ChangeAnimationState(TikiAnimations.Resting);
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Playing ||
            _gameManager.GameState == GameStates.GameOver) {

            if (_gameManager.HitTiki) {
                _gameManager.TikiDamageTaken = _gameManager.TikiHP % 33 == 0 ? true : false;
            }
            if (!_gameManager.TikiDamageTaken) {
                ChangeAnimationState(TikiAnimations.Attacking);
            }
            else if (_gameManager.TikiDamageTaken) {
                ChangeAnimationState(TikiAnimations.Damaged);
                StartCoroutine(DamageDelay());
            }
        }
        else if (_gameManager.GameState == GameStates.Won) {
            ChangeAnimationState(TikiAnimations.Damaged);
        }
        else if (_gameManager.GameState == GameStates.GameOver ||
            _gameManager.GameState == GameStates.InMenu) {
            ChangeAnimationState(TikiAnimations.Resting);
        }
	}

    public void ChangeAnimationState(TikiAnimations animationState) {
        _animator.SetInteger("AnimationState", (int)animationState);
    }

    private IEnumerator DamageDelay() {
        yield return new WaitForSeconds(damageDelay);
        _gameManager.TikiDamageTaken = false;
    }
}

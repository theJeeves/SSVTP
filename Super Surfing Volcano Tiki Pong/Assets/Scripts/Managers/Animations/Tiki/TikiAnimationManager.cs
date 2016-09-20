using UnityEngine;
using System.Collections;

public enum TikiAnimations {
    Attacking = 0,
    Resting = 1,
    Damaged = 2
}

public class TikiAnimationManager : MonoBehaviour {

    [SerializeField]
    private float damageDelay;

    private GameManager _gameManager;
    private Animator _animator;

    void OnEnable() {
        GameManager.OnInMenu += ResetingAnimation;
        GameManager.OnGSPlaying += AttackingAnimation;
        GameManager.OnGameOver += AttackingAnimation;
        GameManager.OnGameWon += DamagedAnimation;
        FBCollisionEvent.onTikiCollision += DamagedAnimation;
    }

    void OnDisable() {
        GameManager.OnInMenu -= ResetingAnimation;
        GameManager.OnGSPlaying -= AttackingAnimation;
        GameManager.OnGameOver -= AttackingAnimation;
        GameManager.OnGameWon -= DamagedAnimation;
        FBCollisionEvent.onTikiCollision -= DamagedAnimation;
    }

	// Use this for initialization
	void Start () {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<Animator>();
        ChangeAnimationState(TikiAnimations.Resting);
	}

    private void AttackingAnimation() {
        ChangeAnimationState(TikiAnimations.Attacking);
    }

    private void DamagedAnimation() {
        if (_gameManager.TikiHealth % 33 == 0) {
            ChangeAnimationState(TikiAnimations.Damaged);
            if (_gameManager.GameState == GameStates.Playing) {
                StartCoroutine(DamageDelay());
            }
        }
    }

    private void ResetingAnimation() {
        ChangeAnimationState(TikiAnimations.Resting);
    }

    public void ChangeAnimationState(TikiAnimations animationState) {
        _animator.SetInteger("AnimationState", (int)animationState);
    }

    private IEnumerator DamageDelay() {
        yield return new WaitForSeconds(damageDelay);
        ChangeAnimationState(TikiAnimations.Attacking);
    }
}

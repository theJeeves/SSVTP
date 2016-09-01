using UnityEngine;
using System.Collections;

public class FBAnimationManager : MonoBehaviour {

    private Animator _animatorComponent;
    private bool _hitLeftWall;
    private GameManager _gameManager;

    [SerializeField]
    private AnimationClip FBDestoryAnimation;

	void Awake () {
        _gameManager = GameManager.Instance;
        _animatorComponent = GetComponent<Animator>();
        _hitLeftWall = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Playing) {
            if (!_hitLeftWall) {
                ChangeAnimationState(0);
            }
            if (_hitLeftWall) {
                ChangeAnimationState(1);
                Destroy(gameObject, FBDestoryAnimation.length);
            }
        }
        else if (_gameManager.GameState == GameStates.Won) {
            ChangeAnimationState(1);
            Destroy(gameObject, FBDestoryAnimation.length);
        }
        else if (_gameManager.GameState == GameStates.GameOver) {
            ChangeAnimationState(1);
            Destroy(gameObject, FBDestoryAnimation.length);
        }
	}

    void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += OnPlayerDamage;
        PauseMenu.OnReturnToMainMenu += DestroyFireball;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= OnPlayerDamage;
        PauseMenu.OnReturnToMainMenu -= DestroyFireball;
    }

    private void OnPlayerDamage(GameObject leftWall) {
        _hitLeftWall = true;
    }

    private void DestroyFireball(WindowIDs close, WindowIDs open) { 
        Destroy(gameObject);
    }

    private void ChangeAnimationState(int animationNum) {
        _animatorComponent.SetInteger("AnimationState", animationNum);
    }
}

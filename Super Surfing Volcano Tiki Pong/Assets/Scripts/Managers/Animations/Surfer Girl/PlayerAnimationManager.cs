using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    public float blockAnimationDuration;

    private GameManager _gameManager;
    private Animator _animator;
    private CollisionState _collisionState;

    void OnEnable() {
        FBCollisionEvent.onSGCollision += BlockFB;
    }

    void OnDisable() {
        FBCollisionEvent.onSGCollision -= BlockFB;
    }

    void Start() {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<Animator>();
        _collisionState = GetComponent<CollisionState>();
    }

    private void BlockFB() {
        ChangeAnimationState(1);
        StartCoroutine(BlockAnimationDelay());
    }

    //Update is called once per frame
    void Update() {
        if (_gameManager.GameState == GameStates.Playing) {
            if (_collisionState.surfing) {
                ChangeAnimationState(0);
            }
            if (!_collisionState.surfing) {
                ChangeAnimationState(1);
            }
        }
        else if (_gameManager.GameState == GameStates.InMenu ||
            _gameManager.GameState == GameStates.Won) {
            ChangeAnimationState(0);
        }
        else if (_gameManager.GameState == GameStates.GameOver) {
            ChangeAnimationState(1);
        }
    }

    void ChangeAnimationState(int animationNum) {
        _animator.SetInteger("AnimationState", animationNum);
    }

    private IEnumerator BlockAnimationDelay() {
        yield return new WaitForSeconds(blockAnimationDuration);
    }
}

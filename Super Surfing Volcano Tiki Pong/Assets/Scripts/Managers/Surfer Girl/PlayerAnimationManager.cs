using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    public float blockAnimationDuration;

    private Animator _animator;
    private CollisionState _collisionState;

    void Awake() {
        _animator = GetComponent<Animator>();
        _collisionState = GetComponent<CollisionState>();
    }

    // Update is called once per frame
    void Update() {

        if (_collisionState.surfing) {
            ChangeAnimationState(0);
        }
        if (!_collisionState.surfing) {
            ChangeAnimationState(1);
        }
        if (_collisionState.BlockedFB) {
            ChangeAnimationState(1);
            StartCoroutine(BlockAnimationDelay());
        }
    }

    void ChangeAnimationState(int animationNum) {
        _animator.SetInteger("AnimationState", animationNum);
    }

    private IEnumerator BlockAnimationDelay() {
        yield return new WaitForSeconds(blockAnimationDuration);
        _collisionState.BlockedFB = false;
    }
}

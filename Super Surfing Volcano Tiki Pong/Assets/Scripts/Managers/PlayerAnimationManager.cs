using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    public float blockAnimationDuration;

    private Animator animator;
    private CollisionState collisionState;

    void Awake() {
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
    }

    // Update is called once per frame
    void Update() {

        if (collisionState.surfing) {
            ChangeAnimationState(0);
        }
        if (!collisionState.surfing) {
            ChangeAnimationState(1);
        }
        if (collisionState.BlockedFB) {
            ChangeAnimationState(1);
        }
    }

    void ChangeAnimationState(int animationNum) {
        animator.SetInteger("AnimationState", animationNum);
    }
}

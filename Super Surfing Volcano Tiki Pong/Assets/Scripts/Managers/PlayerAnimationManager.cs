using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    private Animator animator;
    private CollisionState collisionState;
    private InputState controllableCharacter;

    void Awake() {
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
        controllableCharacter = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update() {

        if (collisionState.surfing) {
            ChangeAnimationState(0);
        }
        if (!collisionState.surfing) {
            ChangeAnimationState(1);
        }
    }

    void ChangeAnimationState(int animationNum) {
        animator.SetInteger("AnimationState", animationNum);
    }
}

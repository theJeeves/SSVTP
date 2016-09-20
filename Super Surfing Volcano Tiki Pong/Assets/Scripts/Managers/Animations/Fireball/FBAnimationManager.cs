using UnityEngine;
using System.Collections;

public class FBAnimationManager : MonoBehaviour {

    private Animator _animatorComponent;

    [SerializeField]
    private AnimationClip FBDestoryAnimation;

	void Awake () {
        _animatorComponent = GetComponent<Animator>();
	}

    void OnEnable() {
        GameManager.OnGameWon += AnimationDestory;
        GameManager.OnGameOver += AnimationDestory;
        FBCollisionEvent.onDamagePlayerCollision += OnPlayerDamage;
        PauseMenu.OnReturnToMainMenu += QuickDestory;

        ChangeAnimationState(0);
    }

    void OnDisable() {
        GameManager.OnGameWon -= AnimationDestory;
        GameManager.OnGameOver -= AnimationDestory;
        FBCollisionEvent.onDamagePlayerCollision -= OnPlayerDamage;
        PauseMenu.OnReturnToMainMenu -= QuickDestory;
    }

    private void OnPlayerDamage() {
        ChangeAnimationState(1);
        Destroy(gameObject, FBDestoryAnimation.length);
    }

    private void AnimationDestory() {
        ChangeAnimationState(1);
        Destroy(gameObject, FBDestoryAnimation.length);
    }

    private void QuickDestory(WindowIDs close, WindowIDs open) { 
        Destroy(gameObject);
    }

    private void ChangeAnimationState(int animationNum) {
        _animatorComponent.SetInteger("AnimationState", animationNum);
    }
}

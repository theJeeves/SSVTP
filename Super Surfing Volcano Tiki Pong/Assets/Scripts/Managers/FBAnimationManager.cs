using UnityEngine;
using System.Collections;

public class FBAnimationManager : MonoBehaviour {

    private Animator animatorComponent;
    private bool hitLeftWall;

    [SerializeField]
    private AnimationClip FBDestoryAnimation;

	void Awake () {
        animatorComponent = GetComponent<Animator>();
        hitLeftWall = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!hitLeftWall) {
            ChangeAnimationState(0);
        }
        if (hitLeftWall) {
            ChangeAnimationState(1);
            Destroy(gameObject, FBDestoryAnimation.length);
        }
	}

    void OnEnable() {
        FBCollisionEvent.onDamagePlayerCollision += OnPlayerDamage;
    }

    void OnDisable() {
        FBCollisionEvent.onDamagePlayerCollision -= OnPlayerDamage;
    }

    private void OnPlayerDamage(GameObject leftWall) {
        hitLeftWall = true;
    }

    private void ChangeAnimationState(int animationNum) {
        animatorComponent.SetInteger("AnimationState", animationNum);
    }
}

using UnityEngine;
using System.Collections;

public class VolcanoAnimationManager : MonoBehaviour {

    private bool isAnimationState;
    private Animator animatorComp;

    void Awake() {
        animatorComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.GameState == 0) {

            isAnimationState = true;

            if (GameManager.Instance.VolcanoHealth == 2) {
                ChangeAnimationState(2);
            }
            if (GameManager.Instance.VolcanoHealth == 1) {
                ChangeAnimationState(1);
            }
            if (GameManager.Instance.VolcanoHealth == 0) {
                ChangeAnimationState(0);
            }
        }
        else {
            isAnimationState = false;
            
            if (GameManager.Instance.GameState == 1) {
                ChangeAnimationState(1);
            }
            else if (GameManager.Instance.GameState == -1) {
                ChangeAnimationState(-1);
            }
        }
	}

    private void ChangeAnimationState(int animationNum) {
        if (isAnimationState) {
            animatorComp.SetInteger("AnimationState", animationNum);
        }
        else if (!isAnimationState) {
            animatorComp.SetInteger("GameState", animationNum);
        }
    }
}

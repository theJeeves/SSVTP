using UnityEngine;
using System.Collections;

public class VolcanoAnimationManager : MonoBehaviour {

    private GameManager _gameManager;
    private bool _isAnimationState;
    private Animator _animatorComp;

    void Awake() {
        _gameManager = GameManager.Instance;
        _animatorComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (_gameManager.GameState == GameStates.Playing) {

            _isAnimationState = true;

            if (_gameManager.VolcanoHealth == 2) {
                ChangeAnimationState(2);
            }
            if (_gameManager.VolcanoHealth == 1) {
                ChangeAnimationState(1);
            }
            if (_gameManager.VolcanoHealth == 0) {
                ChangeAnimationState(0);
            }
        }
        else {
            _isAnimationState = false;
            
            if (_gameManager.GameState == GameStates.Won) {
                ChangeAnimationState(1);
            }
            else if (_gameManager.GameState == GameStates.GameOver) {
                ChangeAnimationState(-1);
            }
        }
	}

    private void ChangeAnimationState(int animationNum) {
        if (_isAnimationState) {
            _animatorComp.SetInteger("AnimationState", animationNum);
        }
        else if (!_isAnimationState) {
            _animatorComp.SetInteger("GameState", animationNum);
        }
    }
}

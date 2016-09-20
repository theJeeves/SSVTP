using UnityEngine;
using System.Collections;

public class VolcanoAnimationManager : MonoBehaviour {

    private GameManager _gameManager;
    private Animator _animatorComp;

    void Start() {
        _gameManager = GameManager.Instance;
        _animatorComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (_gameManager.GameState == GameStates.InMenu) {
            ChangeGameState((int)GameStates.InMenu);
        }
        else if (_gameManager.GameState == GameStates.Playing) {
            ChangeGameState((int)GameStates.Playing);

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
        else if (_gameManager.GameState == GameStates.Won) {
            ChangeGameState((int)GameStates.Won);
        }
        else if (_gameManager.GameState == GameStates.GameOver) {
            ChangeGameState((int)GameStates.GameOver);
        }
	}

    private void ChangeAnimationState(int animationNum) {
        _animatorComp.SetInteger("AnimationState", animationNum);
    }

    private void ChangeGameState(int gameStateNum) {
        _animatorComp.SetInteger("GameState", gameStateNum);
    }
}

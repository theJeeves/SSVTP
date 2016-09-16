using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

    public float jumpSpeed = 50.0f;

    private bool _hitSky = false;
    private float _pressedTime = 0.0f;
    private GameManager _gameManager;

    void Start() {
        _gameManager = GameManager.Instance;
    }
	
	// Update is called once per frame
	void Update () {

        if (_gameManager.GameState == GameStates.Playing) {
            if (collisionState.hitSky) {
                _hitSky = true;
            }

            if (!_hitSky) {
                if (collisionState.surfing || _pressedTime > 0.0f) {
                    bool canJump = controllableCharacter.GetButtonPressed(inputButtons[0]);
                    _pressedTime = controllableCharacter.GetButtonPressTime(inputButtons[0]);

                    if (canJump) {
                        OnJump();
                    }
                }
                else if (!collisionState.surfing || _pressedTime <= 0.0f) {
                    DriftDown();
                }
            }
            else if (_hitSky) {
                DriftDown();
            }

            if (collisionState.surfing) {
                _hitSky = false;
            }
        }
        else if (_gameManager.GameState == GameStates.GameOver) {
            body2d.velocity = new Vector2(0, 0);
        }
        else if (_gameManager.GameState == GameStates.Won) {
            DriftDown();
        }

	}

    private void OnJump() {
        Vector2 velocity = body2d.velocity;
        body2d.velocity = new Vector2(velocity.x, jumpSpeed);
    }

    private void DriftDown() {
        Vector2 velocity = body2d.velocity;
        body2d.velocity = new Vector2(velocity.x, -jumpSpeed);
    }
}

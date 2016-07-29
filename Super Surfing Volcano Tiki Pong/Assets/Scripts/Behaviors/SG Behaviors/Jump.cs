using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

    public float jumpSpeed = 50.0f;

    private bool hitSky = false;
    private float pressedTime = 0.0f;
	
	// Update is called once per frame
	void Update () {

        if (collisionState.hitSky) {
            hitSky = true;
        }

        if (!hitSky) {
            if (collisionState.surfing || pressedTime > 0.0f) {
                bool canJump = controllableCharacter.GetButtonPressed(inputButtons[0]);
                pressedTime = controllableCharacter.GetButtonPressTime(inputButtons[0]);

                if (canJump) {
                    OnJump();
                }
            }
            else if (!collisionState.surfing || pressedTime <= 0.0f) {
                DriftDown();
            }
        }
        else if (hitSky) {
            DriftDown();
        }

        if (collisionState.surfing) {
            hitSky = false;
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

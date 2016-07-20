using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Directions
{
    Right = 1,
    Left = -1
}

public class ButtonState
{
    public bool isPressed = false;
    public float pressedTime = 0.0f;
}

public class InputState : MonoBehaviour
{
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();
	
    public void SetButtonState(Buttons key, bool isPressed) {

        if (!buttonStates.ContainsKey(key)) {
            buttonStates.Add(key, new ButtonState());
        }

        ButtonState buttonState = buttonStates[key];

        if (buttonState.isPressed && !isPressed) {
            buttonState.pressedTime = 0.0f;
        }
        else if(buttonState.isPressed && isPressed) {
            buttonState.pressedTime += Time.deltaTime;
        }

        buttonState.isPressed = isPressed;
    }

    public bool GetButtonPressed(Buttons key) {

        if (buttonStates.ContainsKey(key)) {
            return buttonStates[key].isPressed;
        }
        else {
            return false;
        }
    }

    public float GetButtonPressTime(Buttons key) {

        if (buttonStates.ContainsKey(key)) {
            return buttonStates[key].pressedTime;
        }
        else {
            return 0.0f;
        }
    }
}

using UnityEngine;
using System.Collections;

public enum Buttons {

    Jump
}

public enum ButtonCondition {

    GreaterThan, LessThan
}

[System.Serializable]
public class InputAxisState {
    public string axisName;
    public float offValue;
    public Buttons button;
    public ButtonCondition condition;

    public bool axisInputValue {
        get {
            float axisInputValue = Input.GetAxis(axisName);
            switch (condition) {
                case ButtonCondition.GreaterThan:
                    return axisInputValue > offValue;
                case ButtonCondition.LessThan:
                    return axisInputValue < offValue;
            }

            return false;
        }
    }
}

public class InputManager : MonoBehaviour {
    public InputAxisState[] inputs;
    public InputState controllableCharacter;

    void Start() {
    }

    // Update is called once per frame
    void Update () {

        foreach (InputAxisState input in inputs) {
            controllableCharacter.SetButtonState(input.button, input.axisInputValue);
        }
	}
}

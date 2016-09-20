using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TikiHealthManager : MonoBehaviour {

    [SerializeField]
    private Slider tikiHealth;
    [SerializeField]
    private RectTransform fillRect;

    private GameManager _gameManager;
    private Color healthColor = new Color (0.0f, 1.0f, 0.0f, 1.0f);

    void Awake() {
        _gameManager = GameManager.Instance;
        tikiHealth.minValue = 0;
        tikiHealth.maxValue = _gameManager.MaxTikiHealth;
        tikiHealth.wholeNumbers = true;
    }

    void OnEnable() {
        tikiHealth.value = _gameManager.TikiHealth;
        UpdateColor();
        FBCollisionEvent.onTikiCollision += UpdateHealth;
    }

    void OnDisable() {
        FBCollisionEvent.onTikiCollision -= UpdateHealth;
    }

    private void UpdateHealth() {
        DecrementValue();
        UpdateColor();
    }

    private void DecrementValue() {
        tikiHealth.value -= 11;
    }

    private void UpdateColor() {

        if ((int)tikiHealth.value > 132) {
            healthColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }
        else if ((int)tikiHealth.value > 99) {
            healthColor = new Color(0.5f, 1.0f, 0.0f, 1.0f); 
        }
        else if ((int)tikiHealth.value > 66) {
            healthColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        else if ((int)tikiHealth.value > 33) {
            healthColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        }
        else if ((int)tikiHealth.value > 0) {
            healthColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        FillRectColor();
    }

    private void FillRectColor() {
        fillRect.GetComponentInChildren<Image>().color = healthColor;
    }
}

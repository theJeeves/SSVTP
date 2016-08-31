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
    }

	// Use this for initialization
	void Start () {
        tikiHealth.minValue = 0;
        tikiHealth.maxValue = _gameManager.TikiHP;
        tikiHealth.wholeNumbers = true;
        tikiHealth.value = tikiHealth.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Playing) {

            if (_gameManager.HitTiki) {
                if (_gameManager.TikiHP % 33 == 0) { 
                    DecrementHealthValue();
                    DecrementHealthColor();
                }
            }
        }
        if (_gameManager.GameState == GameStates.Won) {
            DecrementHealthValue();
            DecrementHealthColor();
        }
	}

    private void OnEnable() {
        _gameManager.TikiHP = _gameManager.MaxTikiHealth;
        tikiHealth.value = tikiHealth.maxValue;
        healthColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        FillRectColor();
    }

    private void DecrementHealthValue() {
        tikiHealth.value -= 33;
    }

    private void DecrementHealthColor() {
        if (healthColor.r != 1.0) {
            healthColor.r += .5f;
        }
        else {
            healthColor.g -= .5f;
        }

        FillRectColor();
    }

    private void FillRectColor() {
        fillRect.GetComponentInChildren<Image>().color = healthColor;
    }
}

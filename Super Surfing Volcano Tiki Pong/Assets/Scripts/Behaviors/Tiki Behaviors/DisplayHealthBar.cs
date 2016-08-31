using UnityEngine;
using System.Collections;

public class DisplayHealthBar : MonoBehaviour {

    [SerializeField]
    private GameObject _healthBar;

    private void OnEnable() {
        TikiAnimationManager.ToggleUI += OnStart;
        PauseMenu.OnReturnToMain += OnMainMenu;
    }

    private void OnDisable() {
        TikiAnimationManager.ToggleUI -= OnStart;
        PauseMenu.OnReturnToMain += OnMainMenu;
    }

    private void OnStart() {
        _healthBar.SetActive(true);
    }

    private void OnMainMenu() {
        _healthBar.SetActive(false);
    }
}

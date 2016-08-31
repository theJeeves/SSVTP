using UnityEngine;
using System.Collections;

public class PauseMenu : GenericWindow {

    public delegate void ReturnToMain();

    public static event ReturnToMain OnReturnToMain; 

    public override void Open() {
        base.Open();
        _gameManager.GameState = GameStates.Paused;
    }

    public override void Close() {
        _gameManager.GameState = GameStates.Playing;
        base.Close();
    }

    public void BackToMain() {
        base.Close();
        OnReturnToMain();
        _gameManager.GameState = GameStates.InMenu;
        _windowManager.Open(WindowIDs.StartWindow);
    }
}

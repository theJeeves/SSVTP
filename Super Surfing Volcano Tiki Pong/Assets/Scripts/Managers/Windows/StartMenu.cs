using UnityEngine;
using System.Collections;

public class StartMenu : GenericWindow {

    public delegate void StartMenuEvent(WindowIDs close, WindowIDs open);

    public static event StartMenuEvent OnStartGame;
    public static event StartMenuEvent OnCredits;
    public static event StartMenuEvent OnOptionsMenu;

    public void StartGame() {
        OnStartGame(WindowIDs.MainMenuWindow, WindowIDs.None);
    }

    public void DisplayCredits() {
        OnCredits(WindowIDs.MainMenuWindow, WindowIDs.Credits);
    }

    public void OpenOptionsMenu() {
        OnOptionsMenu(WindowIDs.MainMenuWindow, WindowIDs.Options);
    }

    public void OnExit() {
        Application.Quit();
    }
}

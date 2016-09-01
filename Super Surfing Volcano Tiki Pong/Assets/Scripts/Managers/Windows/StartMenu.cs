using UnityEngine;
using System.Collections;

public class StartMenu : GenericWindow {

    public delegate void StartMenuEvent(WindowIDs close, WindowIDs open);

    public static event StartMenuEvent OnStartGame;
    public static event StartMenuEvent OnCredits;

    public void StartGame() {
        OnStartGame(WindowIDs.StartWindow, WindowIDs.None);
    }

    public void DisplayCredits() {
        OnCredits(WindowIDs.StartWindow, WindowIDs.Credits);
    }
}

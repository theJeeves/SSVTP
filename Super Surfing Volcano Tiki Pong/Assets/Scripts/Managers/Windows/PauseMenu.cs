using UnityEngine;
using System.Collections;

public class PauseMenu : GenericWindow {

    public delegate void PauseMenuEvent(WindowIDs close, WindowIDs open);
    public static event PauseMenuEvent OnResume;
    public static event PauseMenuEvent OnReturnToMainMenu;

    public void Resume() {
        OnResume(WindowIDs.PauseWindow, WindowIDs.None);
    }

    public void ReturnToMainMenu() {
        OnReturnToMainMenu(WindowIDs.PauseWindow, WindowIDs.StartWindow);
    }
}

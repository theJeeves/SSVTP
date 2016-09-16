using UnityEngine;
using System.Collections;

public class PauseMenu : GenericWindow {

    public delegate void PauseMenuEvent(WindowIDs close, WindowIDs open);
    public static event PauseMenuEvent OnResume;
    public static event PauseMenuEvent OnReturnToMainMenu;
    public static event PauseMenuEvent OnOptions;

    public void Resume() {
        OnResume(WindowIDs.PauseWindow, WindowIDs.None);
    }

    public void ReturnToMainMenu() {
        OnReturnToMainMenu(WindowIDs.PauseWindow, WindowIDs.MainMenuWindow);
    }

    public void OpenOptionsMenu() {
        GetComponentInChildren<RectTransform>().Find("PauseButtons").gameObject.SetActive(false);
        OnOptions(WindowIDs.PauseWindow, WindowIDs.Options);
    }

    public void CloseOptionsMenu() {
        GetComponentInChildren<RectTransform>().Find("PauseButtons").gameObject.SetActive(true);
        OnOptions(WindowIDs.Options, WindowIDs.PauseWindow);
    }
}

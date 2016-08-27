using UnityEngine;
using System.Collections;

public class PauseMenu : GenericWindow {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public override void Open() {
        base.Open();
        _gameManager.PauseGame();
    }

    public override void Close() {
        _gameManager.ResumeGame();
        base.Close();
    }

    public void BackToMain() {
        base.Close();
        _windowManager.Open(WindowIDs.StartWindow);
    }
}

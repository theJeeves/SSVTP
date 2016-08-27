using UnityEngine;
using System.Collections;

public class StartMenu : GenericWindow {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Open() {
        base.Open();
    }

    public override void Close() {
        base.Close();
        _gameManager.ResumeGame();
        _gameManager.GameState = GameStates.Playing;
    }
}

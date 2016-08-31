using UnityEngine;
using System.Collections;

public class StartMenu : GenericWindow {

    public delegate void StartGame();
    public static event StartGame OnStartGame;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Open() {
        base.Open();
        _gameManager.GameState = GameStates.InMenu;
    }

    public override void Close() {
        base.Close();
        OnStartGame();
        _gameManager.ResetAttack = true;
    }
}

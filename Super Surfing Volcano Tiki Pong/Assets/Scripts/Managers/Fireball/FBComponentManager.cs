using UnityEngine;
using System.Collections;

public class FBComponentManager : MonoBehaviour {

    private GameManager _gameManager;
    private Collider2D _collider2d;

	// Use this for initialization
	void Start () {
        _gameManager = GameManager.Instance;
        _collider2d = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_gameManager.HitLeftWall ||
            _gameManager.GameState == GameStates.Won) {
            _collider2d.enabled = false;
        }
	}
}

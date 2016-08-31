using UnityEngine;
using System.Collections;

public class ShootFB : MonoBehaviour {

    [SerializeField]
    private GameObject Fireball;
    [SerializeField]
    private GameObject FBSpawnPos;

    private GameManager _gameManager;

    void Start() {
        _gameManager = GameManager.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Playing) {
            if (GameManager.Instance.ResetAttack) {
                Instantiate(Fireball, FBSpawnPos.transform.position, Quaternion.identity);
                GameManager.Instance.ResetAttack = false;
            }
        }
	}
}

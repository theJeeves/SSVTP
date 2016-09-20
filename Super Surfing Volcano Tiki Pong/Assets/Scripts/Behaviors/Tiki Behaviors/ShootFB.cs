using UnityEngine;
using System.Collections;

public class ShootFB : MonoBehaviour {

    [SerializeField]
    private GameObject Fireball;
    [SerializeField]
    private GameObject FBSpawnPos;
    [SerializeField]
    private float _attackDelay = 1.0f;

    private GameManager _gameManager;

    void Start() {
        _gameManager = GameManager.Instance;
    }

    void OnEnable() {
        GameManager.OnGSPlaying += TikiAttack;
        FBCollisionEvent.onDamagePlayerCollision += ResetAttack;
    }

    void OnDisable() {
        GameManager.OnGSPlaying -= TikiAttack;
        FBCollisionEvent.onDamagePlayerCollision -= ResetAttack;
    }

    private void TikiAttack() {
        if (_gameManager.GameState == GameStates.Playing) {
            Instantiate(Fireball, FBSpawnPos.transform.position, Quaternion.identity);
        }
    }

    private void ResetAttack() {
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        yield return new WaitForSeconds(_attackDelay);
        TikiAttack();
    }
}

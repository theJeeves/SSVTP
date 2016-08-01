using UnityEngine;
using System.Collections;

public class TikiAnimationManager : MonoBehaviour {

    [SerializeField]
    private float damageDelay;

    private GameManager _gameManager;
    private Animator _animator;

	// Use this for initialization
	void Awake () {
        _gameManager = GameManager.Instance;
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameManager.GameState == GameStates.Playing) {

            if (_gameManager.HitTiki) {
                _gameManager.TikiDamageTaken = _gameManager.TikiHP % 3 == 0 ? true : false;
            }
            if (!_gameManager.TikiDamageTaken) {
                ChangeAnimationState(0);
            }
            else if (_gameManager.TikiDamageTaken) {
                ChangeAnimationState(2);
                StartCoroutine(DamageDelay());
            }
        }
	}

    private void ChangeAnimationState(int animationState) {
        _animator.SetInteger("AnimationState", animationState);
    }

    private IEnumerator DamageDelay() {
        yield return new WaitForSeconds(damageDelay);
        _gameManager.HitTiki = false;
        _gameManager.TikiDamageTaken = false;
    }
}
